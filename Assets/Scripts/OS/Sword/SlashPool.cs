using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class SlashPool : MonoBehaviour
{
    public List<GameObject> slashes = new List<GameObject> ();
    public bool hori = false;
    public bool vert = false;

    public static SlashPool instance = null;

    public void Awake()
    {
        instance = this;
    }


    public void Attack()
    {
        //Vector3 playerLocation = FindAnyObjectByType<CharacterController>().gameObject.transform.position;
        Vector3 playerLocation = Vector3.zero;
        if (playerLocation == null)
        {
            playerLocation = new Vector3(0, 0, 0);
        }
        if(hori) 
        {
            slashes[0].transform.position = playerLocation;
            slashes[0].gameObject.SetActive (true);
            int i = slashes[0].transform.childCount;
            for(int j = 0; j < i ; j++)
            {
                slashes[0].transform.GetChild(j).gameObject.SetActive (true);
            }

        }
        if(vert) 
        {
            slashes[1].transform.position = playerLocation;
            slashes[1].gameObject.SetActive(true);

            int i = slashes[1].transform.childCount;
            for (int j = 0; j < i; j++)
            {
                slashes[1].transform.GetChild(j).gameObject.SetActive(true);
            }
        }

        hori = false;
        vert = false;


    }

}
