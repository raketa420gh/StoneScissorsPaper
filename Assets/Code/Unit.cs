using System;
using System.Linq;
using Pathfinding;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private AIPath _aiPath;
    
    private UnitData _unitData;
    private PlayerData _playerData;
    private UnitType _enemyType;

    private UnitType EnemyType => _enemyType;

    private void OnEnable() => 
        CheckNewEnemy();

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
        
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        _aiPath = GetComponent<AIPath>();

        SetEnemyType(unitData);
        SetupView(playerData);
    }

    private void MoveToEnemyTower()
    {
        var enemyTower = FindEnemyTower();
        _aiPath.destination = enemyTower.transform.position;
    }

    private void CheckNewEnemy()
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

        Debug.Log($"{gameObject.name} enemy = {enemy}");
        Debug.DrawLine(transform.position, enemy.transform.position, Color.red);
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

    private void SetupView(PlayerData playerData) => 
        _meshRenderer.material.color = playerData.Material.color;

    public void DestroyUnit() => 
        Destroy(gameObject);
}