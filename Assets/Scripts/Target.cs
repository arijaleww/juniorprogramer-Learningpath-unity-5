using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public ParticleSystem explosionParticle;
    private GameManager gameManager;
    public int pointValue;
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -2;
    private Rigidbody targetRb;
    // Start is called before the first frame update
    void Start()
    {

    // Mencari objek GameManager di dalam scene dan mengambil komponen script-nya
    gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    
    // Mengambil komponen Rigidbody dari objek
    targetRb = GetComponent<Rigidbody>();

    // Memberikan gaya dorong ke atas secara acak
    targetRb.AddForce(RandomForce(), ForceMode.Impulse);

    // Memberikan efek putaran (torsi) acak pada sumbu X, Y, dan Z
    targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

    // Menentukan posisi awal objek secara acak di sumbu X dan tetap di sumbu Y (-6)
    transform.position = RandomSpawnPos();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown() 
    { 
        if (gameManager.isGameActive) 
        {
            Destroy(gameObject);
            gameManager.UpdateScore(pointValue);
            // Memunculkan partikel ledakan di posisi dan rotasi objek saat ini
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);

        }

    }
    private void OnTriggerEnter(Collider other) 
    { 
        Destroy(gameObject); 
        if (!gameObject.CompareTag("Bad")) 
        { 
            gameManager.GameOver(); 
        }
    }
    
    Vector3 RandomForce() 
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque() 
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 RandomSpawnPos() 
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
}
