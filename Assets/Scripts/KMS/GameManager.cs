using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
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
    public void InputKey_I()
    {
        InventoryManager instance = InventoryManager.Instance;
        if (instance.GetInventoryActive())
        {
            instance.SetInventoryActive(false);
        }
        else
        {
            instance.SetInventoryActive(true);
        }

        // I 입력 시 로딩 중인가, 강화 중인가, 일시 정지 중인가 등 입력불가 판별해 입력 받을지 말지 걸러주는 부분 추가 필요
        // inventory 창 열거나 닫아주기
    }

}
