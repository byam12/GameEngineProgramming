using UnityEngine;
[System.Serializable]
public class AmuletData6
{
    public int spriteCode;
    public string itemName;
    public int itemRarity;
    public int enhancementLevel;
    public string statBonusTypes;
    public AmuletData6(int spriteCode,
    string itemName, int itemRarity,
    int enhancementLevel, string statBonusTypes)
    {
        this.spriteCode = spriteCode;
        this.itemName = itemName;
        this.itemRarity = itemRarity;
        this.enhancementLevel = enhancementLevel;
        this.statBonusTypes = statBonusTypes;
    }
}
