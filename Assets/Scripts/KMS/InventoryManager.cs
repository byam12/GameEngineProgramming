using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using TMPro;
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    [SerializeField] private ItemDataStorage itemDataStorage;
    [SerializeField] private GameObject equipMentSlot;
    [SerializeField] private GameObject inventory;
    [SerializeField] private Image selectedItemImage;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text mainStat;
    [SerializeField] private TMP_Text subStat1;
    [SerializeField] private TMP_Text subStat2;
    [SerializeField] private TMP_Text subStat3;
    [SerializeField] private TMP_Text subStat4;
    [SerializeField] private TMP_Text explanation;
    [SerializeField] private GameObject extractbutton;
    [SerializeField] private GameObject enhancementButton;
    [SerializeField] private GameObject equipButton;
    [SerializeField] private GameObject unEquipButton;
    [SerializeField] private GameObject itemInfoPanel;
    [SerializeField] private GameObject craftingPanel;
    [SerializeField] private Image selectedPartItem;
    [SerializeField] private GameObject selectedPartItemInfoPanel;
    [SerializeField] private Image weaponSlot;
    [SerializeField] private Image shieldSlot;
    [SerializeField] private Image amuletSlot1;
    [SerializeField] private Image amuletSlot2;
    [SerializeField] private Image amuletSlot3;
    [SerializeField] private Image amuletSlot4;
    [SerializeField] private Image amuletSlot5;
    [SerializeField] private Image amuletSlot6;
    [SerializeField] private Image craftingSetSlot1;
    [SerializeField] private Image craftingSetSlot2;
    [SerializeField] private Image craftingSetSlot3;
    [SerializeField] private Image craftingSetSlot4;
    [SerializeField] private Image craftingSetSlot5;
    [SerializeField] private Image craftingSetSlot6;
    public void SetEquipmentSlotActive(bool show)
    {
        equipMentSlot.SetActive(show);
    }
    public void SetInventoryActive(bool show)
    {
        inventory.SetActive(show);
    }
    public void SetItemInfoPanel(bool show)
    {
        itemInfoPanel.SetActive(show);
    }
    public void SetCraftingPanel(bool show)
    {
        craftingPanel.SetActive(show);
    }
    public bool GetEquipmentSlotActive()
    {
        return equipMentSlot.activeSelf;
    }
    public bool GetInventoryActive()
    {
        return inventory.activeSelf;
    }
    public void SetSelectedPartItemInfoPanel(bool show)
    {
        selectedPartItemInfoPanel.SetActive(show);
    }
    public void SetSelectedPartItemImage(Image sprite)
    {
        selectedPartItem.sprite = sprite.sprite;
    }
    public void SetEquipBtn(bool show)
    {
        equipButton.SetActive(show);
    }
    public void SetUnEquipBtn(bool show)
    {
        unEquipButton.SetActive(show);
    }
    public void SetExtractBtn(bool show)
    {
        extractbutton.SetActive(show);
    }
    public void SetEnhanceBtn(bool show)
    {
        enhancementButton.SetActive(show);
    }
    public void SetSelectedItem()
    {
        switch (GameManager.Instance.currentOpenedInventoryNumber)
        {
            case 0:
                AmuletData1 amulet1 = (AmuletData1)GameManager.Instance.currentSelectItem;
                selectedItemImage.sprite = Resources.Load<Sprite>("ItemIcons/" + amulet1.spriteCode.ToString("D4"));
                itemDataStorage.SetDataBySpriteCode(amulet1.spriteCode);
                explanation.text = itemDataStorage.explanation;
                itemName.text = amulet1.itemName + " Lv." + amulet1.enhancementLevel + "/15";
                mainStat.text = amulet1.statBonusTypes + " " + GetAttack(amulet1.enhancementLevel);
                subStat1.text = amulet1.subStat1Type + " " + CalculateSubStat(amulet1.subStat1Type);
                subStat2.text = amulet1.subStat2Type + " " + CalculateSubStat(amulet1.subStat2Type);
                subStat3.text = amulet1.subStat3Type + " " + CalculateSubStat(amulet1.subStat3Type);
                subStat4.text = amulet1.subStat4Type + " " + CalculateSubStat(amulet1.subStat4Type);
                break;
            case 1:
                AmuletData2 amulet2 = (AmuletData2)GameManager.Instance.currentSelectItem;
                selectedItemImage.sprite = Resources.Load<Sprite>("ItemIcons/" + amulet2.spriteCode.ToString("D4"));
                itemDataStorage.SetDataBySpriteCode(amulet2.spriteCode);
                explanation.text = itemDataStorage.explanation;
                itemName.text = amulet2.itemName + " Lv." + amulet2.enhancementLevel + "/15";
                mainStat.text = amulet2.statBonusTypes + " " + GetHP(amulet2.enhancementLevel);
                subStat1.text = amulet2.subStat1Type + " " + CalculateSubStat(amulet2.subStat1Type);
                subStat2.text = amulet2.subStat2Type + " " + CalculateSubStat(amulet2.subStat2Type);
                subStat3.text = amulet2.subStat3Type + " " + CalculateSubStat(amulet2.subStat3Type);
                subStat4.text = amulet2.subStat4Type + " " + CalculateSubStat(amulet2.subStat4Type);
                break;
            case 2:
                AmuletData3 amulet3 = (AmuletData3)GameManager.Instance.currentSelectItem;
                selectedItemImage.sprite = Resources.Load<Sprite>("ItemIcons/" + amulet3.spriteCode.ToString("D4"));
                itemDataStorage.SetDataBySpriteCode(amulet3.spriteCode);
                explanation.text = itemDataStorage.explanation;
                itemName.text = amulet3.itemName + " Lv." + amulet3.enhancementLevel + "/15";
                mainStat.text = amulet3.statBonusTypes + " " + GetDefense(amulet3.enhancementLevel);
                subStat1.text = amulet3.subStat1Type + " " + CalculateSubStat(amulet3.subStat1Type);
                subStat2.text = amulet3.subStat2Type + " " + CalculateSubStat(amulet3.subStat2Type);
                subStat3.text = amulet3.subStat3Type + " " + CalculateSubStat(amulet3.subStat3Type);
                subStat4.text = amulet3.subStat4Type + " " + CalculateSubStat(amulet3.subStat4Type);
                break;
            case 3:
                AmuletData4 amulet4 = (AmuletData4)GameManager.Instance.currentSelectItem;
                selectedItemImage.sprite = Resources.Load<Sprite>("ItemIcons/" + amulet4.spriteCode.ToString("D4"));
                itemDataStorage.SetDataBySpriteCode(amulet4.spriteCode);
                explanation.text = itemDataStorage.explanation;
                itemName.text = amulet4.itemName + " Lv." + amulet4.enhancementLevel + "/15";
                mainStat.text = amulet4.statBonusTypes + " " + CalculateMainStat(amulet4.statBonusTypes, amulet4.enhancementLevel).ToString("F1");
                subStat1.text = amulet4.subStat1Type + " " + CalculateSubStat(amulet4.subStat1Type);
                subStat2.text = amulet4.subStat2Type + " " + CalculateSubStat(amulet4.subStat2Type);
                subStat3.text = amulet4.subStat3Type + " " + CalculateSubStat(amulet4.subStat3Type);
                subStat4.text = amulet4.subStat4Type + " " + CalculateSubStat(amulet4.subStat4Type);
                break;
            case 4:
                AmuletData5 amulet5 = (AmuletData5)GameManager.Instance.currentSelectItem;
                selectedItemImage.sprite = Resources.Load<Sprite>("ItemIcons/" + amulet5.spriteCode.ToString("D4"));
                itemDataStorage.SetDataBySpriteCode(amulet5.spriteCode);
                explanation.text = itemDataStorage.explanation;
                itemName.text = amulet5.itemName + " Lv." + amulet5.enhancementLevel + "/15";
                mainStat.text = amulet5.statBonusTypes + " " + CalculateMainStat(amulet5.statBonusTypes, amulet5.enhancementLevel).ToString("F1");
                subStat1.text = amulet5.subStat1Type + " " + CalculateSubStat(amulet5.subStat1Type);
                subStat2.text = amulet5.subStat2Type + " " + CalculateSubStat(amulet5.subStat2Type);
                subStat3.text = amulet5.subStat3Type + " " + CalculateSubStat(amulet5.subStat3Type);
                subStat4.text = amulet5.subStat4Type + " " + CalculateSubStat(amulet5.subStat4Type);
                break;
            case 5:
                AmuletData6 amulet6 = (AmuletData6)GameManager.Instance.currentSelectItem;
                selectedItemImage.sprite = Resources.Load<Sprite>("ItemIcons/" + amulet6.spriteCode.ToString("D4"));
                itemDataStorage.SetDataBySpriteCode(amulet6.spriteCode);
                explanation.text = itemDataStorage.explanation;
                itemName.text = amulet6.itemName + " Lv." + amulet6.enhancementLevel + "/15";
                mainStat.text = amulet6.statBonusTypes + " " + CalculateMainStat(amulet6.statBonusTypes, amulet6.enhancementLevel).ToString("F1");
                subStat1.text = amulet6.subStat1Type + " " + CalculateSubStat(amulet6.subStat1Type);
                subStat2.text = amulet6.subStat2Type + " " + CalculateSubStat(amulet6.subStat2Type);
                subStat3.text = amulet6.subStat3Type + " " + CalculateSubStat(amulet6.subStat3Type);
                subStat4.text = amulet6.subStat4Type + " " + CalculateSubStat(amulet6.subStat4Type);
                break;
            case 6:
                WeaponData weapon = (WeaponData)GameManager.Instance.currentSelectItem;
                selectedItemImage.sprite = Resources.Load<Sprite>("ItemIcons/" + weapon.spriteCode.ToString("D4"));
                itemDataStorage.SetDataBySpriteCode(weapon.spriteCode);
                explanation.text = itemDataStorage.explanation;
                itemName.text = weapon.itemName + " " + weapon.itemRarity + " Lv." + weapon.enhancementLevel + "/15";
                mainStat.text = weapon.statBonusTypes + " " + CalculateMainStat(weapon.statBonusTypes, weapon.enhancementLevel).ToString("F1");
                subStat1.text = itemDataStorage.explanation;
                subStat2.text = "";
                subStat3.text = "";
                subStat4.text = "";

                break;
            case 7:
                ShieldData shield = (ShieldData)GameManager.Instance.currentSelectItem;
                selectedItemImage.sprite = Resources.Load<Sprite>("ItemIcons/" + shield.spriteCode.ToString("D4"));
                itemDataStorage.SetDataBySpriteCode(shield.spriteCode);
                explanation.text = itemDataStorage.explanation;
                itemName.text = shield.itemName + " " + shield.itemRarity + " Lv." + shield.enhancementLevel + "/15";
                mainStat.text = shield.statBonusType + " " + CalculateMainStat(shield.statBonusType, shield.enhancementLevel).ToString("F1"); ;
                subStat1.text = itemDataStorage.explanation;
                subStat2.text = "";
                subStat3.text = "";
                subStat4.text = "";
                break;
        }
    }
    public int GetHP(int level)
    {
        return CalculateStatInt(550, 2200, level);
    }

    // 공격력 계산
    public int GetAttack(int level)
    {
        return CalculateStatInt(79, 316, level);
    }

    // 방어력 계산
    public int GetDefense(int level)
    {
        return CalculateStatInt(46, 184, level);
    }

    // 공통 계산 함수
    private int CalculateStatInt(int min, int max, int level)
    {
        int steps = 15; //15단계
        float increment = (float)(max - min) / steps;
        float value = min + (increment * level);
        return Mathf.RoundToInt(value);
    }
    private float CalculateMainStat(string mainStat, int level)
    {
        float increment = 0;
        float min = 0;
        switch (mainStat)
        {
            case "치명타확률":
                increment = 1.2f;
                min = 6f;
                break;
            case "치명타피해":
                increment = 2.4f;
                min = 12f;
                break;
            case "공격력%":
                increment = 1.5f;
                min = 7.5f;
                break;
            case "체력%":
                increment = 1.5f;
                min = 7.5f;
                break;
            case "방어력%":
                increment = 2.4f;
                min = 12f;
                break;
            case "불속성피해":
                increment = 1.5f;
                min = 7.5f;
                break;
            case "물속성피해":
                increment = 1.5f;
                min = 7.5f;
                break;
            case "전기속성피해":
                increment = 1.5f;
                min = 7.5f;
                break;
            case "관통%":
                increment = 1.2f;
                min = 1.2f;
                break;
            case "MP자동회복":
                increment = 3f;
                min = 15f;
                break;
            default:
                Debug.Log("주옵 예외발생");
                break;
        }
        float value = min + (increment * level);
        return value;
    }
    private string CalculateSubStat(string substat)
    {
        int level = 0;
        string percent = "%";
        if (substat.Contains("+"))
        {
            string[] parts = substat.Split('+');
            if (parts.Length == 2)
            {
                string left = parts[0];
                string right = parts[1];
                substat = left;
                level = int.Parse(right);
                //Debug.Log("Left: " + left + ", Right: " + right);
            }
            else
            {
                //Debug.LogWarning("문자열이 올바른 형식이 아닙니다.");
                level = 0;
            }
        }

        float increment = 0;
        float min = 0;
        switch (substat)
        {
            case "치명타확률":
                increment = 2.4f;
                min = 2.4f;
                break;
            case "치명타피해":
                increment = 4.8f;
                min = 4.8f;
                break;
            case "공격력%":
                increment = 3f;
                min = 3f;
                break;
            case "체력%":
                increment = 3f;
                min = 3f;
                break;
            case "방어력%":
                increment = 4.8f;
                min = 4.8f;
                break;
            case "공격력":
                increment = 19f;
                min = 19f;
                percent = "";
                break;
            case "체력":
                increment = 112f;
                min = 112f;
                percent = "";
                break;
            case "방어력":
                increment = 15f;
                min = 15f;
                percent = "";
                break;
            case "관통":
                increment = 9f;
                min = 9f;
                percent = "";
                break;
            default:
                Debug.Log("부부옵 예외발생");
                break;
        }
        float value = min + (increment * level);
        return value + "" + percent;
    }
    public void UpdateUIWearingEquipMents() // 착용중인 장비 장비칸에 띄우기
    {
        EquipmentSlotData equipmentSlotData = DataManager.Instance.GetEquipmentSlotData();
        EquipmentSlotSpriteUpdate(0);
        EquipmentSlotSpriteUpdate(1);
        EquipmentSlotSpriteUpdate(2);
        EquipmentSlotSpriteUpdate(3);
        EquipmentSlotSpriteUpdate(4);
        EquipmentSlotSpriteUpdate(5);
        EquipmentSlotSpriteUpdate(6);
        EquipmentSlotSpriteUpdate(7);
    }
    public void EquipmentSlotSpriteUpdate(int slotNumber)
    {
        EquipmentSlotData equipmentSlotData = DataManager.Instance.GetEquipmentSlotData();
        switch (slotNumber)
        {
            case 0:
                if (equipmentSlotData.amulet1 == null) amuletSlot1.sprite = Resources.Load<Sprite>("ItemIcons/0000");
                else amuletSlot1.sprite = Resources.Load<Sprite>("ItemIcons/" + equipmentSlotData.amulet1.spriteCode.ToString("D4"));
                break;
            case 1:
                if (equipmentSlotData.amulet2 == null) amuletSlot2.sprite = Resources.Load<Sprite>("ItemIcons/0000");
                else amuletSlot2.sprite = Resources.Load<Sprite>("ItemIcons/" + equipmentSlotData.amulet2.spriteCode.ToString("D4"));
                break;
            case 2:
                if (equipmentSlotData.amulet3 == null) amuletSlot3.sprite = Resources.Load<Sprite>("ItemIcons/0000");
                else amuletSlot3.sprite = Resources.Load<Sprite>("ItemIcons/" + equipmentSlotData.amulet3.spriteCode.ToString("D4"));
                break;
            case 3:
                if (equipmentSlotData.amulet4 == null) amuletSlot4.sprite = Resources.Load<Sprite>("ItemIcons/0000");
                else amuletSlot4.sprite = Resources.Load<Sprite>("ItemIcons/" + equipmentSlotData.amulet4.spriteCode.ToString("D4"));
                break;
            case 4:
                if (equipmentSlotData.amulet5 == null) amuletSlot5.sprite = Resources.Load<Sprite>("ItemIcons/0000");
                else amuletSlot5.sprite = Resources.Load<Sprite>("ItemIcons/" + equipmentSlotData.amulet5.spriteCode.ToString("D4"));
                break;
            case 5:
                if (equipmentSlotData.amulet6 == null) amuletSlot6.sprite = Resources.Load<Sprite>("ItemIcons/0000");
                else amuletSlot6.sprite = Resources.Load<Sprite>("ItemIcons/" + equipmentSlotData.amulet6.spriteCode.ToString("D4"));
                break;
            case 6:
                if (equipmentSlotData.weapon == null) weaponSlot.sprite = Resources.Load<Sprite>("ItemIcons/0000");
                else weaponSlot.sprite = Resources.Load<Sprite>("ItemIcons/" + equipmentSlotData.weapon.spriteCode.ToString("D4"));
                break;
            case 7:
                if (equipmentSlotData.shield == null) shieldSlot.sprite = Resources.Load<Sprite>("ItemIcons/0000");
                else shieldSlot.sprite = Resources.Load<Sprite>("ItemIcons/" + equipmentSlotData.shield.spriteCode.ToString("D4"));
                break;

        }
    }
    public Sprite GetSpriteBySpriteCode(int spriteCode)
    {
        return Resources.Load<Sprite>("ItemIcons/" + spriteCode.ToString("D4"));
    }
    public void SelectAmuletSetInCrafting(int setNumber)
    {
        craftingSetSlot1.sprite = Resources.Load<Sprite>("ItemIcons/" + (1000 + setNumber).ToString("D4"));
        craftingSetSlot2.sprite = Resources.Load<Sprite>("ItemIcons/" + (2000 + setNumber).ToString("D4"));
        craftingSetSlot3.sprite = Resources.Load<Sprite>("ItemIcons/" + (3000 + setNumber).ToString("D4"));
        craftingSetSlot4.sprite = Resources.Load<Sprite>("ItemIcons/" + (4000 + setNumber).ToString("D4"));
        craftingSetSlot5.sprite = Resources.Load<Sprite>("ItemIcons/" + (5000 + setNumber).ToString("D4"));
        craftingSetSlot6.sprite = Resources.Load<Sprite>("ItemIcons/" + (6000 + setNumber).ToString("D4"));
    }
    public void CraftSelectedItem(Image selectiedItem)
    {
        int spriteCode = int.Parse(selectiedItem.sprite.name);
        int partNumber = int.Parse(selectiedItem.sprite.name.Substring(0, 1));
        switch (partNumber - 1)
        {
            case 0:
                CreateEquipment1(spriteCode);
                break;
            case 1:
                CreateEquipment2(spriteCode);
                break;
            case 2:
                CreateEquipment3(spriteCode);
                break;
            case 3:
                CreateEquipment4(spriteCode);
                break;
            case 4:
                CreateEquipment5(spriteCode);
                break;
            case 5:
                CreateEquipment6(spriteCode);
                break;
        }
    }
    private void CreateEquipment1(int spriteCode)
    {
        itemDataStorage.SetDataBySpriteCode(spriteCode);
        string mainOption = itemDataStorage.statBonusType;
        List<int> numbers = GetSubOptions(mainOption);
        DataManager.Instance.AddAmulet1(
            new AmuletData1(spriteCode, itemDataStorage.itemName, 0, mainOption,
        GetSubStatType(numbers[0]), GetSubStatType(numbers[1]), GetSubStatType(numbers[2]), GetSubStatType(numbers[3])));
    }
    private void CreateEquipment2(int spriteCode)
    {
        itemDataStorage.SetDataBySpriteCode(spriteCode);
        string mainOption = itemDataStorage.statBonusType;
        List<int> numbers = GetSubOptions(mainOption);

        DataManager.Instance.AddAmulet2(
            new AmuletData2(spriteCode, itemDataStorage.itemName, 0, mainOption,
        GetSubStatType(numbers[0]), GetSubStatType(numbers[1]), GetSubStatType(numbers[2]), GetSubStatType(numbers[3])));
    }

    private void CreateEquipment3(int spriteCode)
    {
        itemDataStorage.SetDataBySpriteCode(spriteCode);
        string mainOption = itemDataStorage.statBonusType;
        List<int> numbers = GetSubOptions(mainOption);
        DataManager.Instance.AddAmulet3(
            new AmuletData3(spriteCode, itemDataStorage.itemName, 0, mainOption,
        GetSubStatType(numbers[0]), GetSubStatType(numbers[1]), GetSubStatType(numbers[2]), GetSubStatType(numbers[3])));
    }

    private void CreateEquipment4(int spriteCode)
    {
        itemDataStorage.SetDataBySpriteCode(spriteCode);
        string mainOption = GetMainStatTypeInAmulet4();
        List<int> numbers = GetSubOptions(mainOption);

        DataManager.Instance.AddAmulet4(
            new AmuletData4(spriteCode, itemDataStorage.itemName, 0, mainOption,
        GetSubStatType(numbers[0]), GetSubStatType(numbers[1]), GetSubStatType(numbers[2]), GetSubStatType(numbers[3])));
    }

    private void CreateEquipment5(int spriteCode)
    {
        itemDataStorage.SetDataBySpriteCode(spriteCode);
        string mainOption = GetMainStatTypeInAmulet5();
        List<int> numbers = GetSubOptions(mainOption);
        DataManager.Instance.AddAmulet5(
            new AmuletData5(spriteCode, itemDataStorage.itemName, 0, mainOption,
        GetSubStatType(numbers[0]), GetSubStatType(numbers[1]), GetSubStatType(numbers[2]), GetSubStatType(numbers[3])));
    }

    private void CreateEquipment6(int spriteCode)
    {
        itemDataStorage.SetDataBySpriteCode(spriteCode);
        string mainOption = GetMainStatTypeInAmulet6();
        List<int> numbers = GetSubOptions(mainOption);
        DataManager.Instance.AddAmulet6(
            new AmuletData6(spriteCode, itemDataStorage.itemName, 0, mainOption,
        GetSubStatType(numbers[0]), GetSubStatType(numbers[1]), GetSubStatType(numbers[2]), GetSubStatType(numbers[3])));
    }
    private string GetSubStatType(int num)
    {
        switch (num)
        {
            case 1:
                return "공격력";
            case 2:
                return "방어력";
            case 3:
                return "체력";
            case 4:
                return "공격력%";
            case 5:
                return "방어력%";
            case 6:
                return "체력%";
            case 7:
                return "치명타확률";
            case 8:
                return "치명타피해";
            case 9:
                return "관통";
            default:
                Debug.Log("오류");
                return "";
        }
    }
    public List<int> GetSubOptions(string mainStatType)
    {
        List<int> candidates = new List<int>();

        for (int i = 1; i <= 9; i++)
        {
            if (GetSubStatType(i) != mainStatType)
            {
                candidates.Add(i);
            }
        }

        if (candidates.Count < 4)
        {
            Debug.LogError("조건을 만족하는 서브옵션이 4개 미만입니다.");
            return new List<int>();
        }

        // Fisher–Yates shuffle
        for (int i = candidates.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            int temp = candidates[i];
            candidates[i] = candidates[j];
            candidates[j] = temp;
        }

        return candidates.GetRange(0, 4);
    }
    private string GetMainStatTypeInAmulet4()
    {
        int random = UnityEngine.Random.Range(1, 6);//1~5
        switch (random)
        {
            case 1:
                return "체력%";
            case 2:
                return "방어력%";
            case 3:
                return "공격력%";
            case 4:
                return "치명타확률";
            case 5:
                return "치명타피해";
            default:
                Debug.Log("오류");
                return "";
        }
    }
    private string GetMainStatTypeInAmulet5()
    {
        int random = UnityEngine.Random.Range(1, 8);//1~7
        switch (random)
        {
            case 1:
                return "체력%";
            case 2:
                return "방어력%";
            case 3:
                return "공격력%";
            case 4:
                return "관통%";
            case 5:
                return "불속성피해";
            case 6:
                return "물속성피해";
            case 7:
                return "전기속성피해";
            default:
                Debug.Log("오류");
                return "";
        }
    }
    private string GetMainStatTypeInAmulet6()
    {
        int random = UnityEngine.Random.Range(1, 5);//1~4
        switch (random)
        {
            case 1:
                return "체력%";
            case 2:
                return "방어력%";
            case 3:
                return "공격력%";
            case 4:
                return "MP자동회복";
            default:
                Debug.Log("오류");
                return "";
        }
    }
    
    
}
