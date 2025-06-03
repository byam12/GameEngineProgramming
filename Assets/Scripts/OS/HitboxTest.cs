using System.Collections;
using UnityEngine;

public class HitboxTest : MonoBehaviour
{
    public float fadeDuration = 0.5f;

    public float checkDuration = 0.3f;

    public Renderer rend;
    public Collider col;

   
    public bool hasCollided;

    private void OnEnable()
    {
        rend = GetComponent<Renderer>();
        col = GetComponent<Collider>();
        if (GetComponent<Animator>() != null)
            GetComponent<Animator>().SetTrigger("Spell");

        hasCollided = false;
        StartCoroutine(HitboxSequence());
    }

    private IEnumerator HitboxSequence()
    {
        Color startColor = Color.red;
        if (rend != null)
        {  startColor = rend.material.color; }
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            float t = elapsed / fadeDuration;
            if(rend != null )
            rend.material.color = Color.Lerp(startColor, Color.red, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        if (rend != null)
        { rend.material.color = Color.red; }

        float checkElapsed = 0f;
        while (checkElapsed < checkDuration)
        {
            checkElapsed += Time.deltaTime;
            if(hasCollided)
            {
                Debug.Log("damaged");
            }
            yield return null;
        }

        if (!hasCollided)
            Debug.Log("not dasmaed");

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasCollided = true;
            Debug.Log("col");
        }
    }
}
