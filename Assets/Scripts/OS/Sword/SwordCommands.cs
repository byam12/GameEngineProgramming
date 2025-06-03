using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Sword/Basic")]
public class SwordCommand : ScriptableObject, IBossCommand
{
    public string trigger;
    public List<int> hitboxIndices = new();
    public UnityEvent onAttackEvent;
    public List<SwordCommand> nextCommands = new List<SwordCommand>();

    public bool isSlashVert = false;
    public bool isSlashHori = false;

    public IEnumerator Execute(BossTemplate boss)
    {
        SwordBoss boss1 = (SwordBoss)boss;
        boss1.Animator.SetTrigger(trigger);
        onAttackEvent?.Invoke();
        boss1.HitboxOn(hitboxIndices);
        if(isSlashVert || isSlashHori ) 
        {
            SlashPool.instance.vert = isSlashVert;
            SlashPool.instance.hori = isSlashHori;
            SlashPool.instance.Attack(); 
        }

        AnimatorClipInfo[] clipInfos = boss1.Animator.GetCurrentAnimatorClipInfo(0);

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
                yield return boss1.StartCoroutine(next.Execute(boss1));
            }
            else
            {
                Debug.Log("end pattern");
            }
        }
    }
}

