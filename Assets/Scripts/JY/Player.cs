using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{ 
    [SerializeField] float hp = 100f;
    [SerializeField] int level = 1; // level of Weapon
    [SerializeField] float speed = 4.0f;
    [SerializeField] float jumpForce = 7.5f;
    [SerializeField] float rollForce = 9.0f;
    [SerializeField] int jumpCount = 2;
    [SerializeField] LayerMask groundLayer;


    private Animator animator;
    private Rigidbody2D rb;
    private Collider2D cd;

    private int facingDir = 1;
    private bool grounded = false;
    private Collider2D floor;
    private int jumped = 0;
    private bool rolling = false;
    private float rollDuration = 8.0f / 14.0f;
    private float rollCurrentTime;
    private int currentAttack = 0;
    private float timeSinceAttack = 0.0f;
    private float exp = 0f;
    private bool superarmor = false;
    private bool isBlocking = false;
    private bool isAttackking = false;
    private float delayToIdle = 0.0f;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<Collider2D>();
    }

    void Update()
    {
        // Level up
        // It is temp code
        if (level < 10 && exp >= 100f)
        {
            exp -= 100f;
            level++;
        } 

        // Timer for attack combo
        timeSinceAttack += Time.deltaTime;

        // Timer for checking roll duration
        if (rolling)
            rollCurrentTime += Time.deltaTime;

        if (rollCurrentTime > rollDuration)
        {
            rolling = false;
            superarmor = false;
            rollCurrentTime = 0;
        }

        // Check if player is falling
        if (!grounded)
        {
            animator.SetBool("Grounded", grounded);
        }

        // Check for perfect block
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Block"))
        {
            superarmor = true;
        }
        else
        {
            superarmor = false;
        }

        // Check for attacking
        if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Attack"))
        {
            isAttackking = true;
        }
        else
        {
            isAttackking = false;
        }

        float inputX = Input.GetAxisRaw("Horizontal");

        // Swap direction of sprite depending on walk direction
        // 1 for right, -1 for left
        if (inputX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            facingDir = 1;
        }

        else if (inputX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            facingDir = -1;
        }

        // Move player
        if (!rolling)
        {
            if (!isBlocking)
            {
                float weight = isAttackking ? 0.05f : 1;
                rb.linearVelocity = new Vector2(inputX * speed * weight, rb.linearVelocity.y);
            }
        }
        // Set AirSpeed in animator
        animator.SetFloat("AirSpeedY", rb.linearVelocity.y);

        // Go down when player is on 'Flat' floor
        if (Input.GetKeyDown("s") && grounded) StartCoroutine("FallThroughPlatform");


        // Death
        if (hp <= 0)
        {
            animator.SetTrigger("Death");

            // Need logic for game end!!
        }

        // Attack
        else if (Input.GetMouseButtonDown(0) && timeSinceAttack > 0.25f && !rolling)
        {
            currentAttack++;
            isAttackking = true;

            // Loop back to one after third attack
            if (currentAttack > 3)
                currentAttack = 1;

            // Reset Attack combo if time since last attack is too large
            if (timeSinceAttack > 1.0f)
                currentAttack = 1;

            // "Attack1" or "Attack2" or "Attack3"
            animator.SetTrigger("Attack" + currentAttack);
            

            // Reset timer
            timeSinceAttack = 0.0f;
            isAttackking = false;
        }

        // Block
        else if (Input.GetMouseButtonDown(1) && !rolling)
        {
            rb.linearVelocityX *= 0.5f;
            isBlocking = true;
            animator.SetTrigger("Block");
            animator.SetBool("IdleBlock", true);
        }

        else if (Input.GetMouseButtonUp(1))
        {
            animator.SetBool("IdleBlock", false);
            isBlocking = false;
        }

        // Roll
        else if (Input.GetKeyDown("left shift") && !rolling)
        {
            rolling = true;
            superarmor = true;
            animator.SetTrigger("Roll");
            rb.linearVelocityX = facingDir * rollForce;
        }

        // Jump
        // Player can jupe up to jumpCount
        else if (Input.GetKeyDown("space") && jumped < jumpCount && !rolling)
        {
            jumped++;
            animator.SetTrigger("Jump");
            animator.SetBool("Grounded", grounded);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            grounded = false;
        }

        // Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            // Reset timer
            delayToIdle = 0.1f;
            animator.SetInteger("AnimState", 1);
        }

        // Idle
        else
        {
            // Prevents flickering transitions to idle
            delayToIdle -= Time.deltaTime;
            if (delayToIdle < 0)
                animator.SetInteger("AnimState", 0);
        }
    }

    // To go down, ignore collision beween player and 'Flat' floor for 0.5s
    IEnumerator FallThroughPlatform()
    {
        Physics2D.IgnoreCollision(cd, floor, true);
        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreCollision(cd, floor, false);
    }

    // Check if player is on the ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((groundLayer.value & (1 << collision.gameObject.layer)) != 0)
        {
            if (Mathf.Abs(rb.linearVelocityY) < 0.01f)
            {
                grounded = true;
                jumped = 0;
                animator.SetBool("Grounded", grounded);

                if (collision.gameObject.CompareTag("Flat"))
                {
                    floor = collision.collider;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if ((groundLayer.value & (1 << collision.gameObject.layer)) != 0)
        {
            grounded = false;
        }
    }

    // Hurt
    // Check if attacked by monsters
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Hit") && !superarmor)
        {
            hp -= 10; // temp value
            animator.SetTrigger("Hurt");
        }
    }

    // Get whether player is looking right side
    public int isRight()
    {
        return facingDir;
    }

    // get player's current level
    public int getLevel()
    {
        return level;
    }

    // Give plaeyr to exp
    public void addExp(float getExp)
    {
        exp += getExp;
    }

    // Check if player is able to be attacked
    // 0: possible, 1: avoid, 2: blocked, 3: parrying
    public int getStatus()
    {
        if (superarmor)
        {
            if (isBlocking) return 3;
            else return 1;
        } else if (isBlocking) return 2;
        else return 0;
    }
}
