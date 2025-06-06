using System;
using TMPro;
using UnityEngine;

public class PlayerParametersDefault : MonoBehaviour
{
    public static PlayerParametersDefault Instance { get; private set; }
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
    private int basicAtk;
    private int basicHP;
    private int basicDef;
    public int atk;//최종공
    public int HP;//최종체
    public int def;//최종방
    public int pen;//깡관
    public float critRate;
    public float critDmg;
    public float PenRate;
    public float fireDmg;
    public float waterDmg;
    public float electricDmg;
    public float mpAutoRegeneration;
    private float mpAutoRegenerationBonus;
    private float atkPerBonus;
    private int atkBonus;
    private float HPPerBonus;
    private int HPBonus;
    private float DefPerBonus;
    private int DefBonus;
    private float critRateBonus;
    private float critDmgBonus;
    private float fireDmgBonus;
    private float waterDmgBonus;
    private float electricDmgRateBonus;
    private float PenRateBonus;
    [SerializeField] private TMP_Text text1;
    [SerializeField] private TMP_Text text2;
    [SerializeField] private TMP_Text text3;
    [SerializeField] private TMP_Text text4;
    [SerializeField] private TMP_Text text5;
    [SerializeField] private TMP_Text text6;
    [SerializeField] private TMP_Text text7;
    [SerializeField] private TMP_Text text8;
    [SerializeField] private TMP_Text text9;
    [SerializeField] private TMP_Text text10;
    [SerializeField] private TMP_Text text11;
    void Start()
    {
        UpdatePayerDefaultParameters();
    }
    private void ResetData()
    {
        basicAtk = 938;
        basicHP = 4314;
        basicDef = 340;
        critRate = 19.4f;
        critDmg = 50f;
        PenRate = 0;
        fireDmg = 0;
        waterDmg = 0;
        electricDmg = 0;
        mpAutoRegeneration = 0;
        mpAutoRegenerationBonus = 0;
        atkPerBonus = 0;
        atkBonus = 0;
        HPPerBonus = 0;
        HPBonus = 0;
        DefPerBonus = 0;
        DefBonus = 0;
        pen = 0;
        critRateBonus = 0;
        critDmgBonus = 0;
        fireDmgBonus = 0;
        waterDmgBonus = 0;
        electricDmgRateBonus = 0;
        PenRateBonus = 0;
    }
    public void UpdatePayerDefaultParameters()
    {
        try
        {
            // 널 참조가 발생할 수 있는 코드


            ResetData();
            //공격력부터
            if (DataManager.Instance.GetEquipmentSlotData().weapon == null)
            {
                basicAtk = 938;
            }
            else if (DataManager.Instance.GetEquipmentSlotData().weapon.itemRarity == "S")
            {
                basicAtk = 938 + 48 + (int)(DataManager.Instance.GetEquipmentSlotData().weapon.enhancementLevel * 11.084f);
            }
            else if (DataManager.Instance.GetEquipmentSlotData().weapon.itemRarity == "A")
            {
                basicAtk = 938 + 42 + (int)(DataManager.Instance.GetEquipmentSlotData().weapon.enhancementLevel * 9.7f);
            }
            //체력, 방어력
            if (DataManager.Instance.GetEquipmentSlotData().shield == null)
            {
                basicHP = 4314;
                basicDef = 340;
            }
            else if (DataManager.Instance.GetEquipmentSlotData().shield.itemRarity == "S")
            {
                basicHP = 4314 + (int)(DataManager.Instance.GetEquipmentSlotData().shield.enhancementLevel * 55.984f);
                basicDef = 340 + (int)(DataManager.Instance.GetEquipmentSlotData().shield.enhancementLevel * 4.434f);
            }
            else if (DataManager.Instance.GetEquipmentSlotData().shield.itemRarity == "A")
            {
                basicHP = 4314 + (int)(DataManager.Instance.GetEquipmentSlotData().shield.enhancementLevel * 44.7872f);
                basicDef = 340 + (int)(DataManager.Instance.GetEquipmentSlotData().shield.enhancementLevel * 3.9906f);
            }
            //장신구
            if (DataManager.Instance.GetEquipmentSlotData().amulet1 != null && DataManager.Instance.GetEquipmentSlotData().amulet1.spriteCode != 0)
            {
                atkBonus += InventoryManager.Instance.GetAttack(DataManager.Instance.GetEquipmentSlotData().amulet1.enhancementLevel);
                SubStatSumCalculater(DataManager.Instance.GetEquipmentSlotData().amulet1.subStat1Type);
                SubStatSumCalculater(DataManager.Instance.GetEquipmentSlotData().amulet1.subStat2Type);
                SubStatSumCalculater(DataManager.Instance.GetEquipmentSlotData().amulet1.subStat3Type);
                SubStatSumCalculater(DataManager.Instance.GetEquipmentSlotData().amulet1.subStat4Type);
            }
            if (DataManager.Instance.GetEquipmentSlotData().amulet2 != null && DataManager.Instance.GetEquipmentSlotData().amulet2.spriteCode != 0)
            {
                HPBonus += InventoryManager.Instance.GetHP(DataManager.Instance.GetEquipmentSlotData().amulet2.enhancementLevel);
                SubStatSumCalculater(DataManager.Instance.GetEquipmentSlotData().amulet2.subStat1Type);
                SubStatSumCalculater(DataManager.Instance.GetEquipmentSlotData().amulet2.subStat2Type);
                SubStatSumCalculater(DataManager.Instance.GetEquipmentSlotData().amulet2.subStat3Type);
                SubStatSumCalculater(DataManager.Instance.GetEquipmentSlotData().amulet2.subStat4Type);
            }
            if (DataManager.Instance.GetEquipmentSlotData().amulet3 != null && DataManager.Instance.GetEquipmentSlotData().amulet3.spriteCode != 0)
            {
                DefBonus += InventoryManager.Instance.GetDefense(DataManager.Instance.GetEquipmentSlotData().amulet3.enhancementLevel);
                SubStatSumCalculater(DataManager.Instance.GetEquipmentSlotData().amulet3.subStat1Type);
                SubStatSumCalculater(DataManager.Instance.GetEquipmentSlotData().amulet3.subStat2Type);
                SubStatSumCalculater(DataManager.Instance.GetEquipmentSlotData().amulet3.subStat3Type);
                SubStatSumCalculater(DataManager.Instance.GetEquipmentSlotData().amulet3.subStat4Type);
            }
            if (DataManager.Instance.GetEquipmentSlotData().amulet4 != null && DataManager.Instance.GetEquipmentSlotData().amulet4.spriteCode != 0)
            {
                MainStatSumCalculater(DataManager.Instance.GetEquipmentSlotData().amulet4.statBonusTypes, DataManager.Instance.GetEquipmentSlotData().amulet4.enhancementLevel);
                SubStatSumCalculater(DataManager.Instance.GetEquipmentSlotData().amulet4.subStat1Type);
                SubStatSumCalculater(DataManager.Instance.GetEquipmentSlotData().amulet4.subStat2Type);
                SubStatSumCalculater(DataManager.Instance.GetEquipmentSlotData().amulet4.subStat3Type);
                SubStatSumCalculater(DataManager.Instance.GetEquipmentSlotData().amulet4.subStat4Type);
            }
            if (DataManager.Instance.GetEquipmentSlotData().amulet5 != null && DataManager.Instance.GetEquipmentSlotData().amulet5.spriteCode != 0)
            {
                MainStatSumCalculater(DataManager.Instance.GetEquipmentSlotData().amulet5.statBonusTypes, DataManager.Instance.GetEquipmentSlotData().amulet5.enhancementLevel);
                SubStatSumCalculater(DataManager.Instance.GetEquipmentSlotData().amulet5.subStat1Type);
                SubStatSumCalculater(DataManager.Instance.GetEquipmentSlotData().amulet5.subStat2Type);
                SubStatSumCalculater(DataManager.Instance.GetEquipmentSlotData().amulet5.subStat3Type);
                SubStatSumCalculater(DataManager.Instance.GetEquipmentSlotData().amulet5.subStat4Type);
            }
            if (DataManager.Instance.GetEquipmentSlotData().amulet6 != null && DataManager.Instance.GetEquipmentSlotData().amulet6.spriteCode != 0)
            {
                MainStatSumCalculater(DataManager.Instance.GetEquipmentSlotData().amulet6.statBonusTypes, DataManager.Instance.GetEquipmentSlotData().amulet6.enhancementLevel);
                SubStatSumCalculater(DataManager.Instance.GetEquipmentSlotData().amulet6.subStat1Type);
                SubStatSumCalculater(DataManager.Instance.GetEquipmentSlotData().amulet6.subStat2Type);
                SubStatSumCalculater(DataManager.Instance.GetEquipmentSlotData().amulet6.subStat3Type);
                SubStatSumCalculater(DataManager.Instance.GetEquipmentSlotData().amulet6.subStat4Type);
            }
            //무기
            if (DataManager.Instance.GetEquipmentSlotData().shield != null && DataManager.Instance.GetEquipmentSlotData().shield.spriteCode != 0)
            {
                MainStatSumCalculater(DataManager.Instance.GetEquipmentSlotData().shield.statBonusType, DataManager.Instance.GetEquipmentSlotData().shield.enhancementLevel);
            }
            //실드
            if (DataManager.Instance.GetEquipmentSlotData().weapon != null && DataManager.Instance.GetEquipmentSlotData().weapon.spriteCode != 0)
            {
                MainStatSumCalculater(DataManager.Instance.GetEquipmentSlotData().weapon.statBonusTypes, DataManager.Instance.GetEquipmentSlotData().weapon.enhancementLevel);
            }
            //최종계산
            Debug.Log(basicAtk + "     " + atkBonus + "    " + atkBonus);
            atk = (int)(basicAtk * (1 + atkPerBonus / 100)) + atkBonus;

            HP = (int)(basicHP * (1 + HPPerBonus / 100)) + HPBonus;
            def = (int)(basicDef * (1 + DefPerBonus / 100)) + DefBonus;
            critRate += critRateBonus;
            critDmg += critDmgBonus;
            PenRate += PenRateBonus;
            fireDmg += fireDmgBonus;
            waterDmg += waterDmgBonus;
            electricDmg += electricDmgRateBonus;
            mpAutoRegeneration *= 1 + mpAutoRegenerationBonus / 100;
            //  UIUpdatePlayerParameters();
            text1.text = "공격력: " + atk;
            text2.text = "체력: " + HP;
            text3.text = "방어력: " + def;
            text4.text = "치명타확률: " + critRate + "%";
            text5.text = "치명타피해: " + critDmg + "%";
            text6.text = "관통%: " + PenRate + "%";
            text7.text = "관통: " + pen;
            text8.text = "불속성피해: " + fireDmg + "%";
            text9.text = "물속성피해: " + waterDmg + "%";
            text10.text = "전기속성피해: " + electricDmg + "%";
            text11.text = "MP자동회복: " + mpAutoRegeneration.ToString("F2");
        }
        catch (NullReferenceException ex)
        {
            Debug.LogError("널 참조 예외 발생: " + ex.Message);
        }
    }
    // private void UIUpdatePlayerParameters()
    // {
    //     text1.text = "공격력: " + atk;
    //     text2.text = "체력: " + HP;
    //     text3.text = "방어력: " + def;
    //     text4.text = "치명타확률: " + critRate;
    //     text5.text = "치명타피해: " + critDmg;
    //     text6.text = "관통%: " + PenRate;
    //     text7.text = "관통: " + pen;
    //     text8.text = "불속성피해: " + fireDmg;
    //     text9.text = "물속성피해: " + waterDmg;
    //     text10.text = "전기속성피해: " + electricDmg;

    // }
    private void MainStatSumCalculater(string stat, int level)
    {
        switch (stat)
        {
            case "치명타확률":
                critRateBonus += InventoryManager.Instance.CalculateMainStat(stat, level);
                break;
            case "치명타피해":
                critDmgBonus += InventoryManager.Instance.CalculateMainStat(stat, level);
                break;
            case "공격력%":
                atkPerBonus += InventoryManager.Instance.CalculateMainStat(stat, level);
                break;
            case "체력%":
                HPPerBonus += InventoryManager.Instance.CalculateMainStat(stat, level);
                break;
            case "방어력%":
                DefPerBonus += InventoryManager.Instance.CalculateMainStat(stat, level);
                break;
            case "불속성피해":
                fireDmgBonus += InventoryManager.Instance.CalculateMainStat(stat, level);
                break;
            case "물속성피해":
                waterDmgBonus += InventoryManager.Instance.CalculateMainStat(stat, level);
                break;
            case "전기속성피해":
                electricDmgRateBonus += InventoryManager.Instance.CalculateMainStat(stat, level);
                break;
            case "관통%":
                PenRateBonus += InventoryManager.Instance.CalculateMainStat(stat, level);
                break;
            case "MP자동회복":
                mpAutoRegenerationBonus += InventoryManager.Instance.CalculateMainStat(stat, level);
                break;
            default:
                break;
        }

    }

    private void SubStatSumCalculater(string stat)
    {
        string namePart;
        int valuePart;

        if (stat.Contains("+"))
        {
            string[] parts = stat.Split('+');
            namePart = parts[0];

            if (int.TryParse(parts[1], out int result))
            {
                valuePart = result + 1;
            }
            else
            {
                valuePart = 0; // 숫자 변환 실패 시 0으로 처리
            }
        }
        else
        {
            namePart = stat;
            valuePart = 0 + 1;
        }
        switch (namePart)
        {
            case "공격력":
                atkBonus += valuePart * 19;
                break;
            case "방어력":
                DefBonus += valuePart * 15;
                break;
            case "체력":
                HPBonus += valuePart * 112;
                break;
            case "공격력%":
                atkPerBonus += valuePart * 3f;
                break;
            case "방어력%":
                DefPerBonus += valuePart * 4.8f;
                break;
            case "체력%":
                HPPerBonus += valuePart * 3f;
                break;
            case "치명타확률":
                critRateBonus += valuePart * 2.4f;
                break;
            case "치명타피해":
                critDmgBonus += valuePart * 4.8f;
                break;
            case "관통":
                pen += valuePart * 9;
                break;
            default:
                Debug.Log("오류: 일치하는 스탯 이름 없음");
                break;
        }
    }
}
