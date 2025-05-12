using Cinemachine;
using UnityEngine;

namespace GameCamera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera cinemachineCamera;

        [Header("Move Settings")]
        [SerializeField] private float moveSpeed;

        private ICameraMover _cameraMover;
        private ICameraZoom _cameraZoom;

        [Header("Scrool Settings")]
        [SerializeField] private float scroolSpeed;
        [SerializeField] private float smoothSpeed;
        [SerializeField] private int minFOW = 40;
        [SerializeField] private int maxFOW = 70;

        private void Awake()
        {
            _cameraMover = new MouseMove(transform, () => cinemachineCamera.m_Lens.FieldOfView, () => moveSpeed / 10);

            // Uncomment the following line to use keyboard movement instead of mouse movement
            //_cameraMover = new KeyMove(transform, () => cinemachineCamera.m_Lens.FieldOfView, () => moveSpeed);


            _cameraZoom = new CameraScroolZoom(cinemachineCamera, () => scroolSpeed, () => smoothSpeed);
        }

        private void LateUpdate()
        {
            _cameraZoom.Zoom();
            _cameraMover.Move();
        }
    }
}