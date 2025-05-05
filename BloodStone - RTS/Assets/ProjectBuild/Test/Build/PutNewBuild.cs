using Build;
using UnityEngine;
using Zenject;

public class PutNewBuild : MonoBehaviour
{
    [SerializeField] private BuildInteractableBase build;

    private IBuildingSystemProvider _buildingSystemProvider;

    [Inject]
    private void Construct(IBuildingSystemProvider buildingSystemProvider)
    {
        _buildingSystemProvider = buildingSystemProvider;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            _buildingSystemProvider.SetBuild(Instantiate(build));
        }
    }
}