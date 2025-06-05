using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButtonManager : MonoBehaviour
{
    public static InventoryButtonManager Instance { get; private set; }
    //[SerializeField] EquipmentButtonManager equipmentButtonManager;
    
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
        InventoryManager.Instance.SetItemInfoPanel(true);
        InventoryManager.Instance.SetSelectedPartItemInfoPanel(false);
        switch (GameManager.Instance.currentOpenedInventoryNumber)
        {
            case 0:
                AmuletInventory1 amuletInventory1 = DataManager.Instance.GetAmuletInventory1();
                GameManager.Instance.currentSelectItem = amuletInventory1.amulets1[buttonIndex];
                break;
            case 1:
                AmuletInventory2 amuletInventory2 = DataManager.Instance.GetAmuletInventory2();
                GameManager.Instance.currentSelectItem = amuletInventory2.amulets2[buttonIndex];
                break;
            case 2:
                AmuletInventory3 amuletInventory3 = DataManager.Instance.GetAmuletInventory3();
                GameManager.Instance.currentSelectItem = amuletInventory3.amulets3[buttonIndex];
                break;
            case 3:
                AmuletInventory4 amuletInventory4 = DataManager.Instance.GetAmuletInventory4();
                GameManager.Instance.currentSelectItem = amuletInventory4.amulets4[buttonIndex];
                break;
            case 4:
                AmuletInventory5 amuletInventory5 = DataManager.Instance.GetAmuletInventory5();
                GameManager.Instance.currentSelectItem = amuletInventory5.amulets5[buttonIndex];
                break;
            case 5:
                AmuletInventory6 amuletInventory6 = DataManager.Instance.GetAmuletInventory6();
                GameManager.Instance.currentSelectItem = amuletInventory6.amulets6[buttonIndex];
                break;
            case 6:
                WeaponInventory weaponInventory = DataManager.Instance.GetWeaponInventory();
                GameManager.Instance.currentSelectItem = weaponInventory.weapons[buttonIndex];
                break;
            case 7:
                ShieldInventory shieldInventory = DataManager.Instance.GetShieldInventory();
                GameManager.Instance.currentSelectItem = shieldInventory.shields[buttonIndex];
                break;
        }
    }
    public void EquipItem()
    {
        object currentSelectItem = GameManager.Instance.currentSelectItem;
        switch (GameManager.Instance.currentOpenedInventoryNumber)
        {
            case 0:
                DataManager.Instance.WearEquipment((AmuletData1)currentSelectItem);
                break;
            case 1:
                DataManager.Instance.WearEquipment((AmuletData2)currentSelectItem);
                break;
            case 2:
                DataManager.Instance.WearEquipment((AmuletData3)currentSelectItem);
                break;
            case 3:
                DataManager.Instance.WearEquipment((AmuletData4)currentSelectItem);
                break;
            case 4:
                DataManager.Instance.WearEquipment((AmuletData5)currentSelectItem);
                break;
            case 5:
                DataManager.Instance.WearEquipment((AmuletData6)currentSelectItem);
                break;
            case 6:
                DataManager.Instance.WearEquipment((WeaponData)currentSelectItem);
                break;
            case 7:
                DataManager.Instance.WearEquipment((ShieldData)currentSelectItem);
                break;
        }
        InventoryManager.Instance.EquipmentSlotSpriteUpdate(GameManager.Instance.currentOpenedInventoryNumber);
    }
    public void UnEquipItem()
    {
        DataManager.Instance.UnWearEquipment(GameManager.Instance.currentOpenedInventoryNumber);
        InventoryManager.Instance.EquipmentSlotSpriteUpdate(GameManager.Instance.currentOpenedInventoryNumber);
    }
}