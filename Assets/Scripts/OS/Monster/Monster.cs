using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Monster : MonoBehaviour
{
    [Header("patrorl range, put hte empty object")]
    public Transform leftPoint;
    public Transform rightPoint;
    public float patrolSpeed = 2f;

    [Header("chadse")]
    public float sightRange = 6f; // 플레이어 감지 범위
    public float chaseSpeed = 3f;

    [Header("attack")]
    public float attackRange = 1.2f;
    public float attackCooldown = 1.0f;
    public string attackTrigger = "Attack";
    public int hitboxIndex = 0;
    public UnityEvent onStrikeEvent; 

    enum State { Patrol, Chase, Attack }
    State state = State.Patrol;
    bool facingLeft;
    float nextAttackTime;

    Animator anim;
    Rigidbody2D rb;
    public List<GameObject> hitboxes = new();

    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        foreach (Transform t in GetComponentsInChildren<Transform>(true))
            if (t.CompareTag("Hitbox")) hitboxes.Add(t.gameObject);
    }

    void Update()
    {
        Vector3 playerPos = GetPlayerPos();
        float distX = playerPos.x - transform.position.x;
        float distAbs = Mathf.Abs(distX);

        if (distX > 0.1f) Face(false); 
        else if (distX < -0.1f) Face(true); 

        switch (state)
        {
            case State.Patrol:
                Patrol();
                if (distAbs < sightRange) state = State.Chase;
                break;

            case State.Chase:
                Chase(playerPos);
                if (distAbs <= attackRange && Time.time >= nextAttackTime)
                    state = State.Attack;
                else if (distAbs > sightRange * 1.2f)
                    state = State.Patrol;
                break;

            case State.Attack:
                StartCoroutine(AttackRoutine());
                break;
        }
    }

    void Patrol()
    {
        float dir = facingLeft ? -1f : +1f;
        rb.velocity = new Vector2(dir * patrolSpeed, rb.velocity.y);

        if (transform.position.x < leftPoint.position.x) Face(false);
        if (transform.position.x > rightPoint.position.x) Face(true);

        anim.SetBool("Walk", true);
    }

    void Chase(Vector3 target)
    {
        float dir = facingLeft ? -1f : +1f;
        rb.velocity = new Vector2(dir * chaseSpeed, rb.velocity.y);
        anim.SetBool("Walk", true);
    }

    IEnumerator AttackRoutine()
    {
        state = State.Attack;
        rb.velocity = Vector2.zero; 
        anim.SetBool("Walk", false);

        anim.SetTrigger(attackTrigger);
        onStrikeEvent?.Invoke();
        if (hitboxIndex >= 0 && hitboxIndex < hitboxes.Count)
            hitboxes[hitboxIndex].SetActive(true);

        yield return new WaitForSeconds(GetCurrentClipLength());

        nextAttackTime = Time.time + attackCooldown;
        state = State.Chase;  
    }

    float GetCurrentClipLength()
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        if (info.length > 0f)
            return info.length / Mathf.Max(info.speed, 0.01f);
        return 0.5f;
    }

    void Face(bool toLeft)
    {
        if (facingLeft == toLeft) return;
        facingLeft = toLeft;

        Vector3 s = transform.localScale;
        s.x = -s.x;
        transform.localScale = s;
    }

    Vector3 GetPlayerPos()
    {
        Player p = FindAnyObjectByType<Player>();
        if (p != null) 
            return p.transform.position;
        return transform.position; 
    }
}
