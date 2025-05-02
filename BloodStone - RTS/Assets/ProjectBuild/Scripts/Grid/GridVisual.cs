using GlobalData;
using System.Collections.Generic;
using UnityEngine;

public class GridVisual : MonoBehaviour
{

    [field: SerializeField] private int Height { get; set; } = 10;
    [field: SerializeField] private int Width { get; set; } = 10;


    
    private void OnDrawGizmos()
    {
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                if ((i + j) % 2 == 0)
                {
                    Gizmos.color = Color.red;
                }
                else
                {
                    Gizmos.color = Color.green;
                }

                Gizmos.color = new Color(Gizmos.color.r, Gizmos.color.g, Gizmos.color.b, 0.5f);
                Gizmos.DrawCube(new Vector3(i + 0.5f, 0, j + 0.5f), new Vector3(1, 0.3f, 1));
            }
        }
    }
}
