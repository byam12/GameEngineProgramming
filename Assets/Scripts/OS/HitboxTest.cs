using System.Collections;
using UnityEngine;

public class HitboxTest : MonoBehaviour
{
    [Header("영향 시간")]
    public float fadeDuration = 0.5f;
    public float checkDuration = 0.3f; 

    [Header("데미지")]
    public float damage = 10f;

    Renderer rend;
    Collider2D col;

    bool hasCollided;

    void OnEnable()
    {
        rend = GetComponent<Renderer>();
        col = GetComponent<Collider2D>();

        hasCollided = false;
        col.enabled = false; 
        StartCoroutine(HitboxSequence());
    }

    IEnumerator HitboxSequence()
    {
        Color start = rend ? rend.material.color : Color.white;
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            if (rend) rend.material.color = Color.Lerp(start, Color.red, t / fadeDuration);
            yield return null;
        }
        if (rend) rend.material.color = Color.red;

        col.enabled = true;
        float elapsed = 0f;
        while (elapsed < checkDuration)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        col.enabled = false;
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) 
            return;

        Player pl = other.GetComponent<Player>();
        if (pl == null) 
            return;

        switch (pl.getStatus())
        {
            case 0:
                pl.hurt(damage);
                Debug.Log("Damage " + damage);
                break;

            case 2:
                Debug.Log("block");
                break;

            case 3:
                Debug.Log("parry");
                GetComponentInParent<BossTemplate>().EnterParried();
                break;
        }

        hasCollided = true;
    }
}
