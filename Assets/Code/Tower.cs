using TMPro;
using UnityEngine;

public class Tower : Unit
{
    [SerializeField] private int _armor;
    [SerializeField] private TMP_Text _tmpArmor;
    private int _currentArmor;

    private void Start() => 
        ResetArmor();

    private void OnTriggerEnter(Collider other)
    {
        var unit = other.GetComponent<Unit>();

        if (unit)
        {
            unit.DestroyUnit();
            ChangeArmor(-1);
        }
    }

    private void ResetArmor()
    {
        _currentArmor = _armor;
        UpdateArmorView();
    }

    private void UpdateArmorView() => 
        _tmpArmor.text = _currentArmor.ToString();

    private void ChangeArmor(int amount)
    {
        _currentArmor += amount;
        
        if (_currentArmor <= 0)
            ResetArmor();
        
        UpdateArmorView();
    }
}
