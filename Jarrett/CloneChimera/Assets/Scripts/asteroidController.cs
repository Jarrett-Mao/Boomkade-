using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        // Vector3 randomDirection = new Vector3(Random.value, Random.value);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
