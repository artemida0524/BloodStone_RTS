using Select;
using State;
using System.Collections.Generic;
using Unit;
using UnityEngine;

namespace Build
{
    public class GenerateNewSimpleUnitTower : BuildInteractableBase
    {
        [SerializeField] private Transform pointSpawn;
        [SerializeField] private SimpleUnitBase unitPrefab;

        private const float timeOut = 10f;
        private float time = 0f;

        protected override void Update()
        {
            time += Time.deltaTime;
            if (time > timeOut)
            {
                time = 0f;

                SimpleUnitBase unit = Instantiate(unitPrefab, pointSpawn.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)), Quaternion.identity);
                unit.InitializationEntity(FactionType);
            }
        }

        public override void Interact()
        {
            Debug.Log("Just");
        }

        public override void Interact(IReadOnlyList<ISelectable> units)
        { 
            Debug.Log(units.Count);
        }
    }
}