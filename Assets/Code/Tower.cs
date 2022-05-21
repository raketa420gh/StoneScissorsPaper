using TMPro;
using UnityEngine;

public class Tower : Unit
{
    [SerializeField] private int _armor;
    [SerializeField] private TMP_Text _tmpArmor;
    private int _currentArmor;
    private MeshRenderer _meshRenderer;
    private PlayerData _playerData;
    private PlayerType _playerType;

    public PlayerType PlayerType => _playerType;

    public void Initialize(PlayerData playerData)
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        _playerData = playerData;
        _playerType = playerData.Type;
        SetupView(playerData);
        ResetArmor();
    }

    private void OnTriggerEnter(Collider other)
    {
        var unit = other.GetComponent<Unit>();

        if (unit)
            if (unit.PlayerType != _playerType)
            {
                unit.DestroyUnit();
                ChangeArmor(-1);
            }
    }

    private void UpdateArmorView() =>
        _tmpArmor.text = _currentArmor.ToString();

    private void SetupView(PlayerData playerData) =>
        _meshRenderer.material.color = playerData.Material.color;

    private void ResetArmor()
    {
        _currentArmor = _armor;
        UpdateArmorView();
    }

    private void ChangeArmor(int amount)
    {
        _currentArmor += amount;

        if (_currentArmor <= 0)
            DestroyUnit();

        UpdateArmorView();
        ActivateBounceAnimation();
    }
}