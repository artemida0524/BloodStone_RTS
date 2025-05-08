using Bar;
using UnityEngine;
using Zenject;

namespace Unit
{
    public class SimpleUnitTest : SimpleUnitBase
    {
        [Inject]
        private void Construct()
        {

            Debug.Log("Construct");
        }
    }
}