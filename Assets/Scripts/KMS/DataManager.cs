using UnityEngine;
using System.IO;
public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }
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
    private int itemNumber = 1; // 아이템 고유넘버 , 종류 무관 획득순으로 부여 
    private WeaponInventory weaponInventory;
    private EquipmentSlotData equipmentSlotData;
    void Start()
    {
        JsonFileNullCheck();
    }
    private void JsonFileNullCheck()
    {
        //아이템 고유넘버 초기화   
        BasicData basicData = LoadFromJson<BasicData>("BasicData");
        if (basicData == null)
        {
            basicData = new BasicData();
            basicData.itemNumber = 0;
            SaveToJson<BasicData>(basicData, "BasicData");
        }
        else
        {
            itemNumber = basicData.itemNumber;
        }
        Debug.Log("ItemNumber is " + itemNumber);
        //무기 인벤  
        weaponInventory = LoadFromJson<WeaponInventory>("WeaponInventory");
        if (weaponInventory == null)
        {
            weaponInventory = new WeaponInventory();
            SaveToJson<WeaponInventory>(weaponInventory, "WeaponInventory");
        }
        //장비칸 아이템 착용 정보
        equipmentSlotData = LoadFromJson<EquipmentSlotData>("EquipmentSlotData");
        if (equipmentSlotData == null){
            equipmentSlotData = new EquipmentSlotData();
            SaveToJson<EquipmentSlotData>(equipmentSlotData, "EquipmentSlotData");
        }
    }
    public int[] GetWeaponInventoryItemNumber()
    {
        int count = weaponInventory.weapons.Count;
        int[] itemNumbers = new int[count];
        for (int i = 0; i < count; i++){
            itemNumbers[i] = weaponInventory.weapons[i].itemNumber;
        }
        return itemNumbers;
    }
    public void WearEquipment(WeaponData weapon)//장비착용 메소드, 오버로드해서 사용
    {
        equipmentSlotData.weapon = weapon;
        SaveToJson<EquipmentSlotData>(equipmentSlotData, "EquipmentSlotData");
    }
    public EquipmentSlotData GetEquipmentSlotData()
    {
        return equipmentSlotData;    
    }
    public void AddWeapon(WeaponData weaponData)//무기획득 메소드
    {
        weaponInventory.weapons.Add(weaponData);
        CountItemNumber();
    }
    public void CountItemNumber()//장비 고유넘버 + 1
    {
        itemNumber++;
        BasicData basicData = LoadFromJson<BasicData>("BasicData");
        basicData.itemNumber = itemNumber;
        SaveToJson<BasicData>(basicData, "BasicData");
        Debug.Log("ItemNumber is " + itemNumber);
    }

    public static void SaveToJson<T>(T data, string fileName)
    {
        string json = JsonUtility.ToJson(data, true); // pretty print
        string path = Path.Combine(Application.persistentDataPath, fileName);
        File.WriteAllText(path, json);
        Debug.Log($"[DataManager] Saved to {path}");
    }

    public static T LoadFromJson<T>(string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);
        if (!File.Exists(path))
        {
            Debug.LogWarning($"[DataManager] File not found at {path}");
            return default;
        }

        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<T>(json);
    }

    // public static bool DeleteData(string fileName)
    // {
    //     string path = Path.Combine(Application.persistentDataPath, fileName);
    //     if (File.Exists(path))
    //     {
    //         File.Delete(path);
    //         Debug.Log($"[DataManager] Deleted {fileName}");
    //         return true;
    //     }

    //     Debug.LogWarning($"[DataManager] File not found: {fileName}");
    //     return false;
    // } 게임 완전 초기화 구현용
}
