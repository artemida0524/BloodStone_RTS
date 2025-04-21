using Cinemachine;
using UnityEditor.MemoryProfiler;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float speedScrool;
    [SerializeField] private float speedZoom;


    private CinemachineVirtualCamera cCamera;
    private float destinationZoom;

    private float x;
    private float z;

    private void Start()
    {
        cCamera = FindAnyObjectByType<CinemachineVirtualCamera>();
        destinationZoom = cCamera.m_Lens.FieldOfView;
    }

    private void Update()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");


        transform.position += new Vector3(x, 0, z) * Time.deltaTime * speed * (cCamera.m_Lens.FieldOfView / 10);

        float aa = Input.GetAxis("Mouse ScrollWheel") * speedScrool;

        if (aa != 0)
        {
            destinationZoom -= aa;
            destinationZoom = Mathf.Clamp(destinationZoom, 40, 70);
        }
        cCamera.m_Lens.FieldOfView = Mathf.Lerp(cCamera.m_Lens.FieldOfView, destinationZoom, Time.deltaTime * speedZoom);

    }

}
