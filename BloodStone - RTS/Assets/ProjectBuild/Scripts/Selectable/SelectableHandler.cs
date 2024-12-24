using Entity;
using Faction;
using System;
using System.Collections.Generic;
using Unit;
using UnityEngine;
using UnityEngine.EventSystems;
using Build;


namespace Select
{
    public class SelectableHandler : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private RectTransform selectRect;
        [SerializeField] private Build.Faction factionData;

        private Vector3 startPosition;
        private Vector3 endPosition;

        private List<UnitBase> selectedUnits = new();
        private Camera camera;

        public event Action<List<UnitBase>> OnSelectedUnits;

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
                    if (hitInfo.collider.TryGetComponent(out UnitBase unit))
                    {

                        if (unit.FactionType == factionData.FactionType)
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
            if (eventData.button == PointerEventData.InputButton.Left && factionData.Data.InteractionMode == InteractionMode.None)
            {
                #region View
                startPosition = Input.mousePosition;

                selectRect.gameObject.SetActive(true);
                #endregion

                factionData.Data.ChangeInteractionMode(InteractionMode.Selectable);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left && factionData.Data.InteractionMode == InteractionMode.Selectable)
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
                List<UnitBase> allUnits = factionData.Data.GetUnits<UnitBase>();

                foreach (var item in allUnits)
                {
                    if (rect.Contains(GetScreenPoint(item)))
                    {
                        if (item.FactionType == factionData.FactionType)
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

            if (eventData.button == PointerEventData.InputButton.Left && factionData.Data.InteractionMode == InteractionMode.Selectable)
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


        public void UnselectAll()
        {
            UnselectUnits();
            selectedUnits.Clear();
            OnSelectedUnits?.Invoke(selectedUnits);
            UpdateInteractionMode();
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
            factionData.Data.ChangeInteractionMode(mode);
        }
    }
}
