using System.Collections.Generic;
using TMPro;
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
    private UnitsCounter _unitsCounter;

    public PlayerData PlayerData => _playerData;

    [Inject]
    public void Construct(GameFactory factory, UnitsCounter unitsCounter)
    {
        _factory = factory;
        _unitsCounter = unitsCounter;
    }

    public void Initialize()
    {
        foreach (var uiButtonPointer in _uiButtonPointers)
        {
            uiButtonPointer.Initialize();
            uiButtonPointer.OnPointerDragEnd += OnPointerDragEnd;
        }
        
        _creationZone.Initialize(_playerData);
        _unitsCounter.AddTower(_tower);
        _tower.Initialize(_playerData);
    }

    public void Activate() => 
        _uiPanel.SetActive(true);

    public void Deactivate() => 
        _uiPanel.SetActive(false);

    public void HideAllPointers()
    {
        foreach (var uiButtonPointer in _uiButtonPointers)
            uiButtonPointer.HidePointer();
    }

    private void TryToCreateUnit(UnitData unitData, Pointer pointer, Vector3 position)
    {
        var offset = 1f;
        var positionWithOffset = new Vector3(position.x, position.y + offset, position.z);

        if (pointer.CanCreate)
        {
            if (_playerData.Type == PlayerType.Player2)
            {
                var unit = CreateUnit(unitData, positionWithOffset);
                unit.transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            if (_playerData.Type == PlayerType.Player1)
            {
                var unit = CreateUnit(unitData, positionWithOffset);
            }
        }
    }

    private Unit CreateUnit(UnitData unitData, Vector3 positionWithOffset)
    {
        var unit = _factory.CreateUnit(positionWithOffset, unitData.AssetPath);
        unit.Initialize(_playerData, unitData);
        _unitsCounter.AddUnit(unit);
        return unit;
    }

    private void OnPointerDragEnd(UnitData unitData, Pointer pointer, Vector3 position) => 
        TryToCreateUnit(unitData, pointer, position);
}