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

        private float _destinationZoom;

        public CameraScroolZoom(CinemachineVirtualCamera camera, Func<float> scroolSpeed, Func<float> smoothSpeed)
        {
            _camera = camera;
            _scroolSpeed = scroolSpeed;
            _smoothSpeed = smoothSpeed;

            _destinationZoom = camera.m_Lens.FieldOfView;
        }

        public void Zoom()
        {
            float aa = Input.GetAxis("Mouse ScrollWheel") * _scroolSpeed();

            if (aa != 0)
            {
                _destinationZoom -= aa;
                _destinationZoom = Mathf.Clamp(_destinationZoom, 40, 70);
            }
            _camera.m_Lens.FieldOfView = Mathf.Lerp(_camera.m_Lens.FieldOfView, _destinationZoom, Time.deltaTime * _smoothSpeed());
        }
    }
}