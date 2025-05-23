using UnityEngine;
[System.Serializable]
public class WeaponData
{
    public int itemNumber;
    public int spriteCode;
    public string itemName;
    public int itemRarity;
    public int enhancementLevel;
    public string statBonusTypes;
    public WeaponData(int itemNumber, int spriteCode,
    string itemName, int itemRarity,
    int enhancementLevel, string statBonusTypes)
    {
        this.itemNumber = itemNumber;
        this.spriteCode = spriteCode;
        this.itemName = itemName;
        this.itemRarity = itemRarity;
        this.enhancementLevel = enhancementLevel;
        this.statBonusTypes = statBonusTypes;
    }
}
