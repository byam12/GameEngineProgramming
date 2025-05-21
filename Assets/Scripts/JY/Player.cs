using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public int jumpCount = 2;

    [Header("Ground Check")]
    public LayerMask groundLayer;
    [SerializeField]
    private GameObject Feet;

    [Header("Weapon")]
    public GameObject weapon;
    public GameObject effect;

    private Rigidbody2D rb;
    private Collider2D cd;

    private float moveInput;
    private bool isGrounded = false;
    private bool jump;
    private int jumped = 0;
    private Collider2D floor;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<Collider2D>();
    }

    void Update()
    {
        // 입력 감지
        moveInput = Input.GetAxisRaw("Horizontal");

        // 점프 입력
        if (Input.GetButtonDown("Jump") && jumped < jumpCount)
        {
            jump = true;
            jumped++;
        }

        // 바닥 체크
        if (Physics2D.Raycast(Feet.transform.position, Vector2.down, 0.1f, groundLayer))
        {
            isGrounded = true;
            jumped = 0;
            
        } else {
            isGrounded = false;
        }

        //바닥 내려가기
        if (Input.GetKeyDown(KeyCode.S) && isGrounded) {
            StartCoroutine(FallThroughPlatform());
        }

        //만들 것: 패링, 공격(마우스 클릭), 대쉬?
    }
    
    IEnumerator FallThroughPlatform()
    {
        Physics2D.IgnoreCollision(cd, floor, true);
        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreCollision(cd, floor, false);
    }

    void FixedUpdate()
    {
        // 이동 적용
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocityY);

        //점프 실행
        if (jump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
            jump = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //'Flat' tag가 있는 지형만 아래로 통과할 수 있다.
        if (collision.gameObject.CompareTag("Flat"))
        {
            floor = collision.collider;
        }
    }
}
