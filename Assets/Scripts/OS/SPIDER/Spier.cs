using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Spider/Pattern")]
public class SpiderCommand : ScriptableObject, IBossCommand
{
    public string trigger;
    public int hitboxIndex;
    public UnityEvent onAttackEvent;
    public List<SpiderCommand> nextCommands = new List<SpiderCommand>();
    public SpiderCommand determistic;

    public IEnumerator Execute(BossTemplate boss)
    {
        if (trigger != null)
        { boss.Animator.SetTrigger(trigger); }

        onAttackEvent.Invoke();
        if (hitboxIndex != null)
        { boss.HitboxOn(hitboxIndex); }

        AnimatorClipInfo[] clipInfos = boss.Animator.GetCurrentAnimatorClipInfo(0);
        float waitTime = 1f;
        if (clipInfos.Length > 0)
        {
            waitTime = clipInfos[0].clip.length;
        }

        yield return new WaitForSeconds(waitTime);

        if (boss.IsParried || boss.IsGroggy)
            yield break;
        if (determistic != null)

        { 
            yield return boss.StartCoroutine(determistic.Execute(boss)); 
        }
        else
        {
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
        }
        

        Debug.Log("end attack patterns");

    }
}

[CreateAssetMenu(menuName = "Spider/JumpUp")]
public class SpiderJumpUp : SpiderCommand
{
    public IEnumerator Execute(BossTemplate boss)
    {
        boss.Animator.SetTrigger(trigger);       
        AnimatorClipInfo[] clipInfos = boss.Animator.GetCurrentAnimatorClipInfo(0);
        float waitTime = 1f;
        if (clipInfos.Length > 0)
        {
            waitTime = clipInfos[0].clip.length;
        }

        yield return new WaitForSeconds(waitTime);

        //make boss invisible, unattackable

        if (boss.IsParried || boss.IsGroggy) yield break;

        if (determistic != null)
            yield return boss.StartCoroutine(determistic.Execute(boss));
    }
}

[CreateAssetMenu(menuName = "Spider/WaitFollow")]
public class SpiderWaitFollow : SpiderCommand
{
    public float followTime = 2f;
    public float followSpeed = 5f;

    public IEnumerator Execute(Spier boss)
    {
        float t = 0f;
        while (t < followTime)
        {
            t += Time.deltaTime;

            Vector3 target = boss.PlayerPosition();
            boss.transform.position = Vector3.MoveTowards(
                boss.transform.position, target, followSpeed * Time.deltaTime);

            yield return null;
            if (boss.IsParried || boss.IsGroggy) yield break;
        }

        // 고정 단계(JumpDown)로
        if (determistic != null)
            yield return boss.StartCoroutine(determistic.Execute(boss));
    }
}

[CreateAssetMenu(menuName = "Spider/JumpDown")]
public class SpiderJumpDown : SpiderCommand
{
    public int landingHitbox = 0;


    public IEnumerator Execute(BossTemplate boss)
    {
        boss.transform.position = boss.PlayerPosition();
        

        //make visible again

        boss.Animator.SetTrigger(trigger);
        boss.HitboxOn(landingHitbox);

        AnimatorClipInfo[] clipInfos = boss.Animator.GetCurrentAnimatorClipInfo(0);
        float waitTime = 1f;
        if (clipInfos.Length > 0)
        {
            waitTime = clipInfos[0].clip.length;
        }

        yield return new WaitForSeconds(waitTime);

        if (boss.IsParried || boss.IsGroggy) yield break;

        if (nextCommands != null && nextCommands.Count > 0)
        {
            int idx = Random.Range(-1, nextCommands.Count);
            if (idx >= 0)
                yield return boss.StartCoroutine(nextCommands[idx].Execute(boss));
        }
    }
}


public class Spier : BossTemplate
{

}
