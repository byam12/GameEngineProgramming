using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 4.0f;
    [SerializeField] float jumpForce = 7.5f;
    [SerializeField] float rollForce = 9.0f;
    [SerializeField] int jumpCount = 2;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image manaBar;
    public GameObject basicSkill;


    private Animator animator;
    private Rigidbody2D rb;
    private Collider2D cd;

    private int facingDir = 1;
    private bool dead = false;
    private bool grounded = false;
    private Collider2D floor;
    private int jumped = 0;
    private bool rolling = false;
    private float rollDuration = 8.0f / 14.0f;
    private float rollCurrentTime;
    private int currentAttack = 0;
    private float timeSinceAttack = 0.0f;
    private bool extra = false;
    private float extraCurrentTime;
    private bool superarmor = false;
    private bool isBlocking = false;
    private bool isAttackking = false;
    private float delayToIdle = 0.0f;
    private bool isGate = false;
    private bool isNPC = false;
    private bool isItem = false;
    

    //temp values
    private float maxHP = 100;
    private float hp = 100;
    private float maxMP = 100;
    private float mp = 100;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<Collider2D>();
        healthBar = GameObject.Find("HealthBar").GetComponent<Image>();
        manaBar = GameObject.Find("ManaBar").GetComponent<Image>();
        //temp code
        UpdateHP(hp, maxHP);
        UpdateMP(mp, maxMP);
    }

    void Update()
    {
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

        // Timer for checking skill duration
        if (extra)
        {
            extraCurrentTime += Time.deltaTime;
        }

        if ( extraCurrentTime > 1.5f)
        {
            extra = false;
            extraCurrentTime = 0;
        }

        // Death
        if (hp == 0 && !dead) //temp condition
        {
            dead = true;
            animator.SetTrigger("Death");

            // Need logic for game end!!
            StartCoroutine("Return");
        }
        else if (!dead)
        {

            // Check if player is falling
            if (!grounded)
            {
                animator.SetBool("Grounded", grounded);
            }

            // Check for perfect block or roll or hurt
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Block") || animator.GetCurrentAnimatorStateInfo(0).IsName("Roll") || animator.GetCurrentAnimatorStateInfo(0).IsName("Hurt"))
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

            // Go through the 'Gate'
            if (Input.GetKeyDown("w") && isGate) Debug.Log("enter"); //temp code

            // Open the Inventory
            if (Input.GetKeyDown("i")) { Debug.Log("gate opens"); } //temp code

            // Interact with NPC or item
            if (Input.GetKeyDown("f"))
            {
                if (isNPC)
                {
                    //Need to add
                    Debug.Log("NPC");
                }
                else if (isItem)
                {
                    //Need to add
                    Debug.Log("item");
                }
            }

            // Attack
            if (Input.GetMouseButtonDown(0) && timeSinceAttack > 0.25f && !rolling)
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

            // Use skill
            else if (Input.GetKeyDown("e") && !extra)
            {
                animator.SetTrigger("AttackExtra");
                extra = true;

                //temp code;
                UpdateMP(mp -= 5, maxMP);
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
    }

    // To go down, ignore collision beween player and 'Flat' floor for 0.5s
    IEnumerator FallThroughPlatform()
    {
        Physics2D.IgnoreCollision(cd, floor, true);
        yield return new WaitForSeconds(0.6f);
        Physics2D.IgnoreCollision(cd, floor, false);
    }

    // Return to town
    IEnumerator Return()
    {
        yield return new WaitForSeconds(3f);
        dead = false;
        animator.SetTrigger("Revive");
        UpdateHP(hp = maxHP, maxHP);
        UpdateMP(mp = maxMP, maxMP);
        //do something
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
    }

    // Check if player is on the ground
    private void OnCollisionStay2D(Collision2D collision)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Hit") && !superarmor && !dead)  // Hurt: Check if attacked by monsters
        {
            hurt(10); //temp damage value
            animator.SetTrigger("Hurt");
        }
        else if (collision.CompareTag("Spike") && !superarmor && !dead)  // Hurt: Check if attacked by spikes
        {
            hurt(5);
            jumped = jumpCount;
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce * 0.75f);
            grounded = false;
            animator.SetTrigger("Hurt");
        } 
        else if (collision.CompareTag("Gate")) // Check for gate, npc, item
        {
            isGate = true;
        }
        else if (collision.CompareTag("NPC"))
        {
            isNPC = true;
        }
        else if (collision.CompareTag("Item"))
        {
            isItem = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Gate"))
        {
            isGate = false;
        }
        else if (collision.CompareTag("NPC"))
        {
            isNPC = false;
        }
        else if (collision.CompareTag("Item"))
        {
            isItem = false;
        }

    }

    // Update health_bar
    private void UpdateHP(float current, float max)
    {
        if (dead) return;

        float ratio;

        if (current <= 0 && !dead)
        {
            //current = 0;
            hp = 0;
            current = hp;

        }
        ratio = current / max;

        healthBar.rectTransform.localPosition = new Vector3(healthBar.rectTransform.rect.width * ratio - healthBar.rectTransform.rect.width, 0, 0);
    }

    // Update mana_bar
    private void UpdateMP(float current, float max)
    {
        float ratio;

        if (current <= 0)
        {
            ratio = 0;
        }
        else ratio = current / max;

        manaBar.rectTransform.localPosition = new Vector3(manaBar.rectTransform.rect.width * ratio - manaBar.rectTransform.rect.width, 0, 0);
    }

    // Get whether player is looking right side
    public int isRight()
    {
        return facingDir;
    }

    // Notify the inventory that the player is injured
    public void hurt(float damage)
    {
        // Need to add!!
        UpdateHP(hp -= damage, maxMP);
    }

    // Check if player is able to be attacked
    // 0: possible, 1: avoid, 2: blocked, 3: parrying
    public int getStatus()
    {
        if (superarmor)
        {
            if (isBlocking) return 3;
            else return 1;
        }
        else if (isBlocking) return 2;
        else return 0;
    }

}
