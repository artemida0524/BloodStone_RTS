using System.Collections.Generic;
using UnityEngine;


namespace Bar
{
    public class UIBarDataAssetList : MonoBehaviour
    {
        [field: SerializeField] public List<UIBarDataAsset> BarDataList { get; private set; }
    }
}