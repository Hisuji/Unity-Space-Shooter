using Mono.Cecil.Cil;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    public Camera Camera;
    public GameObject Player;
    public GameObject[] Metorites;

    public float SpawnRateMinimum = 0.5f;
    public float SpawnRateMaximum = 1.5f;

    public float MetoritesRotationMinimum = 0.5f;
    public float MetoritesRotationMaximum = 1.5f;
    public float MetoritesSpeedMinimum = 1f;
    public float MetoritesSpeedMaximum = 3f;

    private float spawnTime;

    private void DetermineNextSpawnTime()
    {
        spawnTime = Time.time + Random.Range(SpawnRateMinimum, SpawnRateMaximum);
    }

    void Update()
    {
        if (Time.time >= spawnTime)
        {
            SpawnMetorite();
            DetermineNextSpawnTime();
        }
    }

    void Start()
    {
        DetermineNextSpawnTime();
    }

    void SpawnMetorite()
    {
        var prefabIndextoSpaawn = Random.Range(0, Metorites.Length);
        var pfrefabToSpawm = Metorites[prefabIndextoSpaawn];

        var metorit = Instantiate(pfrefabToSpawm, transform);

        var placeVertical = Random.Range(0, 2) == 0;
        float yPosition;
        float xPosition;

        if (placeVertical)
        {
            var halfWidth = Camera.orthographicSize * Camera.aspect;
            xPosition = Random.Range(-halfWidth, halfWidth);

            var sign = Random.Range(0, 2) == 0 ? -1 : 1;
            yPosition = sign * (Camera.orthographicSize + 1);
        }
        else
        {
            var halfWidth = Camera.orthographicSize;
            yPosition = Random.Range(-halfWidth, halfWidth);

            var sign = Random.Range(0, 2) == 0 ? -1 : 1;
            xPosition = sign * (Camera.orthographicSize * Camera.aspect + 1);
        }

        var position = new Vector3(xPosition, yPosition, 1);
        metorit.transform.position = position;

        var direction = position - Player.transform.position;
        var speed = Random.Range(MetoritesSpeedMinimum, MetoritesSpeedMaximum);

        var rigodbody = metorit.GetComponent<Rigidbody2D>();



        rigodbody.AddForce(-direction.normalized * speed , ForceMode2D.Impulse);
    

        var rotaion = Random.Range(MetoritesRotationMinimum, MetoritesRotationMaximum);
        rigodbody.AddTorque(rotaion);



    }



}
