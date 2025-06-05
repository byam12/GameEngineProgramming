using UnityEngine;
using UnityEngine.UI;
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

    [SerializeField] private GameObject equipMentSlot;
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject itemInfoPanel;
    [SerializeField] private GameObject craftingPanel;
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

    public void UpdateUIWearingEquipMents() // 착용중인 장비 장비칸에 띄우기
    {
        EquipmentSlotData equipmentSlotData = DataManager.Instance.GetEquipmentSlotData();
        weaponSlot.sprite = Resources.Load<Sprite>("ItemIcons/" + equipmentSlotData.weapon.spriteCode.ToString("D4"));
        shieldSlot.sprite = Resources.Load<Sprite>("ItemIcons/" + equipmentSlotData.shield.spriteCode.ToString("D4"));
        amuletSlot1.sprite = Resources.Load<Sprite>("ItemIcons/" + equipmentSlotData.amulet1.spriteCode.ToString("D4"));
        amuletSlot2.sprite = Resources.Load<Sprite>("ItemIcons/" + equipmentSlotData.amulet2.spriteCode.ToString("D4"));
        amuletSlot3.sprite = Resources.Load<Sprite>("ItemIcons/" + equipmentSlotData.amulet3.spriteCode.ToString("D4"));
        amuletSlot4.sprite = Resources.Load<Sprite>("ItemIcons/" + equipmentSlotData.amulet4.spriteCode.ToString("D4"));
        amuletSlot5.sprite = Resources.Load<Sprite>("ItemIcons/" + equipmentSlotData.amulet5.spriteCode.ToString("D4"));
        amuletSlot6.sprite = Resources.Load<Sprite>("ItemIcons/" + equipmentSlotData.amulet6.spriteCode.ToString("D4"));
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
                else amuletSlot6.sprite = Resources.Load<Sprite>("ItemIcons/" + equipmentSlotData.amulet5.spriteCode.ToString("D4"));
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
    // public void CraftSelectedItem(Button selectiedItem)
    // {
    //     int partNumber = int.Parse(selectiedItem.image.sprite.name.Substring(0, 1));
    //     int setNumber = int.Parse(selectiedItem.image.sprite.name.Substring(1, 3));
    //     switch (partNumber - 1)
    //     {
    //         case 0:
    //             Crafter(partNumber, setNumber);
    //             break;
    //     }
    // }
    // private object Crafter(int partNumber, int setNumber)
    // {
    //     switch (setNumber)
    //     {
    //         case 0:
    //             return CraftItem(partNumber);
    //         case 1:
    //             return CraftItem(partNumber);
    //     }
    // }
    // private object CraftItem(int partNumber)
    // {
    
    //     return partNumber;
    // }
    
}
