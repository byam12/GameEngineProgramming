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
    private ShieldInventory shieldInventory;
    private AmuletInventory1 amuletInventory1;
    private AmuletInventory2 amuletInventory2;
    private AmuletInventory3 amuletInventory3;
    private AmuletInventory4 amuletInventory4;
    private AmuletInventory5 amuletInventory5;
    private AmuletInventory6 amuletInventory6;
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
        //실드 
        shieldInventory = LoadFromJson<ShieldInventory>("ShieldInventory");
        if (shieldInventory == null)
        {
            shieldInventory = new ShieldInventory();
            SaveToJson<ShieldInventory>(shieldInventory, "ShieldInventory");
        }
        //장신구1
        amuletInventory1 = LoadFromJson<AmuletInventory1>("AmuletInventory1");
        if (amuletInventory1 == null)
        {
            amuletInventory1 = new AmuletInventory1();
            SaveToJson<AmuletInventory1>(amuletInventory1, "AmuletInventory1");
        }
        //장신구2
        amuletInventory2 = LoadFromJson<AmuletInventory2>("AmuletInventory2");
        if (amuletInventory2 == null)
        {
            amuletInventory2 = new AmuletInventory2();
            SaveToJson<AmuletInventory2>(amuletInventory2, "AmuletInventory2");
        }
        //장신구3
        amuletInventory3 = LoadFromJson<AmuletInventory3>("AmuletInventory3");
        if (amuletInventory3 == null)
        {
            amuletInventory3 = new AmuletInventory3();
            SaveToJson<AmuletInventory3>(amuletInventory3, "AmuletInventory3");
        }
        //장신구4
        amuletInventory4 = LoadFromJson<AmuletInventory4>("AmuletInventory4");
        if (amuletInventory4 == null)
        {
            amuletInventory4 = new AmuletInventory4();
            SaveToJson<AmuletInventory4>(amuletInventory4, "AmuletInventory4");
        }
        //장신구5
        amuletInventory5 = LoadFromJson<AmuletInventory5>("AmuletInventory5");
        if (amuletInventory5 == null)
        {
            amuletInventory5 = new AmuletInventory5();
            SaveToJson<AmuletInventory5>(amuletInventory5, "AmuletInventory5");
        }
        //장신구6
        amuletInventory6 = LoadFromJson<AmuletInventory6>("AmuletInventory6");
        if (amuletInventory6 == null)
        {
            amuletInventory6 = new AmuletInventory6();
            SaveToJson<AmuletInventory6>(amuletInventory6, "AmuletInventory6");
        }

        //장비칸 아이템 착용 정보
        equipmentSlotData = LoadFromJson<EquipmentSlotData>("EquipmentSlotData");
        if (equipmentSlotData == null){
            equipmentSlotData = new EquipmentSlotData();
            SaveToJson<EquipmentSlotData>(equipmentSlotData, "EquipmentSlotData");
        }
    }
    public void WearEquipment(WeaponData weapon)//장비착용 메소드, 오버로드해서 사용
    {
        equipmentSlotData.weapon = weapon;
        SaveToJson<EquipmentSlotData>(equipmentSlotData, "EquipmentSlotData");
    }
    public void WearEquipment(ShieldData shield)//장비착용 메소드, 오버로드해서 사용
    {
        equipmentSlotData.shield = shield;
        SaveToJson<EquipmentSlotData>(equipmentSlotData, "EquipmentSlotData");
    }
    public void WearEquipment(AmuletData1 amulet1)//장비착용 메소드, 오버로드해서 사용
    {
        equipmentSlotData.amulet1 = amulet1;
        SaveToJson<EquipmentSlotData>(equipmentSlotData, "EquipmentSlotData");
    }
    public void WearEquipment(AmuletData2 amulet2)//장비착용 메소드, 오버로드해서 사용
    {
        equipmentSlotData.amulet2 = amulet2;
        SaveToJson<EquipmentSlotData>(equipmentSlotData, "EquipmentSlotData");
    }
    public void WearEquipment(AmuletData3 amulet3)//장비착용 메소드, 오버로드해서 사용
    {
        equipmentSlotData.amulet3 = amulet3;
        SaveToJson<EquipmentSlotData>(equipmentSlotData, "EquipmentSlotData");
    }
    public void WearEquipment(AmuletData4 amulet4)//장비착용 메소드, 오버로드해서 사용
    {
        equipmentSlotData.amulet4 = amulet4;
        SaveToJson<EquipmentSlotData>(equipmentSlotData, "EquipmentSlotData");
    }
    public void WearEquipment(AmuletData5 amulet5)//장비착용 메소드, 오버로드해서 사용
    {
        equipmentSlotData.amulet5 = amulet5;
        SaveToJson<EquipmentSlotData>(equipmentSlotData, "EquipmentSlotData");
    }
    public void WearEquipment(AmuletData6 amulet6)//장비착용 메소드, 오버로드해서 사용
    {
        equipmentSlotData.amulet6 = amulet6;
        SaveToJson<EquipmentSlotData>(equipmentSlotData, "EquipmentSlotData");
    }
    public void UnWearEquipment(int slotNum)//장비착용 메소드, 오버로드해서 사용
    {
        switch (slotNum)
        {
            case 0:
                for (int i = 0; i < amuletInventory1.amulets1.Count; i++) amuletInventory1.amulets1[i].isEquiped = false;
                equipmentSlotData.amulet1 = null;
                break;
            case 1:
                for (int i = 0; i < amuletInventory2.amulets2.Count; i++) amuletInventory2.amulets2[i].isEquiped = false;
                equipmentSlotData.amulet2 = null;
                break;
            case 2:
                for (int i = 0; i < amuletInventory3.amulets3.Count; i++) amuletInventory3.amulets3[i].isEquiped = false;
                equipmentSlotData.amulet3 = null;
                break;
            case 3:
                for (int i = 0; i < amuletInventory4.amulets4.Count; i++) amuletInventory4.amulets4[i].isEquiped = false;
                equipmentSlotData.amulet4 = null;
                break;
            case 4:
                for (int i = 0; i < amuletInventory5.amulets5.Count; i++) amuletInventory5.amulets5[i].isEquiped = false;
                equipmentSlotData.amulet5 = null;
                break;
            case 5:
                for (int i = 0; i < amuletInventory6.amulets6.Count; i++) amuletInventory6.amulets6[i].isEquiped = false;
                equipmentSlotData.amulet6 = null;
                break;
            case 6:
                for (int i = 0; i < weaponInventory.weapons.Count; i++) weaponInventory.weapons[i].isEquiped = false;
                equipmentSlotData.weapon = null;
                break;
            case 7:
                for (int i = 0; i < shieldInventory.shields.Count; i++) shieldInventory.shields[i].isEquiped = false;
                equipmentSlotData.shield = null;
                break;
        }
        SaveToJson<EquipmentSlotData>(equipmentSlotData, "EquipmentSlotData");
    }
    public EquipmentSlotData GetEquipmentSlotData()
    {
        return equipmentSlotData;
    }
    public WeaponInventory GetWeaponInventory()
    {
        return weaponInventory;
    }
    public ShieldInventory GetShieldInventory()
    {
        return shieldInventory;
    }
    public AmuletInventory1 GetAmuletInventory1()
    {
        return amuletInventory1;
    }
    public AmuletInventory2 GetAmuletInventory2()
    {
        return amuletInventory2;
    }
    public AmuletInventory3 GetAmuletInventory3()
    {
        return amuletInventory3;
    }
    public AmuletInventory4 GetAmuletInventory4()
    {
        return amuletInventory4;
    }
    public AmuletInventory5 GetAmuletInventory5()
    {
        return amuletInventory5;
    }
    public AmuletInventory6 GetAmuletInventory6()
    {
        return amuletInventory6;
    }
    public void AddWeapon(WeaponData weaponData)//무기획득 메소드
    {
        weaponInventory.weapons.Add(weaponData);
        SaveToJson<WeaponInventory>(weaponInventory, "WeaponInventory");
    }
    public void AddShield(ShieldData shieldData)//방패획득 메소드
    {
        shieldInventory.shields.Add(shieldData);
        SaveToJson<ShieldInventory>(shieldInventory, "ShieldInventory");
    }
    public void AddAmulet1(AmuletData1 amuletData1)//장신구획득 메소드
    {
        amuletInventory1.amulets1.Add(amuletData1);
        SaveToJson<AmuletInventory1>(amuletInventory1, "AmuletInventory1");
    }
    public void AddAmulet2(AmuletData2 amuletData2)//장신구획득 메소드
    {
        amuletInventory2.amulets2.Add(amuletData2);
        SaveToJson<AmuletInventory2>(amuletInventory2, "AmuletInventory2");
    }
    
    public void AddAmulet3(AmuletData3 amuletData3)//장신구획득 메소드
    {
        amuletInventory3.amulets3.Add(amuletData3);
        SaveToJson<AmuletInventory3>(amuletInventory3, "AmuletInventory3");
    }
    
    public void AddAmulet4(AmuletData4 amuletData4)//장신구획득 메소드
    {
        amuletInventory4.amulets4.Add(amuletData4);
        SaveToJson<AmuletInventory4>(amuletInventory4, "AmuletInventory4");
    }
    public void AddAmulet5(AmuletData5 amuletData5)//장신구획득 메소드
    {
        amuletInventory5.amulets5.Add(amuletData5);
        SaveToJson<AmuletInventory5>(amuletInventory5, "AmuletInventory5");
    }
    public void AddAmulet6(AmuletData6 amuletData6)//장신구획득 메소드
    {
        amuletInventory6.amulets6.Add(amuletData6);
        SaveToJson<AmuletInventory6>(amuletInventory6, "AmuletInventory6");
    }
    public void SaveInventory(int num)
    {
        switch (num)
        {
            case 1:
                SaveToJson<AmuletInventory1>(amuletInventory1, "AmuletInventory1");
                break;
            case 2:
                SaveToJson<AmuletInventory2>(amuletInventory2, "AmuletInventory2");
                break;
            case 3:
                SaveToJson<AmuletInventory3>(amuletInventory3, "AmuletInventory3");
                break;
            case 4:
                SaveToJson<AmuletInventory4>(amuletInventory4, "AmuletInventory4");
                break;
            case 5:
                SaveToJson<AmuletInventory5>(amuletInventory5, "AmuletInventory5");
                break;
            case 6:
                SaveToJson<AmuletInventory6>(amuletInventory6, "AmuletInventory6");
                break;
            case 7:
                SaveToJson<WeaponInventory>(weaponInventory, "WeaponInventory");
                break;
            case 8:
                SaveToJson<ShieldInventory>(shieldInventory, "ShieldInventory");
                break;

        }
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
