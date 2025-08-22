using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private float acceleration; // beschleunigung
    private float steering; // lenkung
    public float accelerationSpeed = 3;
    public float steeringSpeed = 3;
    public Rigidbody2D Rigidbody;
    public GameMangager gameMangager;

    public int maxHealth = 6;
    public int currentHealth;

    public WeaponSysrem [] weaponSystem;
    private int currentWeaponSystemIndex = 0;
    public screenWarping screenWarping;
    public TrailRenderer[] trailRenderer;
    public GameObject flame;

    public SpriteRenderer DamageOverlayRenderer;
    public Sprite[] DamageOverlaySprites;
    private int damageOverlayIndex = -1;

    void Start() // einmalig beim Start
    {
        currentHealth = maxHealth;
        gameMangager.setHealth(currentHealth);
        flame.SetActive(false);
    }

    void Update() // jede fram aufgerufen
    {
        // input = eingabe
        // getAxis = Eingabe von Tastatur oder Controller
        // () je nach richtung
        acceleration = Math.Max(0, Input.GetAxis("Vertical"));
        steering = Input.GetAxis("Horizontal");
        // 0,1,-1
        // 0 nicht gedrückt, 1 gedrückt, -1 andere taste gedrückt bei joysticks gibt es zwischenschenschritte 0.1,0.2....

        flame.SetActive(acceleration > 0); // wenn beschleunigt wird, dann aktiviere die Flamme

        if (Input.GetKey(KeyCode.Space)) // gedrückt hält
        {
            weaponSystem[currentWeaponSystemIndex].Fire();
        }

        if (Input.GetKeyDown(KeyCode.Q)) //kurzes drücken
        {
            currentWeaponSystemIndex--;

            if (currentWeaponSystemIndex <= 0)
            {
                currentWeaponSystemIndex = weaponSystem.Length - 1; // gehe zum letzten System
            }
           
        }
         if (Input.GetKeyDown(KeyCode.E)) //kurzes drücken
            {
                currentWeaponSystemIndex++;

                if (currentWeaponSystemIndex >= weaponSystem.Length)
                {
                    currentWeaponSystemIndex = 0;
                }
                
            }    

    }

    void FixedUpdate() // unabhängig der framerate
    {
        Vector3 velocity = new Vector2(0, acceleration * accelerationSpeed); // vorwärts beschleunigen
        Rigidbody.AddRelativeForce(velocity); 
 
        Rigidbody.AddTorque(-steering * steeringSpeed); // drehen
        
        Vector3 tempPosition = transform.localPosition + velocity * Time.deltaTime;
        if (screenWarping.AmIOutOfBounds(tempPosition))
        {
            
            Vector3 newPosition = screenWarping.CalculateWarpedPosition(tempPosition);
            newPosition.z = transform.position.z;
            transform.position = newPosition;

        }
    }

    public void TakeDamage(int damage) // aufrufbar von anderen klassen
    {
        currentHealth -= damage;
        gameMangager.setHealth(currentHealth);

        damageOverlayIndex++;
        damageOverlayIndex = Mathf.Min(damageOverlayIndex, DamageOverlaySprites.Length - 1);
        DamageOverlayRenderer.sprite = DamageOverlaySprites[damageOverlayIndex];
        
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            gameMangager.GameOver();
        }
    }
        
}
