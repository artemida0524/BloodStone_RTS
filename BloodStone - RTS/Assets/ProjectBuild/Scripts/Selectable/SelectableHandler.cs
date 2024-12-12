using Entity;
using Faction;
using System;
using System.Collections.Generic;
using Unit;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Select
{
    public class SelectableHandler : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private RectTransform selectRect;
        [SerializeField] private FactionData faction;

        private Vector3 startPosition;
        private Vector3 endPosition;

        private List<UnitBase> selectedUnits = new();
        private Camera camera;

        public event Action<List<UnitBase>> OnSelectedUnits;

        private void Awake()
        {
            camera = Camera.main;

            // must be changed
            foreach (var item in faction.Units)
            {
                if (item.gameObject.activeSelf)
                {
                    item.Initialization(faction.FactionType, faction.CollectionData);
                }
            }
        }

        private void Update()
        {
            Debug.Log(faction.InteractionMode);

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                    if (hitInfo.collider.TryGetComponent(out UnitBase unit))
                    {

                        if (unit.FactionType == faction.FactionType)
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
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                #region View
                startPosition = Input.mousePosition;

                selectRect.gameObject.SetActive(true);
                #endregion

                faction.ChangeInteractionMode(InteractionMode.Selectable);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
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
                List<UnitBase> allUnits = faction.CollectionData.GetUnits<UnitBase>();

                //List<UnitBase> visibleUnits = allUnits.Where(unit => unit.Renderer.isVisible == true).ToList();

                foreach (var item in allUnits)
                {
                    if (rect.Contains(GetScreenPoint(item)))
                    {
                        if (item.FactionType == faction.FactionType)
                        {
                            if (item.Select())
                            {
                                selectedUnits.Add(item);
                            }
                        }
                    }
                }
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {

            if (eventData.button == PointerEventData.InputButton.Left)
            {
                #region View
                selectRect.gameObject.SetActive(false);

                #endregion

                UpdateInteractionMode();
                OnSelectedUnits?.Invoke(selectedUnits);
            }

        }

        private void UnselectUnits()
        {
            foreach (var item in selectedUnits)
            {
                item.Unselect();
            }
        }

        private void Unselect()
        {
            UnselectUnits();

            selectedUnits.Clear();
        }

        private Vector2 GetScreenPoint(EntityBase entity)
        {
            return camera.WorldToScreenPoint(entity.transform.position);
        }

        void ToggleUnitSelection(UnitBase unit)
        {
            if (unit.IsSelection)
            {
                unit.Unselect();
                selectedUnits.Remove(unit);
            }
            else if (unit.Select())
            {
                selectedUnits.Add(unit);
            }

            OnSelectedUnits?.Invoke(selectedUnits);
        }

        void UpdateInteractionMode()
        {
            var mode = selectedUnits.Count > 0 ? InteractionMode.Setable : InteractionMode.None;
            faction.ChangeInteractionMode(mode);
        }
    }
}
