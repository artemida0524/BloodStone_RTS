using Cinemachine;
using DG.Tweening;
using UnityEngine;

namespace GameCamera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera cinemachineCamera;

        [Header("Move Settings")]
        [SerializeField] private float moveSpeed;
        [SerializeField] private float clickMoveSpeed = 0.3f;
        [SerializeField] private Ease ease;
        [SerializeField] private LayerMask mask;

        private ICameraMover _cameraMover;
        private ICameraClickMover _cameraClickMover;

        [Header("Scrool Settings")]
        [SerializeField] private float scroolSpeed;
        [SerializeField] private float smoothSpeed;
        [SerializeField] private int minFOW = 40;
        [SerializeField] private int maxFOW = 70;

        private ICameraZoom _cameraZoom;

        private void Awake()
        {
            _cameraMover = new MouseMove(transform, () => cinemachineCamera.m_Lens.FieldOfView, () => moveSpeed / 10);

            // Uncomment the following line to use keyboard movement instead of mouse movement
            //_cameraMover = new KeyMove(transform, () => cinemachineCamera.m_Lens.FieldOfView, () => moveSpeed);

            _cameraZoom = new CameraScroolZoom(cinemachineCamera, () => scroolSpeed, () => smoothSpeed, () => minFOW, () => maxFOW);
            _cameraClickMover = new MiddleClickCameraMover(transform, () => ease, () => clickMoveSpeed, mask);
        }

        private void LateUpdate()
        {
            _cameraZoom.Zoom();

            _cameraMover.Move();
            _cameraClickMover.Move();

            if (_cameraMover.IsMoving)
            {
                if(_cameraClickMover.IsMoving)
                {
                    _cameraClickMover.Reset();
                }
            }
        }
    }
}