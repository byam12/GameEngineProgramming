using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] private GameObject inventory;
    public void SetInventoryActive(bool show)
    {
        inventory.SetActive(show);
    }
    public bool GetInventoryActive()
    {
        return inventory.activeSelf;
    }

}
