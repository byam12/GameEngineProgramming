using UnityEngine;
[System.Serializable]
public class AmuletData3
{
    public int spriteCode;
    public string itemName;
    public int itemRarity;
    public int enhancementLevel;
    public AmuletData3(int spriteCode,
    string itemName, int itemRarity,
    int enhancementLevel)
    {
        this.spriteCode = spriteCode;
        this.itemName = itemName;
        this.itemRarity = itemRarity;
        this.enhancementLevel = enhancementLevel;
    }
}
