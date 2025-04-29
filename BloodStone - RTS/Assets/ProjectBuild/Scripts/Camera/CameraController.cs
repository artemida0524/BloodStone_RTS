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

        [Header("Scrool Settings")]
        [SerializeField] private float scroolSpeed;
        [SerializeField] private float smoothSpeed;
        [SerializeField] private int minFOW = 40;
        [SerializeField] private int maxFOW = 70;

        private float _destinationZoom;

        private void Awake()
        {
            _cameraMover = new MouseMove(transform, () => cinemachineCamera.m_Lens.FieldOfView, () => moveSpeed / 10);

            // Uncomment the following line to use keyboard movement instead of mouse movement
            //_cameraMover = new KeyMove(transform, () => cinemachineCamera.m_Lens.FieldOfView, () => moveSpeed);
        }

        private void Start()
        {
            _destinationZoom = cinemachineCamera.m_Lens.FieldOfView;
        }

        private void LateUpdate()
        {
            HandleZoomInput();

            _cameraMover.Move();
        }

        private void HandleZoomInput()
        {
            float aa = Input.GetAxis("Mouse ScrollWheel") * scroolSpeed;

            if (aa != 0)
            {
                _destinationZoom -= aa;
                _destinationZoom = Mathf.Clamp(_destinationZoom, minFOW, maxFOW);
            }
            cinemachineCamera.m_Lens.FieldOfView = Mathf.Lerp(cinemachineCamera.m_Lens.FieldOfView, _destinationZoom, Time.deltaTime * smoothSpeed);
        }
    }
}