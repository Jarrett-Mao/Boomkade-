using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpZone : MonoBehaviour
{
    private Vector3 startPos;
    public Vector3 spawnPos;
    public Vector3 warpPos;
    public GameObject spawner;
    public GameObject leftSpawner;
    // public bool enterWarpZone;
    public bool leftWarpZone;

    // Start is called before the first frame update
    void Start()
    {
        startPos = this.transform.position;
        StartCoroutine(DespawnWarp());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.CompareTag("Player")){
            collider.gameObject.transform.position = warpPos;
            Camera.main.transform.position = warpPos + new Vector3(0, 5, -9.917426f);
            collider.gameObject.GetComponent<PlayerController>().bomber = !collider.gameObject.GetComponent<PlayerController>().bomber;
            if (leftWarpZone){
                // if (!enterWarpZone){
                    // enterWarpZone = true;
                    spawner.GetComponent<AsteroidSpawner>().spawn = true;
                    leftSpawner.GetComponent<AsteroidSpawner>().spawn = false;
            // }
                // else{
                //     // enterWarpZone = false;
                //     spawner.SetActive(false);
                // }
            }
            else {
                spawner.GetComponent<AsteroidSpawner>().spawn = false;
                leftSpawner.GetComponent<AsteroidSpawner>().spawn = true;
            }
            
        }
    }

    IEnumerator SpawnWarp(){
        this.transform.position = spawnPos;
        yield return new WaitForSeconds(5.0f);
        StartCoroutine(DespawnWarp()); 
    }

    IEnumerator DespawnWarp(){
        this.transform.position = startPos;
        yield return new WaitForSeconds(20.0f);
        StartCoroutine(SpawnWarp());
    }
}
