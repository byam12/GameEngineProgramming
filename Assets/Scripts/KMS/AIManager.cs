using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class GeminiChat : MonoBehaviour
{
    private int gameOverCount;//지금까지 몇번 실패했는지
    private int lastStageInfo;//이전 게임에서 스테이지 몇에서 게임오버했는지.

    [Header("Gemini2.0 flash API Key")]
    public string apiKey = "AIzaSyCJsIsZdQpYYUegM_9bO5UXKKtBARP60lk";
    private string inputText = @"";
    public void SetGameOverCount(int gameOverCount){
        this.gameOverCount = gameOverCount;
    }
    public void SetLastStageInfo(int lastStageInfo){
        this.lastStageInfo = lastStageInfo;
    }
    void Start()
    {
        SetGameOverCount(6);
        SetLastStageInfo(7);
        RequestGemini();
    }
    public void RequestGemini(){ //inputText만들고 요청하기
        inputText = @$"
        게임오버횟수:{gameOverCount}번
        직전스테이지:{lastStageInfo}층
        npc정보:
        (세계관 배경은 중세판타지이고, 계속해서 도전하는 플레이어에게 항상 문을 열어주는 문지기, 장난끼 많고 놀리는걸 좋아하여 실패한 플레이어를 놀린다.
        갑옷으로 온몸을 가린 남성이며 누구에게나 반말을 쓴다.)
        응답형식:
        (npc의 입장에서 대답을 해줘야 한다. 또한 대답 형식은 여러줄로 해주어야 하고 줄바꿈은 \n으로 해줘야 한다.
        게임오버횟수만큼 실패하였다는 언급, 직전스테이지만큼의 층까지 도달하였다는 언급은 필수이다. 
        직저스테이지에 따라 어느정도 잘했는지 못했는지에 대한 반응을 보여준다.(던전은 총 9층으로 이에 대한 평가를 해준다. 다만 던전이 9층이라는 것은 비밀이다.)
        직전스테이지가 8,9층의 경우 놀리다가도 그 직후에 너라면 할 수 있을거라는 격려를 해준다.
        그리고 대화의 마지막엔 이제 문을 열어주겠다는 언급과 함께 항상 행운을 빈다고 말해줘야 한다.)";
        StartCoroutine(SendGeminiRequest(inputText));
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