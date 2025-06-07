using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Wizard/CameraZoom")]
public class ZoomPattern : ScriptableObject, IBossCommand
{
    public string trigger;
    public UnityEvent onCastEvent;

 
    public float zoomStep = 1.5f;
    public float zoomTime = 0.35f;
    public float zoomMin = 2.5f;

    public List<IBossCommand> nextCommands;

    public IEnumerator Execute(BossTemplate boss)
    {
        if (!string.IsNullOrEmpty(trigger))
            boss.Animator.SetTrigger(trigger);
        onCastEvent?.Invoke();

        yield return null;

        var state = boss.Animator.GetCurrentAnimatorStateInfo(0);
        float wait = state.length / Mathf.Max(state.speed, 0.01f);
        yield return new WaitForSeconds(wait);

        Camera cam = Camera.main;
        float start = cam.orthographicSize;
        float target = Mathf.Max(start - zoomStep, zoomMin);

        for (float t = 0; t < zoomTime; t += Time.deltaTime)
        {
            cam.orthographicSize = Mathf.Lerp(start, target, t / zoomTime);
            yield return null;
        }
        cam.orthographicSize = target;

        if (nextCommands != null && nextCommands.Count > 0)
        {
            int pick = Random.Range(-1, nextCommands.Count);
            if (pick >= 0)
                yield return boss.StartCoroutine(nextCommands[pick].Execute(boss));
        }
    }

}
