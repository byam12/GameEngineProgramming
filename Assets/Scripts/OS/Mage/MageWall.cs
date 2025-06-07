using UnityEngine;

public class MageWall : MonoBehaviour
{
    public enum Side { Left, Right, Top, Bottom }
    public Side side;
    public float offset = 0.5f;
    public float stageHalfWidth = 10f;
    public float stageHalfHeight = 6f;

    void Start()
    {

}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        Vector3 pos = col.transform.position;

        switch (side)
        {
            case Side.Left: pos.x = stageHalfWidth - offset; break;
            case Side.Right: pos.x = -stageHalfWidth + offset; break;
            case Side.Top: pos.y = -stageHalfHeight + offset; break;
            case Side.Bottom: pos.y = stageHalfHeight - offset; break;
        }
        col.transform.position = pos;
    }
}
