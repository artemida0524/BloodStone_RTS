using BloodStone.Gameplay.Entity;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EntityBase entity))
        {
            Debug.Log(entity.name);
        }
    }
}
