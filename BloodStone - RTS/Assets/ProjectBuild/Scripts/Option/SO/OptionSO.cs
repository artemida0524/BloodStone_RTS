using UnityEngine;

[CreateAssetMenu(fileName = "Option", menuName ="Option")]
public class OptionSO : ScriptableObject
{
    [field:SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
}
