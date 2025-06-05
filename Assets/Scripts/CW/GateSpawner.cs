using System.Collections.Generic;
using UnityEngine;

public class GateSpawner : MonoBehaviour
{
    public Transform[] gatePositions;
    public GameObject gatePrefab;

    void Start()
    {
        int numberOfGates = Random.Range(2, 4);

        List<int> indices = new List<int>();
        for (int i = 0; i < gatePositions.Length; i++)
        {
            indices.Add(i);
        }

        Shuffle(indices);

        for (int i = 0; i < numberOfGates; i++)
        {
            int idx = indices[i];
            Instantiate(gatePrefab, gatePositions[idx].position, Quaternion.identity);
        }
    }

    void Shuffle(List<int> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int rand = Random.Range(0, i + 1);
            int temp = list[i];
            list[i] = list[rand];
            list[rand] = temp;
        }
    }
}
