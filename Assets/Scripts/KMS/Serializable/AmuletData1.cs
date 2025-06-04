using UnityEngine;
[System.Serializable]
public class AmuletData1
{
    public int spriteCode;
    public string itemName;
    public int itemRarity;
    public int enhancementLevel;
    public AmuletData1(int spriteCode,
    string itemName, int itemRarity,
    int enhancementLevel)
    {
        this.spriteCode = spriteCode;
        this.itemName = itemName;
        this.itemRarity = itemRarity;
        this.enhancementLevel = enhancementLevel;
    }
}
