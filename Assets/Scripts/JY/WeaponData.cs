using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/WeaponData")]
public class WeaponData : ScriptableObject
{
    public enum WeaponType { Melee, Range}
    public enum Type { Fire, Water, Earth, Wind, Dark, Light }

    [Header("# Main Info")]
    public WeaponType weaponType;
    public Type type;
    public string WeaponName;
    [TextArea]
    public string desc;
    public Sprite sprite;
    public float[] Damages;
    public GameObject effect;
}
