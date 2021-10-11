using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rotSpeed;
    public float moveSpeed;
    public GameObject laser;
    public float laserSpeed;
    public float cooldown = 1f;
    private float time = 0f;
    private Rigidbody2D rb;

    public bool bomber = false;
    public GameObject bombpf;
    public float bombcd;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "asteroid"){
            // if ((this.size / 2) >= this.minSize){
            //     makeSplit();
            //     makeSplit();
            // }

            // Destroy(this.gameObject);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0.0f;

            this.gameObject.SetActive(false);

            FindObjectOfType<GameManager>().playerDied();
            Camera.main.transform.position = new Vector3(0, 0, -9.917426f);
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        if (time > 0f){
            time -= Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.Space)){

            if (bomber != false){
                Instantiate(bombpf, transform.position, bombpf.transform.rotation);
                time = bombcd;
            }
            else {
                GameObject laserObj = Instantiate(laser, transform.TransformPoint(Vector3.forward * 2), transform.rotation);
                laserObj.GetComponent<Rigidbody2D>().velocity = transform.up * laserSpeed * Time.deltaTime;
                time = cooldown;
            }
            
        }

        if (Input.GetKey(KeyCode.W)){
            // GetComponent<Rigidbody>().AddForce(transform.forward * moveSpeed * Time.deltaTime);
            // Debug.Log("wokring");
            rb.velocity = transform.up * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
            // GetComponent<Rigidbody>().AddForce(transform.forward * -moveSpeed * Time.deltaTime);
            rb.velocity = transform.up * -moveSpeed * Time.deltaTime;


        if (Input.GetKey(KeyCode.A))
            transform.Rotate(transform.forward * rotSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.D))
            transform.Rotate(-transform.forward * rotSpeed * Time.deltaTime);
    }
}
