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
    [SerializeField] private Image weaponSlot;
    [SerializeField] private Image shieldSlot;
    [SerializeField] private Image amuletSlot1;
    [SerializeField] private Image amuletSlot2;
    [SerializeField] private Image amuletSlot3;
    [SerializeField] private Image amuletSlot4;
    [SerializeField] private Image amuletSlot5;
    [SerializeField] private Image amuletSlot6;
    public void SetEquipmentSlotActive(bool show)
    {
        equipMentSlot.SetActive(show);
    }
    public void SetInventoryActive(bool show)
    {
        inventory.SetActive(show);
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
    }
    public Sprite GetSpriteBySpriteCode(int spriteCode)
    {
        return Resources.Load<Sprite>("ItemIcons/"+spriteCode.ToString("D4"));
    }
}
