using Bar;
using UnityEngine;
using Zenject;

namespace Unit
{
    public class SimpleUnitTest : SimpleUnitBase
    {
        protected override void Update()
        {
            base.Update();

            Debug.Log(IsSelection); 

        }
    }
}