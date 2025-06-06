using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.LightTransport;
using UnityEngine.UI;

public class InventoryButtonManager : MonoBehaviour
{
    public static InventoryButtonManager Instance { get; private set; }
    [SerializeField] EquipmentButtonManager equipmentButtonManager;

    private Button[] buttons;
    private int latestIndex=-1;
    void Awake()
    {
        // 이 스크립트가 붙은 오브젝트 자식들 중 Button 컴포넌트 다 가져오기
        buttons = GetComponentsInChildren<Button>();

        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // 클로저 문제 방지
            buttons[i].onClick.AddListener(() => OnButtonClicked(index));
        }
    }
    public Button[] GetInventoryButtons()
    {
        return buttons;
    }

    public void OnButtonClicked(int buttonIndex)
    {
        InventoryManager.Instance.SetSelectedPartItemInfoPanel(false);
        bool isEquip = false;
        int enhancementLevel = 0;
        latestIndex = buttonIndex;
        switch (GameManager.Instance.currentOpenedInventoryNumber)
        {
            case 0:
                AmuletInventory1 amuletInventory1 = DataManager.Instance.GetAmuletInventory1();
                GameManager.Instance.currentSelectItem = amuletInventory1.amulets1[buttonIndex];
                isEquip = amuletInventory1.amulets1[buttonIndex].isEquiped;
                enhancementLevel = amuletInventory1.amulets1[buttonIndex].enhancementLevel;
                break;
            case 1:
                AmuletInventory2 amuletInventory2 = DataManager.Instance.GetAmuletInventory2();
                GameManager.Instance.currentSelectItem = amuletInventory2.amulets2[buttonIndex];
                isEquip = amuletInventory2.amulets2[buttonIndex].isEquiped;
                enhancementLevel = amuletInventory2.amulets2[buttonIndex].enhancementLevel;
                break;
            case 2:
                AmuletInventory3 amuletInventory3 = DataManager.Instance.GetAmuletInventory3();
                GameManager.Instance.currentSelectItem = amuletInventory3.amulets3[buttonIndex];
                isEquip = amuletInventory3.amulets3[buttonIndex].isEquiped;
                enhancementLevel = amuletInventory3.amulets3[buttonIndex].enhancementLevel;
                break;
            case 3:
                AmuletInventory4 amuletInventory4 = DataManager.Instance.GetAmuletInventory4();
                GameManager.Instance.currentSelectItem = amuletInventory4.amulets4[buttonIndex];
                isEquip = amuletInventory4.amulets4[buttonIndex].isEquiped;
                enhancementLevel = amuletInventory4.amulets4[buttonIndex].enhancementLevel;
                break;
            case 4:
                AmuletInventory5 amuletInventory5 = DataManager.Instance.GetAmuletInventory5();
                GameManager.Instance.currentSelectItem = amuletInventory5.amulets5[buttonIndex];
                isEquip = amuletInventory5.amulets5[buttonIndex].isEquiped;
                enhancementLevel = amuletInventory5.amulets5[buttonIndex].enhancementLevel;
                break;
            case 5:
                AmuletInventory6 amuletInventory6 = DataManager.Instance.GetAmuletInventory6();
                GameManager.Instance.currentSelectItem = amuletInventory6.amulets6[buttonIndex];
                isEquip = amuletInventory6.amulets6[buttonIndex].isEquiped;
                enhancementLevel = amuletInventory6.amulets6[buttonIndex].enhancementLevel;
                break;
            case 6:
                WeaponInventory weaponInventory = DataManager.Instance.GetWeaponInventory();
                GameManager.Instance.currentSelectItem = weaponInventory.weapons[buttonIndex];
                isEquip = weaponInventory.weapons[buttonIndex].isEquiped;
                enhancementLevel = -1;
                break;
            case 7:
                ShieldInventory shieldInventory = DataManager.Instance.GetShieldInventory();
                GameManager.Instance.currentSelectItem = shieldInventory.shields[buttonIndex];
                isEquip = shieldInventory.shields[buttonIndex].isEquiped;
                enhancementLevel = -1;
                break;
        }
        InventoryManager.Instance.SetItemInfoPanel(true);
        InventoryManager.Instance.SetSelectedItem();
        if (isEquip)
        {
            InventoryManager.Instance.SetEquipBtn(false);
            InventoryManager.Instance.SetExtractBtn(false);
            InventoryManager.Instance.SetEnhanceBtn(true);
            InventoryManager.Instance.SetUnEquipBtn(true);
        }
        else
        {
            InventoryManager.Instance.SetEquipBtn(true);
            InventoryManager.Instance.SetExtractBtn(true);
            InventoryManager.Instance.SetEnhanceBtn(true);
            InventoryManager.Instance.SetUnEquipBtn(false);
        }
        if (enhancementLevel <= -1 || enhancementLevel >= 15)
        {
            InventoryManager.Instance.SetEnhanceBtn(false);
        }
    }
    public void EquipItem()
    {
        UnEquipItem();
        object currentSelectItem = GameManager.Instance.currentSelectItem;
        switch (GameManager.Instance.currentOpenedInventoryNumber)
        {
            case 0:
                ((AmuletData1)currentSelectItem).isEquiped = true;
                DataManager.Instance.GetAmuletInventory1().amulets1[latestIndex].isEquiped = true;
                DataManager.Instance.SaveInventory(GameManager.Instance.currentOpenedInventoryNumber+1);
                DataManager.Instance.WearEquipment((AmuletData1)currentSelectItem);
                break;
            case 1:
                ((AmuletData2)currentSelectItem).isEquiped = true;
                DataManager.Instance.GetAmuletInventory2().amulets2[latestIndex].isEquiped = true;
                DataManager.Instance.SaveInventory(GameManager.Instance.currentOpenedInventoryNumber+1);
                DataManager.Instance.WearEquipment((AmuletData2)currentSelectItem);
                break;
            case 2:
                ((AmuletData3)currentSelectItem).isEquiped = true;
                DataManager.Instance.GetAmuletInventory3().amulets3[latestIndex].isEquiped = true;
                DataManager.Instance.SaveInventory(GameManager.Instance.currentOpenedInventoryNumber+1);
                DataManager.Instance.WearEquipment((AmuletData3)currentSelectItem);
                break;
            case 3:
                ((AmuletData4)currentSelectItem).isEquiped = true;
                DataManager.Instance.GetAmuletInventory4().amulets4[latestIndex].isEquiped = true;
                DataManager.Instance.SaveInventory(GameManager.Instance.currentOpenedInventoryNumber+1);
                DataManager.Instance.WearEquipment((AmuletData4)currentSelectItem);
                break;
            case 4:
                ((AmuletData5)currentSelectItem).isEquiped = true;
                DataManager.Instance.GetAmuletInventory5().amulets5[latestIndex].isEquiped = true;
                DataManager.Instance.SaveInventory(GameManager.Instance.currentOpenedInventoryNumber+1);
                DataManager.Instance.WearEquipment((AmuletData5)currentSelectItem);
                break;
            case 5:
                ((AmuletData6)currentSelectItem).isEquiped = true;
                DataManager.Instance.GetAmuletInventory6().amulets6[latestIndex].isEquiped = true;
                DataManager.Instance.SaveInventory(GameManager.Instance.currentOpenedInventoryNumber+1);
                DataManager.Instance.WearEquipment((AmuletData6)currentSelectItem);
                break;
            case 6:
                ((WeaponData)currentSelectItem).isEquiped = true;
                DataManager.Instance.GetWeaponInventory().weapons[latestIndex].isEquiped = true;
                DataManager.Instance.SaveInventory(GameManager.Instance.currentOpenedInventoryNumber+1);
                DataManager.Instance.WearEquipment((WeaponData)currentSelectItem);
                break;
            case 7:
                ((ShieldData)currentSelectItem).isEquiped = true;
                DataManager.Instance.GetShieldInventory().shields[latestIndex].isEquiped = true;
                DataManager.Instance.SaveInventory(GameManager.Instance.currentOpenedInventoryNumber+1);
                DataManager.Instance.WearEquipment((ShieldData)currentSelectItem);
                break;
        }
        InventoryManager.Instance.EquipmentSlotSpriteUpdate(GameManager.Instance.currentOpenedInventoryNumber);
        equipmentButtonManager.OnButtonClicked(GameManager.Instance.currentOpenedInventoryNumber);
        OnButtonClicked(FindEquipedItemIndex());
        PlayerParametersDefault.Instance.UpdatePayerDefaultParameters();
    }
    public void UnEquipItem()
    {
        DataManager.Instance.UnWearEquipment(GameManager.Instance.currentOpenedInventoryNumber);
        InventoryManager.Instance.EquipmentSlotSpriteUpdate(GameManager.Instance.currentOpenedInventoryNumber);
        equipmentButtonManager.OnButtonClicked(GameManager.Instance.currentOpenedInventoryNumber);
        PlayerParametersDefault.Instance.UpdatePayerDefaultParameters();
    }
    private int FindEquipedItemIndex()
    {
        int index = -1;
        for (int i = 0; i < buttons.Length; i++)
        {
            Outline outline = buttons[i].GetComponent<Outline>();
            if (outline != null && outline.enabled)
            {
                index = i;
                break;  // 하나만 활성화됐으니 찾으면 끝
            }
        }
        return index;
    }
    //추출은 여기
    public void ExtractItem()
    {
        object currentSelectItem = GameManager.Instance.currentSelectItem;
        switch (GameManager.Instance.currentOpenedInventoryNumber)
        {
            case 0:
                DataManager.Instance.GetAmuletInventory1().amulets1.Remove((AmuletData1)currentSelectItem);
                break;
            case 1:
                DataManager.Instance.GetAmuletInventory2().amulets2.Remove((AmuletData2)currentSelectItem);
                break;
            case 2:
                DataManager.Instance.GetAmuletInventory3().amulets3.Remove((AmuletData3)currentSelectItem);
                break;
            case 3:
                DataManager.Instance.GetAmuletInventory4().amulets4.Remove((AmuletData4)currentSelectItem);
                break;
            case 4:
                DataManager.Instance.GetAmuletInventory5().amulets5.Remove((AmuletData5)currentSelectItem);
                break;
            case 5:
                DataManager.Instance.GetAmuletInventory6().amulets6.Remove((AmuletData6)currentSelectItem);
                break;
            case 6:
                DataManager.Instance.GetWeaponInventory().weapons.Remove((WeaponData)currentSelectItem);
                break;
            case 7:
                DataManager.Instance.GetShieldInventory().shields.Remove((ShieldData)currentSelectItem);
                break;
        }
        DataManager.Instance.SaveInventory(GameManager.Instance.currentOpenedInventoryNumber + 1);
        equipmentButtonManager.OnButtonClicked(GameManager.Instance.currentOpenedInventoryNumber);
    }
    // 강화는 여기
    public void Enhancement()
    {
        object currentSelectItem = GameManager.Instance.currentSelectItem;
        switch (GameManager.Instance.currentOpenedInventoryNumber)
        {
            case 0:
                AmuletData1 amuletData1 = (AmuletData1)currentSelectItem;
                if (amuletData1.enhancementLevel < 15)
                {
                    amuletData1.enhancementLevel++;
                    if (amuletData1.enhancementLevel % 3 == 0)
                    {
                        switch (UnityEngine.Random.Range(1, 5))
                        {
                            case 1:
                                amuletData1.subStat1Type = UpgradeSubStat(amuletData1.subStat1Type);
                                break;
                            case 2:
                                amuletData1.subStat2Type = UpgradeSubStat(amuletData1.subStat2Type);
                                break;
                            case 3:
                                amuletData1.subStat3Type = UpgradeSubStat(amuletData1.subStat3Type);
                                break;
                            case 4:
                                amuletData1.subStat4Type = UpgradeSubStat(amuletData1.subStat4Type);
                                break;
                        }
                    }
                }
                break;
            case 1:
                AmuletData2 amuletData2 = (AmuletData2)currentSelectItem;
                if (amuletData2.enhancementLevel < 15)
                {
                    amuletData2.enhancementLevel++;
                    if (amuletData2.enhancementLevel % 3 == 0)
                    {
                        switch (UnityEngine.Random.Range(1, 5))
                        {
                            case 1:
                                amuletData2.subStat1Type = UpgradeSubStat(amuletData2.subStat1Type);
                                break;
                            case 2:
                                amuletData2.subStat2Type = UpgradeSubStat(amuletData2.subStat2Type);
                                break;
                            case 3:
                                amuletData2.subStat3Type = UpgradeSubStat(amuletData2.subStat3Type);
                                break;
                            case 4:
                                amuletData2.subStat4Type = UpgradeSubStat(amuletData2.subStat4Type);
                                break;
                        }
                    }
                }
                break;
            case 2:
                AmuletData3 amuletData3 = (AmuletData3)currentSelectItem;
                if (amuletData3.enhancementLevel < 15)
                {
                    amuletData3.enhancementLevel++;
                    if (amuletData3.enhancementLevel % 3 == 0)
                    {
                        switch (UnityEngine.Random.Range(1, 5))
                        {
                            case 1:
                                amuletData3.subStat1Type = UpgradeSubStat(amuletData3.subStat1Type);
                                break;
                            case 2:
                                amuletData3.subStat2Type = UpgradeSubStat(amuletData3.subStat2Type);
                                break;
                            case 3:
                                amuletData3.subStat3Type = UpgradeSubStat(amuletData3.subStat3Type);
                                break;
                            case 4:
                                amuletData3.subStat4Type = UpgradeSubStat(amuletData3.subStat4Type);
                                break;
                        }
                    }
                }
                break;
            case 3:
                AmuletData4 amuletData4 = (AmuletData4)currentSelectItem;
                if (amuletData4.enhancementLevel < 15)
                {
                    amuletData4.enhancementLevel++;
                    if (amuletData4.enhancementLevel % 3 == 0)
                    {
                        switch (UnityEngine.Random.Range(1, 5))
                        {
                            case 1:
                                amuletData4.subStat1Type = UpgradeSubStat(amuletData4.subStat1Type);
                                break;
                            case 2:
                                amuletData4.subStat2Type = UpgradeSubStat(amuletData4.subStat2Type);
                                break;
                            case 3:
                                amuletData4.subStat3Type = UpgradeSubStat(amuletData4.subStat3Type);
                                break;
                            case 4:
                                amuletData4.subStat4Type = UpgradeSubStat(amuletData4.subStat4Type);
                                break;
                        }
                    }
                }
                break;
            case 4:
                AmuletData5 amuletData5 = (AmuletData5)currentSelectItem;
                if (amuletData5.enhancementLevel < 15)
                {
                    amuletData5.enhancementLevel++;
                    if (amuletData5.enhancementLevel % 3 == 0)
                    {
                        switch (UnityEngine.Random.Range(1, 5))
                        {
                            case 1:
                                amuletData5.subStat1Type = UpgradeSubStat(amuletData5.subStat1Type);
                                break;
                            case 2:
                                amuletData5.subStat2Type = UpgradeSubStat(amuletData5.subStat2Type);
                                break;
                            case 3:
                                amuletData5.subStat3Type = UpgradeSubStat(amuletData5.subStat3Type);
                                break;
                            case 4:
                                amuletData5.subStat4Type = UpgradeSubStat(amuletData5.subStat4Type);
                                break;
                        }
                    }
                }
                break;
            case 5:
                AmuletData6 amuletData6 = (AmuletData6)currentSelectItem;
                if (amuletData6.enhancementLevel < 15)
                {
                    amuletData6.enhancementLevel++;
                    if (amuletData6.enhancementLevel % 3 == 0)
                    {
                        switch (UnityEngine.Random.Range(1, 5))
                        {
                            case 1:
                                amuletData6.subStat1Type = UpgradeSubStat(amuletData6.subStat1Type);
                                break;
                            case 2:
                                amuletData6.subStat2Type = UpgradeSubStat(amuletData6.subStat2Type);
                                break;
                            case 3:
                                amuletData6.subStat3Type = UpgradeSubStat(amuletData6.subStat3Type);
                                break;
                            case 4:
                                amuletData6.subStat4Type = UpgradeSubStat(amuletData6.subStat4Type);
                                break;
                        }
                    }
                }
                break;
        }
        DataManager.Instance.SaveInventory(GameManager.Instance.currentOpenedInventoryNumber + 1);
        OnButtonClicked(latestIndex);
        PlayerParametersDefault.Instance.UpdatePayerDefaultParameters();
    }
    private string UpgradeSubStat(string substat)
    {
        int plusIndex = substat.IndexOf('+');
        if (plusIndex == -1)
        {
            // '+'가 없으면 그냥 +1 붙이기
            return substat + "+1";
        }
        else
        {
            // '+'가 있으면 숫자 부분만 떼서 1 증가 후 붙이기
            string statName = substat.Substring(0, plusIndex);
            string numberPart = substat.Substring(plusIndex + 1);

            if (int.TryParse(numberPart, out int number))
            {
                number += 1;
                return statName + "+" + number.ToString();
            }
            else
            {
                // 숫자 부분이 이상한 경우 (안나올 거라 가정)
                return substat + "+1";
            }
        }
    }
}