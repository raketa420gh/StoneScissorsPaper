using UnityEngine;

[CreateAssetMenu(menuName = "PlayerData", fileName = "PlayerData", order = 53)]
public class PlayerData : ScriptableObject
{
    public PlayerType Type;
    public Material Material;
}