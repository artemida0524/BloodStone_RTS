using UnityEngine;

namespace Build
{
    public interface IVisualizable
    {
        void Visualize();
        void Unvisualize();
        void SetColor(Color color); 
    }
}