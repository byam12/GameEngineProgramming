using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }

    public Transform[] cells = new Transform[9]; // the areas

    void Awake() => Instance = this;

    public Transform GetCell(int idx) => cells[idx];
    public Vector3 GetCellPos(int idx) => cells[idx].position;
}
