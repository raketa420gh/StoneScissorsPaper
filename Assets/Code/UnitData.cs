using UnityEngine;

[CreateAssetMenu(menuName = "Units/Unit", fileName = "Unit", order = 52)]
public class UnitData : ScriptableObject
{
    public string Title;
    public Sprite Icon;
    public string AssetPath;
    public UnitType Type;
}