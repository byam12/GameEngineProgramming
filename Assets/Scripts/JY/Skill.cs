using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] float speed = 7f;
    float xDir = -1;

    private void Start()
    {
        if (gameObject.transform.rotation.y == 180) xDir *= -1;
        Destroy(this.gameObject, 0.8f);
    }

    private void Update()
    {
        gameObject.transform.Translate(Vector3.right * xDir * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("monster")) //temp condition
        { 
            Destroy(this.gameObject);
        }
    }
}
