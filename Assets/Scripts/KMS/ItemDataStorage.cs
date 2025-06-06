using UnityEngine;

public class ItemDataStorage : MonoBehaviour
{
    public string itemName;
    public int itemRarity;
    public int enhancementLevel;
    public string statBonusType;
    public string explanation;


    private string set0explanation =
@$"0번세트
2셋 효과 : 미정
4셋  효과: 미정";
    private string set1explanation =
@$"1번세트
2셋 효과 : 미정
4셋  효과: 미정";


    public void SetDataBySpriteCode(int spriteCode)
    {
        switch (spriteCode)
        {
            case 1000:
                itemName = "투구";
                statBonusType = "공격력";
                explanation = set0explanation;
                break;
            case 2000:
                itemName = "갑옷";
                statBonusType = "체력";
                explanation = set0explanation;
                break;
            case 3000:
                itemName = "다리";
                statBonusType = "방어력";
                explanation = set0explanation;
                break;
            case 4000:
                itemName = "신발";
                explanation = set0explanation;
                break;
            case 5000    :
                itemName = "결정";
                explanation = set0explanation;
                break;
            case 6000:
                itemName = "유물";
                explanation = set0explanation;
                break;


            case 1001:
                itemName = "투구";
                statBonusType = "공격력";
                explanation = set1explanation;
                break;
            case 2001:
                itemName = "갑옷";
                statBonusType = "체력";
                explanation = set1explanation;
                break;
            case 3001:
                itemName = "다리";
                statBonusType = "방어력";
                explanation = set1explanation;
                break;
            case 4001:
                itemName = "신발";
                explanation = set1explanation;
                break;
            case 5001:
                itemName = "결정";
                explanation = set1explanation;
                break;
            case 6001:
                itemName = "유물";
                explanation = set1explanation;
                break;
            
        }
    }
}
