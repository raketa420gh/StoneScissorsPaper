using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

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

    public override async void DestroyUnit()
    {
        CreateVFX();

        var boxCollider = GetComponent<BoxCollider>();
        Destroy(boxCollider);
        Destroy(_tmpArmor);
        ExplodeTower();
        await UniTask.Delay(TimeSpan.FromSeconds(2));
        base.DestroyUnit();
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

    private void ExplodeTower()
    {
        var rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach (var rb in rigidbodies)
        {
            var randomOffset = Random.Range(-0.25f, 0.25f);
            var randomVector = new Vector3(0 + randomOffset, 1 + randomOffset, 0);
            rb.isKinematic = false;
            rb.AddForce(randomVector * 25, ForceMode.Impulse);
        }
    }
}