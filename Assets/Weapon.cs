using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] WeaponData data;
    [SerializeField] WeaponData.WeaponType weaponType;
    [SerializeField] float damage;
    [SerializeField] WeaponData.Type type;
    [SerializeField] GameObject effect;
    SpriteRenderer sr;
    [SerializeField]Sprite weapon;
    [SerializeField] int level;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        weapon = data.sprite;
        sr.sprite = weapon;
        weaponType = data.weaponType;
        type = data.type;
        effect = data.effect;
        level = FindAnyObjectByType<Player>().getLevel();

        Debug.Log(level);

        int idx = data.Damages.Length <= level ? level : data.Damages.Length;
        damage = data.Damages[idx-1];
    }

}
