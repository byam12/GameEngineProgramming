using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class GeminiChat : MonoBehaviour
{
    [Header("Gemini2.0 flash API Key")]
    public string apiKey = "AIzaSyCJsIsZdQpYYUegM_9bO5UXKKtBARP60lk";

    [TextArea]
    public string userInput = @"
당신은 엘렌이라는 NPC입니다. 말투는 고상하고 조용하며, 대화 시 항상 비유를 섞습니다.

게임 정보:
- 게임 시간: 3일차
- 플레이어 직업: 전사
- 진행 지역: 엘다 숲
- 현재 퀘스트: 사라진 유물 찾기
- 소지 아이템: 고대의 열쇠, 해독제

플레이어가 묻습니다: 엘렌, 이 유물은 어디서 찾아야 하나요?";

    public void requestGemini(){
        StartCoroutine(SendGeminiRequest(userInput));
    }

    IEnumerator SendGeminiRequest(string prompt)
    {
        string url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={apiKey}";

        // 요청 JSON 만들기
        GeminiRequest req = new GeminiRequest
        {
            contents = new Content[]
            {
                new Content
                {
                    parts = new Part[]
                    {
                        new Part { text = prompt }
                    }
                }
            }
        };

        string jsonBody = JsonUtility.ToJson(req);
        Debug.Log("요청 JSON: " + jsonBody);

        // 요청 전송
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("응답: " + request.downloadHandler.text);
            GeminiResponse response = JsonUtility.FromJson<GeminiResponse>(request.downloadHandler.text);
            if (response != null && response.candidates.Length > 0)
            {
                string reply = response.candidates[0].content.parts[0].text;
                Debug.Log("Gemini 응답: " + reply);
            }
        }
        else
        {
            Debug.LogError("요청 실패: " + request.error);
        }
    }

    // --- JSON 클래스 정의 ---
    [System.Serializable]
    public class Part
    {
        public string text;
    }

    [System.Serializable]
    public class Content
    {
        public Part[] parts;
    }

    [System.Serializable]
    public class GeminiRequest
    {
        public Content[] contents;
    }

    // 응답 파싱용
    [System.Serializable]
    public class GeminiResponse
    {
        public Candidate[] candidates;
    }

    [System.Serializable]
    public class Candidate
    {
        public Content content;
    }
}