using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotateObj : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Update()
    {
        transform.Rotate(new Vector3(0, -1, 0) * speed * Time.deltaTime);
    }
}
