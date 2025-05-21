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
        // �Է� ����
        moveInput = Input.GetAxisRaw("Horizontal");

        // ���� �Է�
        if (Input.GetButtonDown("Jump") && jumped < jumpCount)
        {
            jump = true;
            jumped++;
        }

        // �ٴ� üũ
        if (Physics2D.Raycast(Feet.transform.position, Vector2.down, 0.1f, groundLayer))
        {
            isGrounded = true;
            jumped = 0;
            
        } else {
            isGrounded = false;
        }

        //�ٴ� ��������
        if (Input.GetKeyDown(KeyCode.S) && isGrounded) {
            StartCoroutine(FallThroughPlatform());
        }

        //���� ��: �и�, ����(���콺 Ŭ��), �뽬?
    }
    
    IEnumerator FallThroughPlatform()
    {
        Physics2D.IgnoreCollision(cd, floor, true);
        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreCollision(cd, floor, false);
    }

    void FixedUpdate()
    {
        // �̵� ����
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocityY);

        //���� ����
        if (jump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
            jump = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //'Flat' tag�� �ִ� ������ �Ʒ��� ����� �� �ִ�.
        if (collision.gameObject.CompareTag("Flat"))
        {
            floor = collision.collider;
        }
    }
}
