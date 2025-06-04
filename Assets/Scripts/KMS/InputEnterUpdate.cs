using TMPro;
using UnityEngine;

public class InputEnterUpdate : MonoBehaviour
{
    void Update(){
        // Enter 감지 오로지 대화창의 enter 입력만을 위해 만들어진 스크립트
        if (Input.GetKeyDown(KeyCode.KeypadEnter)){
            dialogBarManager.Instance.InputEnter();
        }
    }
}
