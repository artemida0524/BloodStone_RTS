using UnityEngine;
using UnityEngine.Rendering;

namespace Bar
{
    [CreateAssetMenu(fileName = "UIBar", menuName = "UIBar")]
	public class UIBarDataAsset : ScriptableObject
	{
		[field: SerializeField] public string Name { get; private set; }
		[field: SerializeField] public Sprite Icon { get; private set; }
		[field: SerializeField] public Sprite Background { get; private set; }
		[field: SerializeField] public Sprite Row { get; private set; }
	}
}