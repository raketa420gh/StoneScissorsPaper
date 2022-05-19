using System;
using System.Linq;
using Pathfinding;
using UnityEngine;
using Zenject;

public class Unit : MonoBehaviour
{
    public event Action<Unit> OnDestroy;
    
    private Outline[] _outlines;
    private AIPath _aiPath;
    
    private UnitData _unitData;
    private PlayerData _playerData;
    private UnitType _enemyType;
    private UnitsCounter _unitsCounter;

    private UnitType EnemyType => _enemyType;

    [Inject]
    public void Construct(UnitsCounter unitsCounter)
    {
        _unitsCounter = unitsCounter;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var unit = collision.gameObject.GetComponent<Unit>();

        if (unit)
            if (_playerData.Type != unit._playerData.Type)
                if (EnemyType == unit._unitData.UnitType)
                    unit.DestroyUnit();
    }

    public void Initialize(PlayerData playerData, UnitData unitData)
    {
        _playerData = playerData;
        _unitData = unitData;

        _outlines = GetComponentsInChildren<Outline>();
        _aiPath = GetComponent<AIPath>();

        SetEnemyType(unitData);
        SetupView(playerData);
    }

    public void DestroyUnit()
    {
        Destroy(gameObject);
        OnDestroy?.Invoke(this);
    }

    public Transform CheckNewEnemy()
    {
        var allUnits = _unitsCounter.AllUnitsOnScene;
        
        foreach (var unit in allUnits)
        {
            if (_playerData.Type != unit._playerData.Type)
            {
                if (EnemyType == unit._unitData.UnitType)
                {
                    return unit.transform;
                }
            }
        }

        return FindEnemyTower().transform;
    }

    public void MoveToEnemy(Transform transform) => 
        _aiPath.destination = transform.position;

    private void MoveToEnemyTower()
    {
        var enemyTower = FindEnemyTower();
        _aiPath.destination = enemyTower.transform.position;
    }

    private Tower FindEnemyTower()
    {
        var allTowers = FindObjectsOfType<Tower>();
        return allTowers.FirstOrDefault(tower => tower.PlayerType != _playerData.Type);
    }

    private void SetEnemyType(UnitData unitData)
    {
        if (unitData.UnitType == UnitType.Scissors)
            _enemyType = UnitType.Paper;
        if (unitData.UnitType == UnitType.Stone)
            _enemyType = UnitType.Scissors;
        if (_unitData.UnitType == UnitType.Paper)
            _enemyType = UnitType.Stone;
    }

    private void SetupView(PlayerData playerData)
    {
        foreach (var outline in _outlines)
        {
            outline.OutlineColor = playerData.Material.color;
            outline.OutlineWidth = 10f;
        }
    }
}