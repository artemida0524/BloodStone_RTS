using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Bar
{
    [CreateAssetMenu(fileName = "UIBar", menuName = "UIBar")]
	public class UIBarDataSO : ScriptableObject
	{
		[field: SerializeField] public string Name { get; private set; }
		[field: SerializeField] public Sprite Icon { get; private set; }
	}

}