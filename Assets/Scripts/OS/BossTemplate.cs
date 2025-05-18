using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum BossState { Idle, Attack, Groggy, Parried }

[System.Serializable]
public class Attack
{
    [Header("공격 이름? 필요한가 ")]
    public string name;

    [Header("Animator 트리거 이름")]
    public string trigger;

    [Header("공격범위 ")]
    public float hitRange = 1f;

    [Header("공격 시 추가 기능 효과음이나, 어떤 효과 여기 넣기")]
    public UnityEvent onAttackEvent;

    [Header("다음 공격들")]
    public List<Attack> nextAttacks = new List<Attack>();
}

public class BossTemplate : MonoBehaviour
{
    [Header("첫 공격 가능한 것들 넣기")]
    public List<Attack> initialAttacks = new List<Attack>();

    [Header("현재상태확인용")]
    public BossState state = BossState.Idle;

    [Header("애니메이터 넣기 안에 애니메이션도")]
    public Animator animator;

    [Header("체력이랑 패링게이지?")]
    public float stamina = 100f;
    public float groggyThreshold = 0f;

    private Coroutine comboRoutine;
    private bool isParried = false;
    private bool isGroggy = false;

    public void Update()
    {
        if (state == BossState.Idle && canStartAttack())
        {
            startAttackCombo();
        }

        if (state == BossState.Attack && checkParryInput())
        {
            isParried = true;
        }
    }

    public bool canStartAttack()
    {
        // need to change
        return Vector3.Distance(transform.position, playerPosition()) < 3f;
    }

    public bool checkParryInput()
    {
        //need to chang 
        return false;
    }

    public void startAttackCombo()
    {
        state = BossState.Attack;
        isParried = false;
        isGroggy = false;

        if (initialAttacks != null && initialAttacks.Count > 0)
        {
            var first = initialAttacks[Random.Range(0, initialAttacks.Count)];
            comboRoutine = StartCoroutine(doAttack(first));
        }
        else
        {
            state = BossState.Idle;
        }
    }

    public IEnumerator doAttack(Attack atk)
    {
        animator.SetTrigger(atk.trigger);

        atk.onAttackEvent?.Invoke();

        doHitCheck(atk.hitRange);

        yield return new WaitUntil(() =>
            animator.GetCurrentAnimatorStateInfo(0).IsName(atk.trigger) &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        if (isParried)
        {
            checkParry();
            yield break;
        }
        if (isGroggy)
        {
            checkgroggy();
            yield break;
        }

        if (atk.nextAttacks != null && atk.nextAttacks.Count > 0)
        {
            var next = atk.nextAttacks[Random.Range(0, atk.nextAttacks.Count)];
            yield return doAttack(next);
        }
        else
        {
            state = BossState.Idle;
        }
    }

    public void doHitCheck(float range)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range, LayerMask.GetMask("Player"));
        foreach (var c in hits)
        {
            //need to change 
        }
    }

    public void checkParry()
    {
        if (comboRoutine != null)
            StopCoroutine(comboRoutine);
        state = BossState.Parried;
        animator.SetTrigger("Parried");
    }

    public void checkgroggy()
    {
        if (comboRoutine != null)
            StopCoroutine(comboRoutine);
        state = BossState.Groggy;
        animator.SetTrigger("Groggy");
        StartCoroutine(wakeUp());
    }

    public IEnumerator wakeUp()
    {
        yield return new WaitForSeconds(2f);
        stamina = 100f;
        state = BossState.Idle;
    }

    public Vector3 playerPosition()
    {
        //need to change
        return Vector3.zero;
    }
}
