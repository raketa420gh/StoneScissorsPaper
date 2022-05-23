using System;
using TMPro;
using UnityEngine;

public class Tower : UnitBase
{
    [SerializeField] private int _armor;
    [SerializeField] private TMP_Text _tmpArmor;
    private int _currentArmor;

    public override void Initialize(PlayerData playerData, UnitData unitData = null)
    {
        base.Initialize(playerData, unitData);
        ResetArmor();
        SetupView(playerData);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var unit = collision.gameObject.GetComponent<Unit>();

        if (unit)
            if (unit.PlayerType != PlayerType)
            {
                unit.DestroyUnit();
                ChangeArmor(-1);
            }
    }

    private void UpdateArmorView() =>
        _tmpArmor.text = _currentArmor.ToString();

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