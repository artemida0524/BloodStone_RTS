using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.EventSystems;

namespace GameCamera
{
    public class MiddleClickCameraMover : ICameraClickMover
    {
        private readonly Transform _cameraTransform;
        private readonly Func<Ease> _ease;
        private readonly Func<float> _moveSpeed;
        private readonly LayerMask _mask;

        private float _сameraOffsetZ = -16f;

        private float _doubleClickThreshold = 0.2f;
        private float _lastClickTime = -1f;

        private float _maxDistance = 100f;
        private Tween _moveTween;
        public bool IsMoving
        {
            get
            {
                if (_moveTween != null)
                {
                    return _moveTween.IsActive() && _moveTween.IsPlaying();
                }
                return false;
            }
        }

        public MiddleClickCameraMover(Transform cameraTransform, Func<Ease> ease, Func<float> moveSpeed, LayerMask mask)
        {
            _cameraTransform = cameraTransform;
            _ease = ease;
            _moveSpeed = moveSpeed;
            _mask = mask;
        }


        public void Move()
        {
            if (Input.GetMouseButtonUp(2) && !EventSystem.current.IsPointerOverGameObject())
            {
                if (Time.time - _lastClickTime < _doubleClickThreshold)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out RaycastHit hit, _maxDistance, _mask))
                    {
                        Vector3 newPos = hit.point;

                        newPos.y = _cameraTransform.position.y;
                        newPos.z += _сameraOffsetZ;

                        _moveTween?.Kill();

                        _moveTween = _cameraTransform.transform
                            .DOMove(newPos, _moveSpeed())
                            .SetEase(_ease());
                    }
                }
                _lastClickTime = Time.time;
            }
        }

        public void Reset()
        {
            _moveTween?.Kill();
        }
    }
}