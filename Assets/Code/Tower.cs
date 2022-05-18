using TMPro;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private int _armor;
    [SerializeField] private TMP_Text _tmpArmor;
    private int _currentArmor;
    private MeshRenderer _meshRenderer;
    
    public PlayerType PlayerType;
    
    public void Initialize(PlayerData playerData)
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        PlayerType = playerData.Type;
        SetupView(playerData);
        ResetArmor();
    }
    
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
    
    private void SetupView(PlayerData playerData) => 
        _meshRenderer.material.color = playerData.Material.color;

    private void ChangeArmor(int amount)
    {
        _currentArmor += amount;
        
        if (_currentArmor <= 0)
            ResetArmor();
        
        UpdateArmorView();
    }
}
