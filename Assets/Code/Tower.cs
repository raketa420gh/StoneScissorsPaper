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

    public override void DestroyUnit()
    {
        //base.DestroyUnit();

        var rigidbodies = GetComponentsInChildren<Rigidbody>();
        var randomOffset = Random.Range(-0.5f, 0.5f);
        var randomVector = new Vector3(0, 1 + randomOffset, 0);
        
        foreach (var rb in rigidbodies)
        {
            rb.isKinematic = false;
            rb.AddForce(randomVector * 25, ForceMode.Impulse);
        }
        
    }
}