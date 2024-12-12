using System.Collections;
using System.Collections.Generic;
using Unit;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out UnitBase unit))
        {
            Debug.Log(other.name);
        }
    }
}
