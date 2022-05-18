using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _uiPanel;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private Tower _tower;
    [SerializeField] private CreationZone _creationZone;
    [SerializeField] private List<UIButtonPointer> _uiButtonPointers = new List<UIButtonPointer>();
    private GameFactory _factory;

    public PlayerData PlayerData => _playerData;

    [Inject]
    public void Construct(GameFactory factory) => 
        _factory = factory;

    public void Initialize()
    {
        foreach (var uiButtonPointer in _uiButtonPointers)
        {
            uiButtonPointer.Initialize(_playerData);
            uiButtonPointer.OnPointerDragEnd += OnPointerDragEnd;
        }
        
        _creationZone.Initialize(_playerData);
        _tower.Initialize(_playerData);
    }

    public void Activate() => 
        _uiPanel.SetActive(true);

    public void Deactivate() => 
        _uiPanel.SetActive(false);

    private void OnPointerDragEnd(string path, Pointer pointer, Vector3 position)
    {
        var offset = 1f;
        var positionWithOffset = new Vector3(position.x, position.y + offset, position.z);

        if (pointer.CanCreate)
        {
            if (_playerData.Type == PlayerType.Player2)
            {
                var enemy = _factory.CreateUnit(positionWithOffset, path);
                enemy.transform.rotation = Quaternion.Euler(0, 180, 0);
                enemy.Initialize(_playerData);
                enemy.MoveToEnemyTower();
            }
            
            if (_playerData.Type == PlayerType.Player1)
            {
                var enemy = _factory.CreateUnit(positionWithOffset, path);
                enemy.Initialize(_playerData);
                enemy.MoveToEnemyTower();
            }
        }
    }
}