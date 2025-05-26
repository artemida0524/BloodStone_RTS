using Bar;
using Cinemachine;
using GlobalData;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace Bar
{

    public class UIBarContainerView : UIBarContainerViewBase
    {
        [SerializeField] private RectTransform container;
        [SerializeField] private UIBarViewBase uiBarPrefab;

        private List<UIBarViewBase> bars;

        private bool isShow = false;

        private CinemachineVirtualCamera _cinemachineCamera;
        private float referenceFOV = 60f;
        private Vector2 baseScale;

        private void Start()
        {
            _cinemachineCamera = Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera as CinemachineVirtualCamera;
            baseScale = transform.localScale;
        }

        private void Update()
        {
            if (isShow)
            {
                UpdateLookAndScaleToCamera();
            }
        }

        private void UpdateLookAndScaleToCamera()
        {
            // generate with chatgpt))
            // Calculate the position based on the camera's position and rotation
            transform.LookAt(transform.position + _cinemachineCamera.transform.forward);


            // this also
            // Calculate the scale based on the camera's field of view
            float currentFOV = _cinemachineCamera.m_Lens.FieldOfView;
            float scaleMultiplier = Mathf.Tan(Mathf.Deg2Rad * currentFOV * 0.5f) / Mathf.Tan(Mathf.Deg2Rad * referenceFOV * 0.5f);

            transform.localScale = new Vector3(baseScale.x, baseScale.y, 1f) * scaleMultiplier;
        }

        public override void Show()
        {
            // I'm facing an issue where, when the panel is enabled, it first becomes visible and then scales, which causes a slight jitter before the image appears correctly and added an update
            UpdateLookAndScaleToCamera();
            base.Show();
            isShow = true;
        }

        public override void Hide()
        {
            base.Hide();
            isShow = false;
        }

        public override void AddBar(IBar bar)
        {
            if (bars == null)
            {
                bars = new();
            }

            var item = GetBarDataAssetByName(bar.Name);


            UIBarViewBase barInstance = Instantiate(uiBarPrefab, container);
            barInstance.Init(bar, item);

            bars.Add(barInstance);
        }

        public override void RemoveBar(string nameBar)
        {
            foreach (var item in bars)
            {
                if (item.Stat.Name == name)
                {
                    item.Dispose();
                    bars.Remove(item);

                    Destroy(item.gameObject);

                    return;
                }
            }

            throw new Exception("NotFound");
        }

    }

}