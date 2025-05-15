using TMPro;
using UnityEngine;

public class InputEnterUpdate : MonoBehaviour
{
    void Update(){
        // Enter 감지
        if (Input.GetKeyDown(KeyCode.KeypadEnter)){
            dialogBarManager.Instance.InputEnter();
        }
    }
}
