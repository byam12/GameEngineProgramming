using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    public int hitboxIndex; // i will add mulitple hitboxes in boss' child, then assign index of hitbox to be active
    public UnityEvent onAttackEvent;
    public List<AttackCommand> nextCommands = new List<AttackCommand>();

    public IEnumerator Execute(BossTemplate boss)
    {
        boss.Animator.SetTrigger(trigger);
        onAttackEvent.Invoke();
        boss.HitboxOn(hitboxIndex);

        yield return new WaitUntil(() =>
            boss.Animator.GetCurrentAnimatorStateInfo(0).IsName(trigger) &&
            boss.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
        if (boss.IsParried) yield break;
        if (boss.IsGroggy) yield break;
        foreach (var next in nextCommands)
        {
            yield return boss.StartCoroutine(next.Execute(boss));
            if (boss.IsParried || boss.IsGroggy) yield break;
        }
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

    void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (state == BossState.Idle && CanStartAttack())
        {
            StartPattern();
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
        return false;
    }

    void StartPattern()
    {
        state = BossState.Attack;
        IsParried = false;
        IsGroggy = false;
        foreach (var obj in initialCommands)
        {
            if (obj is IBossCommand cmd)
            {
                routine = StartCoroutine(cmd.Execute(this));
                break;
            }
        }
    }

    public void HitboxOn(int hitboxIndex)
    {
        hitbox[hitboxIndex].gameObject.SetActive(true);
        //in hit box script -> onenable -> coroutine, wait some times, then check hit, then set active false
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
        IsParried = false;
        IsGroggy = false;
    }

    Vector3 PlayerPosition()
    {
        //change
        return Vector3.zero;
    }
}
