using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wizard/MultiGridAttack")]
public class MultiGridAttack : ScriptableObject, IBossCommand
{
    public int safeCells = 2;
    public string Trigger;
    public GameObject hitboxPrefab;

    public IEnumerator Execute(BossTemplate boss)
    {
        boss.Animator.SetTrigger(Trigger);

        yield return new WaitForSeconds(2f);

        boss.Animator.ResetTrigger(Trigger);

        List<int> pool = new() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
        List<int> safe = new();
        for (int i = 0; i < safeCells; i++)
        {
            int pick = Random.Range(0, pool.Count);
            safe.Add(pool[pick]);
            pool.RemoveAt(pick);
        }
        List<int> attack = pool; 

        foreach (int idx in attack)
        {
            GameObject hitb = Object.Instantiate( hitboxPrefab, GridManager.Instance.GetCellPos(idx), Quaternion.identity);
            hitb.SetActive(true);
        }

        AnimatorClipInfo[] clipInfos = boss.Animator.GetCurrentAnimatorClipInfo(0);

        float waitTime = 1f;
        if (clipInfos.Length > 0)
        {
            waitTime = clipInfos[0].clip.length;
        }

        yield return new WaitForSeconds(waitTime);

        if (boss.IsParried || boss.IsGroggy)
            yield break;
        //boss.SetIdle();


    }
}
