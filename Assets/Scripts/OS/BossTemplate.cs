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
public class AttackCommand : ScriptableObject, IBossCommand
{
    public string trigger;
    public int hitboxIndex;
    public UnityEvent onAttackEvent;
    public List<AttackCommand> nextCommands = new List<AttackCommand>();

    public IEnumerator Execute(BossTemplate boss)
    {
        boss.Animator.SetTrigger(trigger);
        onAttackEvent.Invoke();
        boss.HitboxOn(hitboxIndex);

        Debug.Log("trigger : " + trigger + " index is : " + hitboxIndex);

        yield return new WaitUntil(() =>
            boss.Animator.GetCurrentAnimatorStateInfo(0).IsName(trigger) &&
            boss.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        if (boss.IsParried || boss.IsGroggy)
            yield break;

        foreach (IBossCommand next in nextCommands)
        {
            yield return boss.StartCoroutine(next.Execute(boss));
            if (boss.IsParried || boss.IsGroggy)
                yield break;
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
    void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    void Update()
    {

        if ((this.transform.position - PlayerPosition()).magnitude > 5f)
            {
                SetIdle();
            }




        if (state == BossState.Idle && CanStartAttack())
        {
            //SetIdle();
            StartPattern();
        }
        else if (state == BossState.Idle && !CanStartAttack())
        {
            Debug.Log("too far");
            Move();
        }
        if (state == BossState.Attack && CheckParryInput())
        {
            IsParried = true;
        }
    }

    bool CanStartAttack()
    {
        return Vector3.Distance(transform.position, PlayerPosition()) < 3f;
    }

    bool CheckParryInput()
    {
        return false; // change
    }

    private void Move()
    {
        Animator.SetBool(WalkParam, true);
        transform.position = Vector3.MoveTowards(transform.position, PlayerPosition(), Time.deltaTime);
    }

    void StartPattern()
    {
        if (running)
            return;
        Animator.SetBool(WalkParam, false);

        state = BossState.Attack;
        IsParried = false;
        IsGroggy = false;
        running = true;
        foreach (var obj in initialCommands)
        {
            if (obj is IBossCommand cmd)
            {
                routine = StartCoroutine(cmd.Execute(this));
                //break;
            }
        }
        running = false;
        SetIdle();
    }

    public void HitboxOn(int hitboxIndex)
    {
        hitbox[hitboxIndex].SetActive(true);
        Debug.Log(hitboxIndex + " hitbox should be active");
    }

    public void EnterParried()
    {
        if (routine != null) StopCoroutine(routine);
        state = BossState.Parried;
        Animator.SetTrigger("Parried");
        IsParried = true;
    }

    public void EnterGroggy()
    {
        if (routine != null) StopCoroutine(routine);
        state = BossState.Groggy;
        Animator.SetTrigger("Groggy");
        StartCoroutine(Wakeup());
        IsGroggy = true;
    }

    IEnumerator Wakeup()
    {
        yield return new WaitForSeconds(2f);
        stamina = 100f;
        state = BossState.Idle;
        Animator.SetBool(WalkParam, false);
        IsParried = false;
        IsGroggy = false;
    }

    Vector3 PlayerPosition()
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
    }
}
