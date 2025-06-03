using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Sword/Dash")]
public class DashCommand : ScriptableObject, IBossCommand
{
    public string trigger;
    public List<int> hitboxIndices = new();
    public UnityEvent onAttackEvent;
    public List<SwordCommand> nextCommands = new List<SwordCommand>();
    public bool isSlashVert = false;
    public bool isSlashHori = false;

    public float dashDistance = 6f;
    public float dashTime = 0.25f;
    public GameObject slashPrefab;

    public IEnumerator Execute(BossTemplate boss)
    {
        SwordBoss boss1 = (SwordBoss)boss;
        boss1.Animator.SetTrigger(trigger);
        onAttackEvent?.Invoke();
        boss1.HitboxOn(hitboxIndices);

        AnimatorClipInfo[] clipInfos = boss1.Animator.GetCurrentAnimatorClipInfo(0);

        float waitTime = 1f;
        if (clipInfos.Length > 0)
        {
            waitTime = clipInfos[0].clip.length;
        }
        yield return new WaitForSeconds(waitTime);

        Vector3 start = boss.transform.position;
        Vector3 dir = (boss.PlayerPosition() - start).normalized;
        Vector3 target = start + dir * dashDistance;
        Debug.Log(target.ToString());
        boss.transform.position = target;
        Debug.Log("dash did");

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
