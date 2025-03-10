using System.Collections.Generic;
using UnityEngine;


namespace Bar
{
    public class UIBarDataSOList : MonoBehaviour
    {
        [field: SerializeField] public List<UIBarDataSO> BarDataList { get; private set; }
    }
}