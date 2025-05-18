using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum BossState { Idle, Attack, Groggy, Parried }

[System.Serializable]
public class Attack
{
    [Header("���� �̸�? �ʿ��Ѱ� ")]
    public string name;

    [Header("Animator Ʈ���� �̸�")]
    public string trigger;

    [Header("���ݹ��� ")]
    public float hitRange = 1f;

    [Header("���� �� �߰� ��� ȿ�����̳�, � ȿ�� ���� �ֱ�")]
    public UnityEvent onAttackEvent;

    [Header("���� ���ݵ�")]
    public List<Attack> nextAttacks = new List<Attack>();
}

public class BossTemplate : MonoBehaviour
{
    [Header("ù ���� ������ �͵� �ֱ�")]
    public List<Attack> initialAttacks = new List<Attack>();

    [Header("�������Ȯ�ο�")]
    public BossState state = BossState.Idle;

    [Header("�ִϸ����� �ֱ� �ȿ� �ִϸ��̼ǵ�")]
    public Animator animator;

    [Header("ü���̶� �и�������?")]
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
