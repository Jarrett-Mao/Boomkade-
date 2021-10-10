using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidController : MonoBehaviour
{
    public Sprite[] sprites;

    public float speed;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    
    private void Awake(){
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // Vector3 randomDirection = new Vector3(Random.value, Random.value);
       
        rb.velocity = transform.up * speed; 
        sr = sprites[Random.Range(0, sprites.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
