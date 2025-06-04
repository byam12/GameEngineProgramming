using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Weapon : MonoBehaviour
{

    [SerializeField] WeaponData data;
    [SerializeField] WeaponData.WeaponType weaponType;
    [SerializeField] float damage;
    [SerializeField] WeaponData.Type type;
    [SerializeField] GameObject effect;
    [SerializeField] GameObject attackPoint;
    [SerializeField] GameObject attackArea;
    SpriteRenderer sr;
    [SerializeField]Sprite weapon;
    [SerializeField] int level;
    Player player;
    SpriteRenderer playerSr;
    GameObject[] attacks = new GameObject[3];
    float height;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        weapon = data.sprite;
        //sr.sprite = weapon;
        weaponType = data.weaponType;
        type = data.type;
        //effect = data.effect;
        player = FindAnyObjectByType<Player>();
        level = player.getLevel();
        playerSr = player.gameObject.GetComponent<SpriteRenderer>();

        int idx = data.Damages.Length <= level ? level : data.Damages.Length;
        damage = data.Damages[idx-1];

        height = sr.bounds.size.y;
        attacks[0] = Instantiate(effect);
        attacks[0].SetActive(false);
        attacks[1] = Instantiate(effect);
        attacks[1].SetActive(false);
        attacks[2] = Instantiate(effect);
        attacks[2].SetActive(false);
    }

    private void LateUpdate()
    {
        string name = playerSr.sprite.name;
        int index = int.Parse(name.Substring(11));

        if (spriteTransforms.TryGetValue(name, out var transformData))
        {
            transform.localPosition = new Vector3(transformData.localX * player.isRight(), transformData.localY, 0);
            transform.localEulerAngles = transformData.localRot * player.isRight();
        }
        if (player.isRight() == 1)
        {
            sr.flipX = true;
        } else
        {
            sr.flipX = false;
        }

        
        // X flip conditions
        if (index == 18 || (index >= 30 && index <= 36)) 
        {
            sr.flipX = player.isRight() == 1 ? false : true;
        }
        else sr.flipX = player.isRight() == 1 ? true : false;

        // attacked / rolling motion
        if (index == 45 || index == 48 || (index >= 71 && index <= 78))
        {
            sr.enabled = false;
        }
        else sr.enabled = true;

        // attacking motion 
        if (index == 20)
        {
            Attack(0);
        } else if (index == 25)
        {
            Attack(1);
        } else if (index == 32)
        {
            Attack(2);
        }

    }
    
    // Weapon is changed
    public void Change(WeaponData data)
    {
        weapon = data.sprite;
        sr.sprite = weapon;
        weaponType = data.weaponType;
        type = data.type;
        effect = data.effect;
        level = player.getLevel();

        int idx = data.Damages.Length <= level ? level : data.Damages.Length;
        damage = data.Damages[idx - 1];

        height = sr.bounds.size.y;
        attacks[0] = Instantiate(effect);
        attacks[0].SetActive(false);
        attacks[1] = Instantiate(effect);
        attacks[1].SetActive(false);
        attacks[2] = Instantiate(effect);
        attacks[2].SetActive(false);
    }

    public void Attack(int index)
    {
        if (!attacks[index].activeSelf) StartCoroutine(AttackPhase(index));
    }

    IEnumerator AttackPhase(int index)
    {
        GameObject attackNow = attacks[index];
        attackNow.SetActive(true);
        float scaleWeight = height * 0.3f;
        float dirWeight = player.isRight();

        // Current attack effect
        attackNow.transform.localScale = Vector3.one * scaleWeight;
        attackNow.transform.position = attackPoint.transform.position + new Vector3((0.3f + sr.bounds.size.y-0.5f) * dirWeight, 0, 0);
        attackNow.transform.eulerAngles = new Vector3(dirWeight == -1 ? 180 : 0, 180, 90 * dirWeight);
        attackNow.GetComponent<ParticleSystem>().Play();

        // Current attack area
        GameObject area = Instantiate(attackArea, transform.position + new Vector3((1 + sr.bounds.size.y ) * dirWeight, 0.5f, 0), Quaternion.identity);
        area.transform.localScale = Vector3.one * height * 1.4f;

        yield return new WaitForSeconds(0.3f);

        attackNow.transform.localScale = Vector3.one;
        Destroy(area);
        attackNow.SetActive(false);
    }

    // local position according to player's current sprite
    Dictionary<string, (float localX, float localY, Vector3 localRot)> spriteTransforms = new Dictionary<string, (float, float, Vector3)>()
    {
        { "HeroKnight_0", (-0.385f, 0.57f, new Vector3(0f, 0f, 142f)) },
        { "HeroKnight_2", (-0.452f, 0.521f, new Vector3(0f, 0f, 142f)) },
        { "HeroKnight_3", (-0.452f, 0.521f, new Vector3(0f, 0f, 142f)) },
        { "HeroKnight_4", (-0.452f, 0.555f, new Vector3(0f, 0f, 142f)) },
        { "HeroKnight_5", (-0.412f, 0.568f, new Vector3(0f, 0f, 142f)) },
        { "HeroKnight_6", (-0.375f, 0.541f, new Vector3(0f, 0f, 142f)) },
        { "HeroKnight_7", (-0.377f, 0.541f, new Vector3(0f, 0f, 142f)) },
        { "HeroKnight_8", (-0.375f, 0.569f, new Vector3(0f, 0f, 135f)) },
        { "HeroKnight_9", (-0.411f, 0.602f, new Vector3(0f, 0f, 135f)) },
        { "HeroKnight_10", (-0.411f, 0.602f, new Vector3(0f, 0f, 135f)) },
        { "HeroKnight_11", (-0.444f, 0.602f, new Vector3(0f, 0f, 135f)) },
        { "HeroKnight_12", (-0.444f, 0.602f, new Vector3(0f, 0f, 135f)) },
        { "HeroKnight_13", (-0.429f, 0.602f, new Vector3(0f, 0f, 135f)) },
        { "HeroKnight_14", (-0.429f, 0.602f, new Vector3(0f, 0f, 135f)) },
        { "HeroKnight_15", (-0.429f, 0.602f, new Vector3(0f, 0f, 135f)) },
        { "HeroKnight_16", (-0.429f, 0.602f, new Vector3(0f, 0f, 135f)) },
        { "HeroKnight_17", (-0.414f, 0.567f, new Vector3(0f, 0f, 135f)) },
        { "HeroKnight_18", (-0.328f, 1.144f, new Vector3(0f, 0f, 117f)) },
        { "HeroKnight_19", (-0.319f, 1.373f, new Vector3(0f, 0f, 93f)) },
        { "HeroKnight_20", (-0.244f, 0.643f, new Vector3(0f, 0f, 131f)) },
        { "HeroKnight_21", (-0.262f, 0.661f, new Vector3(0f, 0f, 131f)) },
        { "HeroKnight_22", (-0.182f, 0.661f, new Vector3(0f, 0f, 131f)) },
        { "HeroKnight_23", (0.265f, 0.646f, new Vector3(0f, 0f, 98f)) },
        { "HeroKnight_24", (0.209f, 1.07f, new Vector3(0f, 0f, 105f)) },
        { "HeroKnight_25", (-0.547f, 1.141f, new Vector3(0f, 0f, 64f)) },
        { "HeroKnight_26", (-0.547f, 1.162f, new Vector3(0f, 0f, 63f)) },
        { "HeroKnight_27", (-0.547f, 1.162f, new Vector3(0f, 0f, 63f)) },
        { "HeroKnight_28", (-0.568f, 0.821f, new Vector3(0f, 0f, 92.5f)) },
        { "HeroKnight_29", (-0.551f, 0.701f, new Vector3(0f, 0f, 112f)) },
        { "HeroKnight_30", (-0.397f, 1.073f, new Vector3(0f, 0f, 118f)) },
        { "HeroKnight_31", (0.257f, 1.37f, new Vector3(0f, 0f, 95.5f)) },
        { "HeroKnight_32", (0.647f, 0.392f, new Vector3(0f, 0f, -116f)) },
        { "HeroKnight_33", (0.628f, 0.392f, new Vector3(0f, 0f, -116f)) },
        { "HeroKnight_34", (0.628f, 0.392f, new Vector3(0f, 0f, -116f)) },
        { "HeroKnight_35", (0.628f, 0.392f, new Vector3(0f, 0f, -116f)) },
        { "HeroKnight_36", (0.234f, 0.477f, new Vector3(0f, 0f, -120f)) },
        { "HeroKnight_37", (-0.309f, 0.566f, new Vector3(0f, 0f, -229f)) },
        { "HeroKnight_38", (-0.38f, 0.514f, new Vector3(0f, 0f, -229f)) },
        { "HeroKnight_39", (-0.38f, 0.544f, new Vector3(0f, 0f, -229f)) },
        { "HeroKnight_40", (-0.38f, 0.615f, new Vector3(0f, 0f, -229f)) },
        { "HeroKnight_41", (-0.406f, 0.645f, new Vector3(0f, 0f, -229f)) },
        { "HeroKnight_42", (-0.406f, 0.645f, new Vector3(0f, 0f, -229f)) },
        { "HeroKnight_43", (-0.406f, 0.645f, new Vector3(0f, 0f, -229f)) },
        { "HeroKnight_44", (-0.406f, 0.645f, new Vector3(0f, 0f, -229f)) },
        { "HeroKnight_45", (-0.381f, 0.54f, new Vector3(0f, 0f, -229f)) },
        { "HeroKnight_46", (-0.432f, 0.573f, new Vector3(0f, 0f, -234f)) },
        { "HeroKnight_47", (-0.414f, 0.573f, new Vector3(0f, 0f, -234f)) },
        { "HeroKnight_48", (-0.414f, 0.573f, new Vector3(0f, 0f, -234f)) },
        { "HeroKnight_49", (-0.414f, 0.498f, new Vector3(0f, 0f, -234f)) },
        { "HeroKnight_50", (-0.414f, 0.186f, new Vector3(0f, 0f, -251f)) },
        { "HeroKnight_51", (-0.414f, 0.14f, new Vector3(0f, 0f, -265f)) },
        { "HeroKnight_52", (-0.414f, 0.129f, new Vector3(0f, 0f, -268f)) },
        { "HeroKnight_53", (-0.414f, 0.129f, new Vector3(0f, 0f, 90f)) },
        { "HeroKnight_54", (-0.414f, 0.129f, new Vector3(0f, 0f, 90f)) },
        { "HeroKnight_55", (-0.414f, 0.129f, new Vector3(0f, 0f, 90f)) },
        { "HeroKnight_56", (-0.414f, 0.129f, new Vector3(0f, 0f, 90f)) },
        { "HeroKnight_57", (-0.414f, 0.129f, new Vector3(0f, 0f, 90f)) },
        { "HeroKnight_58", (-0.313f, 0.506f, new Vector3(0f, 0f, 133f)) },
        { "HeroKnight_59", (-0.313f, 0.506f, new Vector3(0f, 0f, 133f)) },
        { "HeroKnight_60", (-0.313f, 0.506f, new Vector3(0f, 0f, 133f)) },
        { "HeroKnight_61", (-0.313f, 0.533f, new Vector3(0f, 0f, 133f)) },
        { "HeroKnight_62", (-0.313f, 0.533f, new Vector3(0f, 0f, 133f)) },
        { "HeroKnight_63", (-0.313f, 0.533f, new Vector3(0f, 0f, 133f)) },
        { "HeroKnight_64", (-0.313f, 0.533f, new Vector3(0f, 0f, 133f)) },
        { "HeroKnight_65", (-0.313f, 0.506f, new Vector3(0f, 0f, 133f)) },
        { "HeroKnight_66", (-0.294f, 0.488f, new Vector3(0f, 0f, 133f)) },
        { "HeroKnight_67", (-0.371f, 0.512f, new Vector3(0f, 0f, 133f)) },
        { "HeroKnight_68", (-0.414f, 0.512f, new Vector3(0f, 0f, 133f)) },
        { "HeroKnight_69", (-0.371f, 0.512f, new Vector3(0f, 0f, 133f)) },
        { "HeroKnight_70", (-0.317f, 0.512f, new Vector3(0f, 0f, 133f)) },
    };
}


