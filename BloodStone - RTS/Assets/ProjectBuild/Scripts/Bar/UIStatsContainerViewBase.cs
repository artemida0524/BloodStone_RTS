using Game.Gameplay.Stats;
using System.Collections.Generic;
using UnityEngine;



namespace Bar
{
    public abstract class UIStatsContainerViewBase : MonoBehaviour
    {
        [SerializeField] protected UIBarDataAssetList uiBarDataAssetList;
        [SerializeField] protected GameObject panel;

        protected Dictionary<string, UIBarDataAsset> uiBarDataAssetDictionary = new();
        private bool _isInitialized = false;

        protected virtual void Awake()
        {
            if(!_isInitialized)
            {
                Initialization();
            }
        }

        public abstract void AddBar(IStat bar);
        public abstract void RemoveBar(string nameBar);

        public virtual void Show()
        {
            panel?.SetActive(true);
        }

        public virtual void Hide()
        {
            panel?.SetActive(false);
        }

        protected virtual void Initialization()
        {
            foreach (var barData in uiBarDataAssetList.BarDataList)
            {
                uiBarDataAssetDictionary.Add(barData.Name, barData);
            }
            _isInitialized = true;
        }

        protected virtual UIBarDataAsset GetBarDataAssetByName(string nameBar)
        {
            if (!_isInitialized)
            {
                Initialization();
            }

            if(uiBarDataAssetDictionary.TryGetValue(nameBar, out var barData))
            {
                return barData;
            }
            throw new KeyNotFoundException($"Bar with name {nameBar} not found in the dictionary.");
        }
    }
}