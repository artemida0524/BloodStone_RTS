using System;
using UnityEngine;

namespace GameCamera
{
    public class MouseMove : ICameraMover
    {
        private readonly Transform _cameraTransform;
        private readonly Func<float> _speed;
        private readonly Func<float> _fieldOfView;

        private Vector3 _lastMousePosition;

        public MouseMove(Transform cameraTransform, Func<float> fieldOfView, Func<float> speed)
        {
            _cameraTransform = cameraTransform;
            _fieldOfView = fieldOfView;
            _speed = speed;
        }

        public bool IsMoving { get; private set; } = false;

        public void Move()
        {
            if (Input.GetMouseButtonDown(2))
            {
                _lastMousePosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(2))
            {
                if (_lastMousePosition != Input.mousePosition)
                {
                    Vector3 currentMousePosition = Input.mousePosition;
                    Vector3 mouseDelta = currentMousePosition - _lastMousePosition;
                    _lastMousePosition = currentMousePosition;
                    float fieldOfViewFactor = _fieldOfView() / 8;

                    Vector3 move = new Vector3(-mouseDelta.x, 0, -mouseDelta.y) * _speed() * fieldOfViewFactor * Time.deltaTime;
                    _cameraTransform.position += move;

                    IsMoving = true;
                }
                else
                {
                    IsMoving = false;
                }
            }
            else
            {
                IsMoving = false;
            }
        }
    }
}