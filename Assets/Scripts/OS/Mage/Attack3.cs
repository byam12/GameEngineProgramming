using UnityEngine;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Wizard/Attack3")]
public class Attack3 : ScriptableObject, IBossCommand
{

    public string trigger;
    public UnityEvent onCastEvent;

    [Header("Bullet prefab (Rigidbody2D 필요)")]
    public GameObject bulletPrefab;

    [Header("패턴 파라미터")]
    public int waves = 3;
    public int bulletsPerWave = 24; 
    public float bulletSpeed = 6f;        // 탄 속도
    public float waveInterval = 0.4f;     // 연사 간 간격(초)
    public float spinPerWave = 15f;

    [Header("다음 패턴")]
    public ScriptableObject next;

    public IEnumerator Execute(BossTemplate boss)
    {
        if (bulletPrefab == null)
        {
            Debug.LogError("no bullte");
            yield break;
        }

        if (!string.IsNullOrEmpty(trigger))
            boss.Animator.SetTrigger(trigger);
        AnimatorClipInfo[] clipInfos = boss.Animator.GetCurrentAnimatorClipInfo(0);

        float waitTime = 1f;
        if (clipInfos.Length > 0)
        {
            waitTime = clipInfos[0].clip.length;
        }
        yield return new WaitForSeconds(waitTime);


        // shoot
        for (int w = 0; w < waves; w++)
        {
            float baseAngle = w * spinPerWave;
            for (int i = 0; i < bulletsPerWave; i++)
            {
                
                float angle = baseAngle + (360f / bulletsPerWave) * i;
                float rad = angle * Mathf.Deg2Rad;

                
                GameObject b = Object.Instantiate(bulletPrefab, boss.transform.position, Quaternion.Euler(0, 0, angle));
                
                Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
                if (rb != null)
                    rb.velocity = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * bulletSpeed;
            }

            yield return new WaitForSeconds(waveInterval);

            if (boss.IsParried || boss.IsGroggy) 
                yield break;
        }

        if (next is IBossCommand cmd)
            yield return boss.StartCoroutine(cmd.Execute(boss));
    }
}
