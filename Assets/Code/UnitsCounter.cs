using System.Collections.Generic;
using UnityEngine;

public class UnitsCounter : MonoBehaviour
{
    private readonly List<Unit> _allUnitsOnScene = new List<Unit>();

    public List<Unit> AllUnitsOnScene => _allUnitsOnScene;

    public void AddUnit(Unit unit)
    {
        _allUnitsOnScene.Add(unit);
        unit.OnDestroy += OnUnitDestroy;
    }

    private void RemoveUnit(Unit unit) => 
        _allUnitsOnScene.Remove(unit);

    private void OnUnitDestroy(Unit unit) => 
        RemoveUnit(unit);
}