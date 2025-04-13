using Cinemachine;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float speedScrool;

    private CinemachineVirtualCamera cCamera;

    private float x;
    private float z;

    private void Start()
    {
        cCamera = FindAnyObjectByType<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");


        transform.position += new Vector3(x, 0, z) * Time.deltaTime * speed * (cCamera.m_Lens.FieldOfView / 10);

        float aa = Input.GetAxis("Mouse ScrollWheel") * speedScrool;

        if (aa != 0)
        {
            cCamera.m_Lens.FieldOfView -= aa;

            cCamera.m_Lens.FieldOfView = Mathf.Clamp(cCamera.m_Lens.FieldOfView, 40, 70);
        }


    }

}
