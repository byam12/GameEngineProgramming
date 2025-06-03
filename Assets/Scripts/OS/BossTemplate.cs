using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;
using UnityEngine.Scripting.APIUpdating;

public enum BossState
{
    Idle,
    Attack,
    Parried,
    Groggy
}

public interface IBossCommand
{
    IEnumerator Execute(BossTemplate boss);
}

[CreateAssetMenu(menuName = "Boss/Commands/AttackCommand")]
public class BossCommand : ScriptableObject, IBossCommand
{
    public string trigger;
    public int hitboxIndex;
    public UnityEvent onAttackEvent;
    public List<IBossCommand> nextCommands = new List<IBossCommand>();

    public IEnumerator Execute(BossTemplate boss)
    {
        if (trigger != null)
        { boss.Animator.SetTrigger(trigger); }
        onAttackEvent.Invoke();
        if (hitboxIndex != null)
        { boss.HitboxOn(hitboxIndex); }

        Debug.Log("trigger : " + trigger + " index is : " + hitboxIndex);
        AnimatorClipInfo[] clipInfos = boss.Animator.GetCurrentAnimatorClipInfo(0);
        float waitTime = 1f;
        if (clipInfos.Length > 0)
        {
            waitTime = clipInfos[0].clip.length;
        }
        
        yield return new WaitForSeconds(waitTime);

        if (boss.IsParried || boss.IsGroggy)
            yield break;

        if (nextCommands != null && nextCommands.Count > 0)
        {
            int choice = Random.Range(-1, nextCommands.Count);
            if (choice >= 0)
            {
                IBossCommand next = nextCommands[choice];
                yield return boss.StartCoroutine(next.Execute(boss));
            }
            else
            {
                Debug.Log("end pattern");
            }
        }

        Debug.Log("end attack patterns");

    }
}

public class BossTemplate : MonoBehaviour
{
    public List<ScriptableObject> initialCommands = new List<ScriptableObject>();
    public Animator Animator;
    public float stamina = 100f;
    public float groggyThreshold;
    public BossState state;
    private Coroutine routine;

    public List<GameObject> hitbox = new List<GameObject>();
    public bool IsParried { get; private set; }
    public bool IsGroggy { get; private set; }

    private const string WalkParam = "IsWalking";
    public bool running = false;
    public bool moving = false;

    public float attackCoolTiem = 2f;
    public float nextPatternPossible = 0f;

    public float cognitionDistance = 3f;

    void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    void Update()
    {

        if ((this.transform.position - PlayerPosition()).magnitude > 5f && !running && !moving)
            {
                SetIdle();
            }




        if (state == BossState.Idle && isPlayerClose())
        {
            //SetIdle();
            StartPattern();
        }
        else if (state == BossState.Idle && !isPlayerClose())
        {
            Debug.Log("too far");
            Move();
        }

        if(state == BossState.Attack && isPlayerClose() && Time.time > nextPatternPossible)
        {
            if(!running)
            StartPattern();
            else
                return;
        }
        if (state == BossState.Attack && CheckParryInput())
        {
            IsParried = true;
        }
    }

    bool isPlayerClose()
    {
        return Vector3.Distance(transform.position, PlayerPosition()) < cognitionDistance;
    }

    bool CheckParryInput()
    {
        return false; // change
    }

    private void Move()
    {
        moving = true;
        Animator.SetBool(WalkParam, true);
        if(!running)
        transform.position = Vector3.MoveTowards(transform.position, PlayerPosition(), Time.deltaTime);
    }

    void StartPattern()
    {
        if (running)
            return;

        if (routine != null)
        {
            StopCoroutine(routine);
        }
        routine = StartCoroutine(PatternRoutine());
    }

    private IEnumerator PatternRoutine()
    {
        running = true;
        moving = false;
        Animator.SetBool(WalkParam, false);
        state = BossState.Attack;
        IsParried = IsGroggy = false;

        if (initialCommands.Count > 0)
        {
            int idx = Random.Range(0, initialCommands.Count);
            if (initialCommands[idx] is IBossCommand cmd)
                yield return StartCoroutine(cmd.Execute(this));
        }
        running = false;
        nextPatternPossible = Time.time + attackCoolTiem;
        SetIdle();
    }


    public void HitboxOn(int hitboxIndex)
    {
        hitbox[hitboxIndex].SetActive(true);
        Debug.Log(hitboxIndex + " hitbox should be active");
    }

    public void EnterParried()
    {
        if (routine != null)
        { 
            StopCoroutine(routine);
            routine = null;
        }
        running = false;
        moving = false;

        state = BossState.Parried;
        Animator.SetTrigger("Parried");
        IsParried = true;
    }

    public void EnterGroggy()
    {
        if (routine != null)
        { 
            StopCoroutine(routine);
            routine = null;
        }

        running = false;
        moving=false;

        state = BossState.Groggy;
        Animator.SetTrigger("Groggy");
        StartCoroutine(Wakeup());
        IsGroggy = true;
    }

    IEnumerator Wakeup()
    {
        yield return new WaitForSeconds(2f);
        stamina = 100f;
        
        Animator.SetBool(WalkParam, false);
        IsParried = false;
        IsGroggy = false;

        nextPatternPossible = Time.time + attackCoolTiem;
        SetIdle() ;
    }

    public Vector3 PlayerPosition()
    {
        // change
        return Vector3.zero;
    }

    public void SetIdle()
    {
        state = BossState.Idle;
        Animator.SetTrigger("Idle");
        Animator.SetBool(WalkParam, false);
        Debug.Log("setIdle");
        moving = false;
        running = false;
    }
}
