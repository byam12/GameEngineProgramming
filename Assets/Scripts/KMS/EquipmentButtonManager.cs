using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentButtonManager : MonoBehaviour
{
    [SerializeField] InventoryButtonManager inventoryButtonManager;
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
    void OnButtonClicked(int buttonIndex)
    {
        Debug.Log("클릭한 버튼 번호는 " + buttonIndex);
        switch (buttonIndex)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                WeaponInventory inventory = DataManager.Instance.GetWeaponInventory();
                Button[] invenButtons = inventoryButtonManager.GetInventoryButtons();
                for (int i = 0; i < invenButtons.Length; i++)
                {
                    if (i < inventory.weapons.Count)
                    {
                        invenButtons[i].enabled = true;
                        invenButtons[i].image.sprite = InventoryManager.Instance.GetSpriteBySpriteCode(inventory.weapons[i].spriteCode);
                    }
                    else
                    {
                        invenButtons[i].enabled = false;
                    }
                }
                break;
            case 7:
                break;
        }
        InventoryManager.Instance.SetInventoryActive(true);
    }
}