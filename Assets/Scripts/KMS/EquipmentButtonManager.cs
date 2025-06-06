using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentButtonManager : MonoBehaviour
{
    [SerializeField] private InventoryButtonManager inventoryButtonManager;
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
    public Button[] GetEquipMentButtons()
    {
        return buttons;
    }
    public void OnButtonClicked(int buttonIndex)
    {
        InventoryManager.Instance.SetItemInfoPanel(false);
        InventoryManager.Instance.SetInventoryActive(false);
        InventoryManager.Instance.SetCraftingPanel(false);
        InventoryManager.Instance.SetEquipmentSlotActive(true);
        InventoryManager.Instance.SetInventoryActive(true);
        Debug.Log("클릭한 버튼 번호는 " + buttonIndex);
        GameManager.Instance.currentOpenedInventoryNumber = buttonIndex;
        Button[] invenButtons = inventoryButtonManager.GetInventoryButtons();
        switch (buttonIndex)
        {
            case 0:
                AmuletInventory1 amuletInventory1 = DataManager.Instance.GetAmuletInventory1();
                for (int i = 0; i < invenButtons.Length; i++)
                {
                    if (i < amuletInventory1.amulets1.Count)
                    {
                        invenButtons[i].GetComponent<Outline>().enabled = amuletInventory1.amulets1[i].isEquiped;
                        invenButtons[i].enabled = true;
                        invenButtons[i].image.sprite = InventoryManager.Instance.GetSpriteBySpriteCode(amuletInventory1.amulets1[i].spriteCode);
                    }
                    else
                    {
                        invenButtons[i].GetComponent<Outline>().enabled = false;
                        invenButtons[i].enabled = false;
                        invenButtons[i].image.sprite = InventoryManager.Instance.GetSpriteBySpriteCode(0001);
                    }
                }
                break;
            case 1:
                AmuletInventory2 amuletInventory2 = DataManager.Instance.GetAmuletInventory2();
                for (int i = 0; i < invenButtons.Length; i++)
                {
                    if (i < amuletInventory2.amulets2.Count)
                    {
                        invenButtons[i].GetComponent<Outline>().enabled = amuletInventory2.amulets2[i].isEquiped;
                        invenButtons[i].enabled = true;
                        invenButtons[i].image.sprite = InventoryManager.Instance.GetSpriteBySpriteCode(amuletInventory2.amulets2[i].spriteCode);
                    }
                    else
                    {
                        invenButtons[i].GetComponent<Outline>().enabled = false;
                        invenButtons[i].enabled = false;
                        invenButtons[i].image.sprite = InventoryManager.Instance.GetSpriteBySpriteCode(0001);
                    }
                }
                break;
            case 2:
                AmuletInventory3 amuletInventory3 = DataManager.Instance.GetAmuletInventory3();
                for (int i = 0; i < invenButtons.Length; i++)
                {
                    if (i < amuletInventory3.amulets3.Count)
                    {
                        invenButtons[i].GetComponent<Outline>().enabled = amuletInventory3.amulets3[i].isEquiped;
                        invenButtons[i].enabled = true;
                        invenButtons[i].image.sprite = InventoryManager.Instance.GetSpriteBySpriteCode(amuletInventory3.amulets3[i].spriteCode);
                    }
                    else
                    {
                        invenButtons[i].GetComponent<Outline>().enabled = false;
                        invenButtons[i].enabled = false;
                        invenButtons[i].image.sprite = InventoryManager.Instance.GetSpriteBySpriteCode(0001);
                    }
                }
                break;
            case 3:
                AmuletInventory4 amuletInventory4 = DataManager.Instance.GetAmuletInventory4();
                for (int i = 0; i < invenButtons.Length; i++)
                {
                    if (i < amuletInventory4.amulets4.Count)
                    {
                        invenButtons[i].GetComponent<Outline>().enabled = amuletInventory4.amulets4[i].isEquiped;
                        invenButtons[i].enabled = true;
                        invenButtons[i].image.sprite = InventoryManager.Instance.GetSpriteBySpriteCode(amuletInventory4.amulets4[i].spriteCode);
                    }
                    else
                    {
                        invenButtons[i].GetComponent<Outline>().enabled = false;
                        invenButtons[i].enabled = false;
                        invenButtons[i].image.sprite = InventoryManager.Instance.GetSpriteBySpriteCode(0001);
                    }
                }
                break;
            case 4:
                AmuletInventory5 amuletInventory5 = DataManager.Instance.GetAmuletInventory5();
                for (int i = 0; i < invenButtons.Length; i++)
                {
                    if (i < amuletInventory5.amulets5.Count)
                    {
                        invenButtons[i].GetComponent<Outline>().enabled = amuletInventory5.amulets5[i].isEquiped;
                        invenButtons[i].enabled = true;
                        invenButtons[i].image.sprite = InventoryManager.Instance.GetSpriteBySpriteCode(amuletInventory5.amulets5[i].spriteCode);
                    }
                    else
                    {
                        invenButtons[i].GetComponent<Outline>().enabled = false;
                        invenButtons[i].enabled = false;
                        invenButtons[i].image.sprite = InventoryManager.Instance.GetSpriteBySpriteCode(0001);
                    }
                }
                break;
            case 5:
                AmuletInventory6 amuletInventory6 = DataManager.Instance.GetAmuletInventory6();
                for (int i = 0; i < invenButtons.Length; i++)
                {
                    if (i < amuletInventory6.amulets6.Count)
                    {
                        invenButtons[i].GetComponent<Outline>().enabled = amuletInventory6.amulets6[i].isEquiped;
                        invenButtons[i].enabled = true;
                        invenButtons[i].image.sprite = InventoryManager.Instance.GetSpriteBySpriteCode(amuletInventory6.amulets6[i].spriteCode);
                    }
                    else
                    {
                        invenButtons[i].GetComponent<Outline>().enabled = false;
                        invenButtons[i].enabled = false;
                        invenButtons[i].image.sprite = InventoryManager.Instance.GetSpriteBySpriteCode(0001);
                    }
                }
                break;
            case 6:
                WeaponInventory weaponInventory = DataManager.Instance.GetWeaponInventory();
                for (int i = 0; i < invenButtons.Length; i++)
                {
                    if (i < weaponInventory.weapons.Count)
                    {
                        invenButtons[i].GetComponent<Outline>().enabled = weaponInventory.weapons[i].isEquiped;
                        invenButtons[i].enabled = true;
                        invenButtons[i].image.sprite = InventoryManager.Instance.GetSpriteBySpriteCode(weaponInventory.weapons[i].spriteCode);
                    }
                    else
                    {
                        invenButtons[i].GetComponent<Outline>().enabled = false;
                        invenButtons[i].enabled = false;
                        invenButtons[i].image.sprite = InventoryManager.Instance.GetSpriteBySpriteCode(0001);
                    }
                }
                break;
            case 7:
                ShieldInventory shieldInventory = DataManager.Instance.GetShieldInventory();
                for (int i = 0; i < invenButtons.Length; i++)
                {
                    if (i < shieldInventory.shields.Count)
                    {
                        invenButtons[i].GetComponent<Outline>().enabled = shieldInventory.shields[i].isEquiped;
                        invenButtons[i].enabled = true;
                        invenButtons[i].image.sprite = InventoryManager.Instance.GetSpriteBySpriteCode(shieldInventory.shields[i].spriteCode);
                    }
                    else
                    {
                        invenButtons[i].GetComponent<Outline>().enabled = false;
                        invenButtons[i].enabled = false;
                        invenButtons[i].image.sprite = InventoryManager.Instance.GetSpriteBySpriteCode(0001);
                    }
                }
                break;
        }
        InventoryManager.Instance.SetInventoryActive(true);
    }
}