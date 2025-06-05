using UnityEngine;
[System.Serializable]
public class AmuletData2
{
    public int spriteCode;
    public string itemName;
    public int enhancementLevel;
    public string statBonusTypes;
    public string subStat1Type;
    public string subStat2Type;
    public string subStat3Type;
    public string subStat4Type;
    public AmuletData2(int spriteCode,
    string itemName, int enhancementLevel, string statBonusTypes,
    string subStat1Type, string subStat2Type, string subStat3Type, string subStat4Type)
    {
        this.spriteCode = spriteCode;
        this.itemName = itemName;
        this.enhancementLevel = enhancementLevel;
        this.statBonusTypes = statBonusTypes;
        this.subStat1Type = subStat1Type;
        this.subStat2Type = subStat2Type;
        this.subStat3Type = subStat3Type;
        this.subStat4Type = subStat4Type;
    }
}
