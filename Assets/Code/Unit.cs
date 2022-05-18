using System.Linq;
using Pathfinding;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private AIPath _aiPath;
    private PlayerData _playerData;

    public PlayerType PlayerType => _playerData.Type;

    public void Initialize(PlayerData playerData)
    {
        _playerData = playerData;
        
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        _aiPath = GetComponent<AIPath>();
        
        SetupView(playerData);
    }

    public void MoveToEnemyTower()
    {
        var enemyTower = FindEnemyTower();
        _aiPath.destination = enemyTower.transform.position;
    }

    private Tower FindEnemyTower()
    {
        Tower enemyTower;
        var allTowers = FindObjectsOfType<Tower>();

        return allTowers.FirstOrDefault(tower => tower._playerData.Type != _playerData.Type);
    }

    private void SetupView(PlayerData playerData) => 
        _meshRenderer.material.color = playerData.Material.color;

    public void DestroyUnit() => 
        Destroy(gameObject);
}
