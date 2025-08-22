using UnityEngine;

public class WeaponSysrem : MonoBehaviour
{

    public Transform[] Spawnpoints;
    public Bullet bulletPrefab;
    public float fireRate = 1f;
    private float fireCounter;
    public AudioSource soundEffect;

    void Update()
    {
        fireCounter += Time.deltaTime;

    }
    
    public void Fire()
    {
        if (fireCounter >= fireRate)
        {
            fireCounter = 0;

            soundEffect.Play();
            foreach (var spawnpoint in Spawnpoints)
            {
                Instantiate(bulletPrefab, spawnpoint);
                //Debug.Log(spawnpoint.position);
            }
           
       }
    }
}
