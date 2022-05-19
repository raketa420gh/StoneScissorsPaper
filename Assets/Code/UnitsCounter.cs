using System.Collections.Generic;
using UnityEngine;

public class UnitsCounter : MonoBehaviour
{
    private readonly List<Unit> _allUnitsOnScene = new List<Unit>();
    private readonly List<Tower> _allTowersOnScene = new List<Tower>();

    public List<Unit> AllUnitsOnScene => _allUnitsOnScene;
    public List<Tower> AllTowers => _allTowersOnScene;

    public void AddUnit(Unit unit)
    {
        _allUnitsOnScene.Add(unit);
        unit.OnDestroy += OnUnitDestroy;
    }
    
    public void AddTower(Tower tower) => 
        _allTowersOnScene.Add(tower);

    private void RemoveUnit(Unit unit) => 
        _allUnitsOnScene.Remove(unit);

    private void OnUnitDestroy(Unit unit) => 
        RemoveUnit(unit);
}