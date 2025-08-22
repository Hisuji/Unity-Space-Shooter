using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D Rigedbody2D;
    public float Speed;
    public int Damage;

    public GameObject ExplosionPrefab;

    void Start()
    {
        Rigedbody2D.AddRelativeForce(new Vector2(0, Speed), ForceMode2D.Impulse);
        Destroy(gameObject, 5f);
    }

    void OnTriggerEnter2D(Collider2D other) {
    if(other.CompareTag("Enemy"))
    {
        var metroidController = other.GetComponent<MetroidenController>();
    metroidController.TakeDamage(Damage);
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }     
    }
}   




