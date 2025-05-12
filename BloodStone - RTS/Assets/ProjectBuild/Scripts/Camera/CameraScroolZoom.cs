using Cinemachine;
using System;
using UnityEngine;

namespace GameCamera
{
    public class CameraScroolZoom : ICameraZoom
    {
        private readonly CinemachineVirtualCamera _camera;
        private readonly Func<float> _scroolSpeed;
        private readonly Func<float> _smoothSpeed;
        private readonly Func<float> _minFOW;
        private readonly Func<float> _maxFOW;

        private float _destinationZoom;

        public CameraScroolZoom(CinemachineVirtualCamera camera, Func<float> scroolSpeed, Func<float> smoothSpeed, Func<float> minFOW, Func<float> maxFOW)
        {
            _camera = camera;
            _scroolSpeed = scroolSpeed;
            _smoothSpeed = smoothSpeed;
            _minFOW = minFOW;
            _maxFOW = maxFOW;

            _destinationZoom = camera.m_Lens.FieldOfView;
        }

        public void Zoom()
        {
            float scrollDelta = Input.GetAxis("Mouse ScrollWheel") * _scroolSpeed();

            if (scrollDelta != 0)
            {
                _destinationZoom -= scrollDelta;
                _destinationZoom = Mathf.Clamp(_destinationZoom, _minFOW(), _maxFOW());
            }
            _camera.m_Lens.FieldOfView = Mathf.Lerp(_camera.m_Lens.FieldOfView, _destinationZoom, Time.deltaTime * _smoothSpeed());
        }
    }
}