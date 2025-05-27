using UnityEngine;
using UnityEngine.UI;

public class InventoryButtonManager : MonoBehaviour
{
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

    void OnButtonClicked(int buttonIndex)
    {
        Debug.Log("클릭한 버튼 번호는 " + buttonIndex);
    }
}