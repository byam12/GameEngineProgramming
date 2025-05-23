using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class dialogBarManager : MonoBehaviour
{
    public static dialogBarManager Instance { get; private set; }
    [SerializeField] private GameObject dialogBar;
    [SerializeField] public TMP_InputField inputField;
    [SerializeField] private TMP_Text responseText;
    private string[] strArr;
    private int strIndex = 0;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // 중복 방지
        }
    }
    public void SetDialogBar(bool show)
    {
        dialogBar.SetActive(show);
    }
    void Start()
    {
        inputField.onSubmit.AddListener(HandleSubmit);
    }
    public void HandleSubmit(string text)
    {
        if (!string.IsNullOrWhiteSpace(text))
        {
            AIManager.Instance.SetUsetInputAndRequest(inputField.text);

            inputField.text = ""; //입력창 비우기
            StartCoroutine(ReactivateInputField());
        }
        else
        {
            printNextResponse();
        }
    }
    IEnumerator ReactivateInputField()
    {
        yield return null; // 한 프레임 대기
        if (inputField.isFocused == false)
        {
            inputField.ActivateInputField();
        }
    }

    public void PrintResponse(string input)
    {
        strArr = SplitByNewline(input);
        strIndex = 0;
        PrintTypewriter(strArr[strIndex]);
    }
    private void printNextResponse()
    {
        strIndex++;
        PrintTypewriter(strArr[strIndex]);
    }
    private string[] SplitByNewline(string input)
    {
        //문자열에서 \n 기준으로 나눔
        return input.Split(new string[] { @"\n" }, StringSplitOptions.None);
    }
    

    private Coroutine typingCoroutine;
    public void PrintTypewriter(string input, float delayPerChar = 0.03f)
    {
        // 기존 출력 중단 (겹치는 거 방지)
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeTextCoroutine(input, delayPerChar));
    }

    // 코루틴 본체
    private IEnumerator TypeTextCoroutine(string fullText, float delay)
    {
        responseText.text = "";
        foreach (char c in fullText)
        {
            responseText.text += c;
            yield return new WaitForSeconds(delay);
        }
    }
    public void InputEnter(){
        if (inputField.isFocused && !string.IsNullOrWhiteSpace(inputField.text))
        {
            HandleSubmit(inputField.text);
        
        }
    }
}
