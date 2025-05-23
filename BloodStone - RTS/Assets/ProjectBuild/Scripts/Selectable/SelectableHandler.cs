using BloodStone.Gameplay.Build;
using BloodStone.Gameplay.Selection;
using BloodStone.Gameplay.Units;
using BloodStone.Gameplay.Units.Utils;
using BloodStone.Gameplay.Entity;
using Faction;
using System;
using System.Collections.Generic;
using System.Linq;
using Unit;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Select
{
    public class SelectableHandler : MonoBehaviour
    {
        [SerializeField] private RectTransform selectRect;
        private Headquarters faction;

        private Vector3 startPosition;
        private Vector3 endPosition;

        private Vector2 pivotPosition;
        private Vector2 width;
        private Vector2 size;

        private List<ISelectable> selectables = new();
        new private Camera camera;
        private IHoverable currentHover;

        private bool isDragging = false;

        private bool isPreparingToDrag = false;
        [SerializeField] private float dragThreshold = 10f;

        public event Action<IReadOnlyList<ISelectable>> OnSelectedUnits;

        [Inject]
        private void Construct(Headquarters faction)
        {
            this.faction = faction;
        }

        public void Init()
        {
            camera = Camera.main;

            UnitUtility.OnUnitDisableOrDestroy += OnUnitDisableOrDestroyHandler;
            BuildUtility.OnBuildDisableOrDestroy += OnBuildDisableOrDestroyHandler;
            faction.OnFactionTypeChanged += OnFactionTypeChangedHandler;
        }

        private void OnFactionTypeChangedHandler(FactionType type)
        {

            //if (false)
            //{
            //    currentHover?.Unhover(); 
            //}

        }

        private void OnBuildDisableOrDestroyHandler(BuildBase build)
        {
            if (selectables.Contains(build as ISelectable))
            {
                selectables.Remove(build as ISelectable);

                OnSelectedUnits?.Invoke(selectables);
            }
        }

        private void OnUnitDisableOrDestroyHandler(UnitBase unit)
        {
            if (selectables.Contains(unit))
            {
                selectables.Remove(unit);

                OnSelectedUnits?.Invoke(selectables);
            }
        }

        private void Update()
        {
            if (faction.Data.InteractionMode != InteractionMode.Build)
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    HandleClickSelection();
                }
                HandleDragSelection();
                HandleHoverEffect();
            }
        }

        private void HandleClickSelection()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                    if (hitInfo.collider.TryGetComponent(out ISelectable unit))
                    {
                        EntityBase entity = unit as EntityBase;
                        if (entity.FactionType == faction.FactionType)
                        {
                            ToggleUnitSelection(unit);
                        }
                    }
                    else
                    {
                        UnselectAll();
                    }
                    UpdateInteractionMode();
                }
            }
        }

        private void HandleDragSelection()
        {
            if (Input.GetMouseButtonDown(0) && (faction.Data.InteractionMode == InteractionMode.None || faction.Data.InteractionMode == InteractionMode.Setable))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    startPosition = Input.mousePosition;
                    isPreparingToDrag = true;
                }
            }

            if (Input.GetMouseButton(0) && isPreparingToDrag)
            {
                float distance = Vector2.Distance(Input.mousePosition, startPosition);
                if (distance > dragThreshold)
                {
                    isPreparingToDrag = false;
                    isDragging = true;
                    selectRect.gameObject.SetActive(true);
                    selectRect.sizeDelta = Vector2.zero;
                    faction.Data.ChangeInteractionMode(InteractionMode.Selectable);
                    Unselect();
                }
            }

            if (Input.GetMouseButton(0) && isDragging)
            {
                endPosition = Input.mousePosition;

                pivotPosition = Vector2.Min(startPosition, endPosition);
                width = Vector2.Max(startPosition, endPosition);
                size = width - pivotPosition;

                selectRect.position = (startPosition + endPosition) / 2;
                selectRect.sizeDelta = size;
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (isDragging)
                {
                    selectRect.gameObject.SetActive(false);

                    Rect rect = new Rect(pivotPosition, size);
                    IEnumerable<ISelectable> myEntities = faction.Data.GetAll<ISelectable>().Where(unit => (unit as EntityBase).FactionType == faction.FactionType);

                    //Debug.Log(myEntities.OfType<UnitBase>().Count());

                    foreach (var item in myEntities)
                    {
                        if (rect.Contains(GetScreenPoint(item.Position)))
                        {
                            if (item.Select())
                            {
                                selectables.Add(item);
                            }
                        }
                    }

                    UpdateInteractionMode();
                    OnSelectedUnits?.Invoke(selectables);
                    isDragging = false;
                }
                else if (isPreparingToDrag)
                {
                    HandleClickSelection();
                }

                isPreparingToDrag = false;
            }
        }

        private void HandleHoverEffect()
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.TryGetComponent(out IHoverable newHover))
                {
                    FactionType type = (newHover as IEntity).FactionType;

                    if (type == faction.FactionType || type == FactionType.Systems)
                    {
                        if (currentHover != newHover)
                        {
                            currentHover?.Unhover();
                            currentHover = newHover;
                            currentHover.Hover();
                        }
                    }
                }
                else
                {
                    currentHover?.Unhover();
                    currentHover = null;
                }
            }
        }

        private void UnselectUnits()
        {
            foreach (var item in selectables)
            {
                item.Unselect();
            }
        }

        private void Unselect()
        {
            UnselectUnits();
            selectables.Clear();
        }

        public void UnselectAll()
        {
            Unselect();
            OnSelectedUnits?.Invoke(selectables);
            UpdateInteractionMode();
        }

        private Vector2 GetScreenPoint(Vector3 position)
        {
            return camera.WorldToScreenPoint(position);
        }

        private void ToggleUnitSelection(ISelectable select)
        {
            if (select.IsSelection)
            {
                select.Unselect();
                selectables.Remove(select);
            }
            else if (select.Select())
            {
                selectables.Add(select);
            }
            OnSelectedUnits?.Invoke(selectables);
        }

        private void UpdateInteractionMode()
        {
            var mode = selectables.Count > 0 ? InteractionMode.Setable : InteractionMode.None;
            faction.Data.ChangeInteractionMode(mode);
        }
    }
}