using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float xMargin = 9;
    [SerializeField] private float yMargin = 7; // Bottom margin is fixed;
    private Transform player;
    float width;
    float height;

    private void Start()
    {
        player = FindAnyObjectByType<Player>().gameObject.transform;

        if (player == null)
        {
            Debug.LogError("플레이어를 찾을 수 없습니다.");
            
        }
        height = Camera.main.orthographicSize;
        width = height * Camera.main.aspect;
    }
    private void Update()
    {
        float clampedX = Mathf.Clamp(player.position.x, width + xMargin * -1, xMargin - width);
        float clampedY = Mathf.Clamp(player.position.y, height - 5, yMargin - height);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
