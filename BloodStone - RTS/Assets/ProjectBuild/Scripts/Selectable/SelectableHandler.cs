using Build;
using Entity;
using Faction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unit;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Select
{
    public class SelectableHandler : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerMoveHandler
    {
        [SerializeField] private RectTransform selectRect;
        private Build.Faction faction;

        private Vector3 startPosition;
        private Vector3 endPosition;

        private List<ISelectable> selectables = new();

        private Camera camera;

        private IHoverable hover;

        public event Action<List<ISelectable>> OnSelectedUnits;

        [Inject]
        private void Construct(Build.Faction faction)
        {
            this.faction = faction;
        }


        private void Awake()
        {
            camera = Camera.main;
        }

        private void Update()
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
                        Unselect();
                    }

                    UpdateInteractionMode();
                }
            }


        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left && (faction.Data.InteractionMode == InteractionMode.None || faction.Data.InteractionMode == InteractionMode.Setable))
            {
                #region View
                startPosition = Input.mousePosition;

                selectRect.gameObject.SetActive(true);
                #endregion

                faction.Data.ChangeInteractionMode(InteractionMode.Selectable);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left && faction.Data.InteractionMode == InteractionMode.Selectable)
            {
                #region View
                endPosition = Input.mousePosition;

                Vector2 pivotPosition = Vector2.Min(startPosition, endPosition);
                Vector2 width = Vector2.Max(startPosition, endPosition);
                Vector2 size = width - pivotPosition;

                selectRect.anchoredPosition = pivotPosition;

                selectRect.sizeDelta = size;
                #endregion

                Unselect();

                Rect rect = new Rect(pivotPosition, size);


                List<ISelectable> allSelectable = faction.Data.GetAll<ISelectable>();
                IEnumerable<ISelectable> myEntities = allSelectable.Where(unit => (unit as EntityBase).FactionType == faction.FactionType);

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
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {

            if (eventData.button == PointerEventData.InputButton.Left && faction.Data.InteractionMode == InteractionMode.Selectable)
            {
                #region View
                selectRect.gameObject.SetActive(false);

                #endregion

                UpdateInteractionMode();
                OnSelectedUnits?.Invoke(selectables);
            }

        }


        public void OnPointerMove(PointerEventData eventData)
        {
            Ray ray2 = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray2, out RaycastHit hitInfo2))
            {
                if (hitInfo2.collider.TryGetComponent(out IHoverable hover))
                {
                    if (this.hover != hover)
                    {
                        this.hover?.Unhover();
                        this.hover = hover;
                        this.hover.Hover();
                    }
                }
                else
                {
                    this.hover?.Unhover();
                    this.hover = null;
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
            UnselectUnits();
            selectables.Clear();
            OnSelectedUnits?.Invoke(selectables);
            UpdateInteractionMode();
        }

        private Vector2 GetScreenPoint(Vector3 position)
        {
            return camera.WorldToScreenPoint(position);
        }

        void ToggleUnitSelection(ISelectable select)
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

        void UpdateInteractionMode()
        {
            var mode = selectables.Count > 0 ? InteractionMode.Setable : InteractionMode.None;
            faction.Data.ChangeInteractionMode(mode);
        }


    }
}
