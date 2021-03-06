using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    public event Action OnLose;

    [SerializeField] private GameObject _uiPanel;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private Tower _tower;
    [SerializeField] private CreationZone _creationZone;
    [SerializeField] private List<UIButtonPointer> _uiButtonPointers = new List<UIButtonPointer>();
    private GameFactory _factory;
    private SoundPlayer _soundPlayer;
    private UnitsCounter _unitsCounter;

    public PlayerData PlayerData => _playerData;

    [Inject]
    public void Construct(GameFactory factory, SoundPlayer soundPlayer, UnitsCounter unitsCounter)
    {
        _factory = factory;
        _soundPlayer = soundPlayer;
        _unitsCounter = unitsCounter;
    }

    public void Initialize()
    {
        foreach (var uiButtonPointer in _uiButtonPointers)
        {
            uiButtonPointer.Initialize();
            uiButtonPointer.OnPointerDragEnd += OnPointerDragEnd;
        }

        _tower.OnDestroy += OnDestroyTower;
        _tower.OnExplode += OnExplodeTower;

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

        _creationZone.Hide();
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
                CreateUnit(unitData, positionWithOffset);
        }
    }

    private Unit CreateUnit(UnitData unitData, Vector3 positionWithOffset)
    {
        var unit = _factory.CreateUnit(positionWithOffset, unitData.AssetPath);
        unit.Initialize(_playerData, unitData);
        unit.ActivateBounceAnimation();
        _unitsCounter.AddUnit(unit);
        return unit;
    }

    private void OnExplodeTower() => 
        _soundPlayer.CreateSfxTowerExplode(_tower.transform.position);

    private void OnPointerDragEnd(UnitData unitData, Pointer pointer, Vector3 position)
    {
        TryToCreateUnit(unitData, pointer, position);
        _soundPlayer.CreateSfxUnitCreation(pointer.gameObject.transform.position);
        _creationZone.Hide();
    }

    private void OnDestroyTower(UnitBase unitBase)
    {
        _soundPlayer.CreateSfxWin(gameObject.transform.position);
        OnLose?.Invoke();
    }
}