using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    // public GameObject asteroid;
    public Asteroid asteroidpf;

    public float trajectoryVar = 15.0f;

    public float spawnRate;

    public float spawnDistance = 13.0f;

    public int spawnAmount = 1;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
    }

    // Update is called once per frame
    void Update()
    {   

        
    }

    private void Spawn(){
        for (int i = 0; i < this.spawnAmount; i++){
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
            Vector3 spawnPoint = this.transform.position + spawnDirection;

            float variance = Random.Range(-this.trajectoryVar, this.trajectoryVar);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            // GameObject rock = Instantiate(asteroid, spawnPoint, rotation);
            Asteroid asteroid = Instantiate(this.asteroidpf, spawnPoint, rotation);
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);
            asteroid.SetTrajectory(rotation * -spawnDirection);
        }
    }
}
