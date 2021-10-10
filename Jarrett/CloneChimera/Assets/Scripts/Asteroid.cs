using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // public Sprite[] sprites;

    public float speed;
    public SpriteRenderer sr;
    private Rigidbody2D rb;

    public float size = 1.0f;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;

    public float lifeTime = 20.0f;

    private void Awake(){
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // sr = GetComponent<SpriteRenderer>();
        // rb = GetComponent<Rigidbody2D>();
        // Vector3 randomDirection = new Vector3(Random.value, Random.value);
       
        // rb.velocity = transform.up * speed; 
        // sr.sprite = sprites[Random.Range(0, sprites.Length)];

        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        this.transform.localScale = Vector3.one * this.size;

        rb.mass = this.size;
    }

    public void SetTrajectory(Vector2 direction){
        rb.AddForce(direction * this.speed);

        Destroy(this.gameObject, this.lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "laser"){
            if ((this.size / 2) >= this.minSize){
                makeSplit();
                makeSplit();
            }

            Destroy(this.gameObject);
        }
    }

    private void makeSplit(){
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;

        Asteroid half = Instantiate(this, position, this.transform.rotation);
        half.size = this.size * 0.5f;
        half.SetTrajectory(Random.insideUnitCircle.normalized * this.speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
