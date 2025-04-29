using System;
using UnityEngine;

namespace GameCamera
{
    public class KeyMove : ICameraMover
    {
        private readonly Transform _cameraTransform;
        private readonly Func<float> _fieldOfView;
        private readonly Func<float> _speed;

        private float _x;
        private float _z;

        public KeyMove(Transform cameraTransform, Func<float> fieldOfView, Func<float> speed)
        {
            _cameraTransform = cameraTransform;
            _fieldOfView = fieldOfView;
            _speed = speed;
        }

        public void Move()
        {
            _x = Input.GetAxis("Horizontal");
            _z = Input.GetAxis("Vertical");

            if (_x != 0 || _z != 0)
            {
                Vector3 newPsotion = _cameraTransform.position;
                float fieldOfViewFactor = _fieldOfView() / 8;

                newPsotion += new Vector3(_x, 0, _z) * _speed() * fieldOfViewFactor * Time.deltaTime;

                _cameraTransform.position = newPsotion; 
            }
        }
    }
}