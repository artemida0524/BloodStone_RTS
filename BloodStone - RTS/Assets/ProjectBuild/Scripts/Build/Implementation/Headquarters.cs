using UnityEngine;
using Faction;
using System.Collections.Generic;
using System.Linq;
using Bar;
using Currency;
using Select;
using Zenject;
using GlobalData;
using Game.Gameplay.Units.Providers;
using Game.Gameplay.Selection;
using Unity.PlasticSCM.Editor.WebApi;
using System;

namespace Game.Gameplay.Build
{
    // Main Building of the game
    public class Headquarters : BuildInteractableBase, ICurrencyStorage
    {
        public FactionDataHandler Data
        {
            get
            {
                if(_factionDataHandler == null)
                {
                    _factionDataHandler = new FactionDataHandler(_buildingProvider, _unitProvider);
                }
                
                return _factionDataHandler;
            }
        }

        public InteractionMode InteractionMode { get; private set; }

        public event Action<InteractionMode> OnInteractionModeChanged;

        private FactionDataHandler _factionDataHandler;

        [SerializeField] private UIStatsContainerView uiStatsContainer;

        private List<ICurrency> currencies = new List<ICurrency>();
        public IReadOnlyList<ICurrency> Currencies => currencies;

        private IUnitProvider _unitProvider;
        private IBuildingProvider _buildingProvider;


        [Inject]
        private void Construct(IBuildingProvider buildingProvider, IUnitProvider unitProvider)
        {
            _unitProvider = unitProvider;
            _buildingProvider = buildingProvider;
        }

        protected override void Awake()
        {
            base.Awake();

            currencies.Add(new CurrencyBase(ResourceNames.GOLD, 50, 100));
            currencies.Add(new CurrencyBase(ResourceNames.TREE, 50, 100));
        }




        public override void Interact()
        {
            Debug.Log("Interact");
        }

        public override void Interact(IReadOnlyList<ISelectable> units)
        {
            Debug.Log(units.Count);
        }

        public override void Hover()
        {

            base.Hover();

            uiStatsContainer?.gameObject.SetActive(true);

        }

        public override void Unhover()
        {
            base.Unhover();

            uiStatsContainer?.gameObject.SetActive(false);

        }

        protected override void SetStats()
        {
            base.SetStats();

            _entityStats.Add(GetCurrencyByName(ResourceNames.GOLD));
            _entityStats.Add(GetCurrencyByName(ResourceNames.TREE));
        }

        protected override void SetStatsView()
        {
            base.SetStatsView();

            uiStatsContainer?.AddBar(_entityStats[0]);
            uiStatsContainer?.AddBar(_entityStats[1]);
            uiStatsContainer?.AddBar(_entityStats[2]);
        }




        public void ChangeInteractionMode(InteractionMode interactionMode)
        {
            InteractionMode = interactionMode;
            OnInteractionModeChanged?.Invoke(interactionMode);
        }

        public ICurrency GetCurrencyByName(string name)
        {
            return Currencies.FirstOrDefault(c => c.Name == name);
        }

        public ICurrency GetFirstCurrency()
        {
            return Currencies[0];
        }

        public void AddCurrencyByName(string name, int amount)
        {
            GetCurrencyByName(name).Add(amount);
        }

        public bool SpendCurrencyByName(string name, int amount) 
        {
            return GetCurrencyByName(name).Spend(amount);
        }

    }
}