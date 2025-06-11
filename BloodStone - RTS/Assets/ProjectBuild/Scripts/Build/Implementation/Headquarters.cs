using UnityEngine;
using Faction;
using System.Collections.Generic;
using System.Linq;
using Bar;
using Currency;
using Zenject;
using GlobalData;
using Game.Gameplay.Units.Providers;
using Game.Gameplay.Selection;
using System;

namespace Game.Gameplay.Build
{
    // Main Building of the game
    public sealed class Headquarters : BuildInteractableBase, ICurrencyStorage, IHut
    {
        [SerializeField] private UIStatsContainerView uiStatsContainer;
        [field: SerializeField] public int MaxUnitCount { get; private set; }

        public FactionDataHandler Data
        {
            get
            {
                if (_factionDataHandler == null)
                {
                    _factionDataHandler = new FactionDataHandler(_buildingProvider, _unitProvider, FactionType);
                }

                return _factionDataHandler;
            }
        }

        public InteractionMode InteractionMode { get; private set; }
        public IReadOnlyList<ICurrency> Currencies => _currencies;

        private FactionDataHandler _factionDataHandler;
        private List<ICurrency> _currencies = new List<ICurrency>();

        private IUnitProvider _unitProvider;
        private IBuildingProvider _buildingProvider;

        public event Action<InteractionMode> OnInteractionModeChanged;


        [Inject]
        private void Construct(IBuildingProvider buildingProvider, IUnitProvider unitProvider)
        {
            _unitProvider = unitProvider;
            _buildingProvider = buildingProvider;
        }

        protected override void Awake()
        {
            base.Awake();

            _currencies.Add(new CurrencyBase(StatsNames.GOLD, 50, 100));
            _currencies.Add(new CurrencyBase(StatsNames.TREE, 50, 100));
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

            _entityStats.Add(GetCurrencyByName(StatsNames.GOLD));
            _entityStats.Add(GetCurrencyByName(StatsNames.TREE));
        }

        protected override void SetStatsView()
        {
            base.SetStatsView();


            foreach (var item in _entityStats)
            {
                if (item.Name == StatsNames.HEALTH)
                {
                    uiStatsContainer?.AddBar(item);
                    return;
                }
            }
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