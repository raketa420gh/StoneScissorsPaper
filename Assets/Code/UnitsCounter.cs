using System.Collections.Generic;
using UnityEngine;

public class UnitsCounter : MonoBehaviour
{
    private readonly List<UnitBase> _allUnitsOnScene = new List<UnitBase>();
    private readonly List<Tower> _allTowersOnScene = new List<Tower>();

    public List<UnitBase> AllUnitsOnScene => _allUnitsOnScene;
    public List<Tower> AllTowers => _allTowersOnScene;

    public void AddUnit(UnitBase unit)
    {
        _allUnitsOnScene.Add(unit);
        unit.OnDestroy += OnUnitDestroy;
    }
    
    public void AddTower(Tower tower) => 
        _allTowersOnScene.Add(tower);

    private void RemoveUnit(UnitBase unitBase) => 
        _allUnitsOnScene.Remove(unitBase);

    private void OnUnitDestroy(UnitBase unitBase) => 
        RemoveUnit(unitBase);
}