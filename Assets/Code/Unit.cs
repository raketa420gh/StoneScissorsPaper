using System.Linq;
using Pathfinding;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Outline[] _outlines;
    private AIPath _aiPath;
    
    private UnitData _unitData;
    private PlayerData _playerData;
    private UnitType _enemyType;

    private UnitType EnemyType => _enemyType;

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

    private void MoveToEnemyTower()
    {
        var enemyTower = FindEnemyTower();
        _aiPath.destination = enemyTower.transform.position;
    }

    public void CheckNewEnemy()
    {
        var enemy = FindEnemy();

        if (enemy && enemy != this)
            MoveToEnemy(enemy.transform);
        else
            MoveToEnemyTower();
    }

    private Tower FindEnemyTower()
    {
        var allTowers = FindObjectsOfType<Tower>();
        return allTowers.FirstOrDefault(tower => tower.PlayerType != _playerData.Type);
    }

    private Unit FindEnemy()
    {
        var allUnits = FindObjectsOfType<Unit>();
        var enemy = allUnits.FirstOrDefault(unit => unit.EnemyType == EnemyType);
        return enemy;
    }

    private void MoveToEnemy(Transform transform) => 
        _aiPath.destination = transform.position;

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
            outline.OutlineColor = playerData.Material.color;
    }

    public void DestroyUnit() => 
        Destroy(gameObject);
}