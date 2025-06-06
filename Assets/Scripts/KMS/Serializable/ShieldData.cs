using UnityEngine;
[System.Serializable]
public class ShieldData
{
    public int spriteCode;
    public string itemName;
    public string itemRarity;
    public int enhancementLevel;
    public string statBonusType;
    public bool isEquiped = false;
    public ShieldData(int spriteCode,
    string itemName, string statBonusType, string itemRarity,
    int enhancementLevel)
    {
        this.spriteCode = spriteCode;
        this.itemName = itemName;
        this.statBonusType = statBonusType;
        this.itemRarity = itemRarity;
        this.enhancementLevel = enhancementLevel;
    }
}
