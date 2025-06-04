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
    private WeaponInventory weaponInventory;
    private EquipmentSlotData equipmentSlotData;
    void Start()
    {
        JsonFileNullCheck();
    }
    private void JsonFileNullCheck()
    {
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
    public WeaponInventory GetWeaponInventory()
    {
        return weaponInventory;
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
