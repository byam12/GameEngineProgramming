using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.LightTransport;
using UnityEngine.UI;

public class InventoryButtonManager : MonoBehaviour
{
    public static InventoryButtonManager Instance { get; private set; }
    [SerializeField] EquipmentButtonManager equipmentButtonManager;

    private Button[] buttons;
    void Start()
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

    void OnButtonClicked(int buttonIndex)
    {
        InventoryManager.Instance.SetSelectedPartItemInfoPanel(false);
        bool isEquip = false;
        switch (GameManager.Instance.currentOpenedInventoryNumber)
        {
            case 0:
                AmuletInventory1 amuletInventory1 = DataManager.Instance.GetAmuletInventory1();
                GameManager.Instance.currentSelectItem = amuletInventory1.amulets1[buttonIndex];
                isEquip = amuletInventory1.amulets1[buttonIndex].isEquiped;
                break;
            case 1:
                AmuletInventory2 amuletInventory2 = DataManager.Instance.GetAmuletInventory2();
                GameManager.Instance.currentSelectItem = amuletInventory2.amulets2[buttonIndex];
                isEquip = amuletInventory2.amulets2[buttonIndex].isEquiped;
                break;
            case 2:
                AmuletInventory3 amuletInventory3 = DataManager.Instance.GetAmuletInventory3();
                GameManager.Instance.currentSelectItem = amuletInventory3.amulets3[buttonIndex];
                isEquip = amuletInventory3.amulets3[buttonIndex].isEquiped;
                break;
            case 3:
                AmuletInventory4 amuletInventory4 = DataManager.Instance.GetAmuletInventory4();
                GameManager.Instance.currentSelectItem = amuletInventory4.amulets4[buttonIndex];
                isEquip = amuletInventory4.amulets4[buttonIndex].isEquiped;
                break;
            case 4:
                AmuletInventory5 amuletInventory5 = DataManager.Instance.GetAmuletInventory5();
                GameManager.Instance.currentSelectItem = amuletInventory5.amulets5[buttonIndex];
                isEquip = amuletInventory5.amulets5[buttonIndex].isEquiped;
                break;
            case 5:
                AmuletInventory6 amuletInventory6 = DataManager.Instance.GetAmuletInventory6();
                GameManager.Instance.currentSelectItem = amuletInventory6.amulets6[buttonIndex];
                isEquip = amuletInventory6.amulets6[buttonIndex].isEquiped;
                break;
            case 6:
                WeaponInventory weaponInventory = DataManager.Instance.GetWeaponInventory();
                GameManager.Instance.currentSelectItem = weaponInventory.weapons[buttonIndex];
                isEquip = weaponInventory.weapons[buttonIndex].isEquiped;
                break;
            case 7:
                ShieldInventory shieldInventory = DataManager.Instance.GetShieldInventory();
                GameManager.Instance.currentSelectItem = shieldInventory.shields[buttonIndex];
                isEquip = shieldInventory.shields[buttonIndex].isEquiped;
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
    }
    public void EquipItem()
    {
        UnEquipItem();
        object currentSelectItem = GameManager.Instance.currentSelectItem;
        switch (GameManager.Instance.currentOpenedInventoryNumber)
        {
            case 0:
                ((AmuletData1)currentSelectItem).isEquiped = true;
                DataManager.Instance.WearEquipment((AmuletData1)currentSelectItem);
                break;
            case 1:
                ((AmuletData2)currentSelectItem).isEquiped = true;
                DataManager.Instance.WearEquipment((AmuletData2)currentSelectItem);
                break;
            case 2:
                ((AmuletData3)currentSelectItem).isEquiped = true;
                DataManager.Instance.WearEquipment((AmuletData3)currentSelectItem);
                break;
            case 3:
                ((AmuletData4)currentSelectItem).isEquiped = true;
                DataManager.Instance.WearEquipment((AmuletData4)currentSelectItem);
                break;
            case 4:
                ((AmuletData5)currentSelectItem).isEquiped = true;
                DataManager.Instance.WearEquipment((AmuletData5)currentSelectItem);
                break;
            case 5:
                ((AmuletData6)currentSelectItem).isEquiped = true;
                DataManager.Instance.WearEquipment((AmuletData6)currentSelectItem);
                break;
            case 6:
                ((WeaponData)currentSelectItem).isEquiped = true;
                DataManager.Instance.WearEquipment((WeaponData)currentSelectItem);
                break;
            case 7:
                ((ShieldData)currentSelectItem).isEquiped = true;
                DataManager.Instance.WearEquipment((ShieldData)currentSelectItem);
                break;
        }
        InventoryManager.Instance.EquipmentSlotSpriteUpdate(GameManager.Instance.currentOpenedInventoryNumber);
        equipmentButtonManager.OnButtonClicked(GameManager.Instance.currentOpenedInventoryNumber);
        OnButtonClicked(FindEquipedItemIndex());
    }
    public void UnEquipItem()
    {
        DataManager.Instance.UnWearEquipment(GameManager.Instance.currentOpenedInventoryNumber);
        InventoryManager.Instance.EquipmentSlotSpriteUpdate(GameManager.Instance.currentOpenedInventoryNumber);
        equipmentButtonManager.OnButtonClicked(GameManager.Instance.currentOpenedInventoryNumber);
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
        equipmentButtonManager.OnButtonClicked(GameManager.Instance.currentOpenedInventoryNumber);
    }
}