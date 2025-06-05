using UnityEngine;

public class ItemDataStorage : MonoBehaviour
{
    public string itemName;
    public int itemRarity;
    public int enhancementLevel;
    public string statBonusType;

    public void SetDataBySpriteCode(int spriteCode)
    {
        switch (spriteCode)
        {
            case 1000:
                itemName = "모자0";
                statBonusType = "공격력";
                break;
            case 1001:
                itemName = "모자0";
                statBonusType = "공격력";
                break;

        }
    }
}
