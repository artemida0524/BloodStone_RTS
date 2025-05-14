using UnityEngine;


namespace Bar
{
    [CreateAssetMenu(fileName = "UIBar", menuName = "UIBar")]
	public class UIBarDataAsset : ScriptableObject
	{
		[field: SerializeField] public string Name { get; private set; }
		[field: SerializeField] public Sprite Icon { get; private set; }
		[field: SerializeField] public Color RowColor { get; private set; }
	}
}