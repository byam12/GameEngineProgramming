using System.Collections.Generic;
using UnityEngine;

public class SwordBoss : BossTemplate
{
    //new public List<GameObject> hitbox = new List<GameObject>();

    public void HitboxOn(List<int> indices)
    {
        foreach (int idx in indices)
        {
            if (idx < 0 || idx >= hitbox.Count)
            {
                Debug.Log("qwrong num");
                continue;
            }
            hitbox[idx].SetActive(true);
        }
    }
}
