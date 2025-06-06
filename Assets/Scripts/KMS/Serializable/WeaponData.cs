using UnityEngine;
[System.Serializable]
public class WeaponData
{
    public int spriteCode;
    public string itemName;
    public string itemRarity;
    public int enhancementLevel;
    public string statBonusTypes;
    public bool isEquiped = false;

    public WeaponData(int spriteCode,
    string itemName, string itemRarity,
    int enhancementLevel, string statBonusTypes)
    {
        this.spriteCode = spriteCode;
        this.itemName = itemName;
        this.itemRarity = itemRarity;
        this.enhancementLevel = enhancementLevel;
        this.statBonusTypes = statBonusTypes;
    }
}
