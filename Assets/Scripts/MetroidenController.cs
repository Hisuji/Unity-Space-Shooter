using UnityEngine;

public class MetroidenController : MonoBehaviour
{
    public int damage = 1;
    public int points = 1;
    public int maxHealth = 3;
    public int currentHealth;
    private GameMangager gameMangager;

    public GameObject explisionPrefab;


    void Start()
    {
        gameMangager = FindObjectOfType<GameMangager>();
        currentHealth = maxHealth;

        Destroy(gameObject, 30f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var playerController = other.GetComponent<PlayerController>();
            playerController.TakeDamage(damage);
            Instantiate(explisionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            gameMangager.addPoints(points);
            Destroy(gameObject);
        }
    }

}
