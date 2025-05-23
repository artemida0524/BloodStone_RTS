using BloodStone.Gameplay.Units;
using BloodStone.Gameplay.Units.Providers;
using GlobalData;
using Unit;
using UnityEngine;

public class UnitInitializer
{
    private readonly IUnitProvider _unitProvider;

    public UnitInitializer(IUnitProvider provider)
    {
        _unitProvider = provider;
    }


    public void Init()
    {
        UnitBase[] units = Object.FindObjectsOfType<UnitBase>();

        foreach (var item in units)
        {
            _unitProvider.PlaceUnit(item, item.transform.position);
        }
    }

}