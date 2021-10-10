using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rotSpeed;
    public float moveSpeed;
    public GameObject laser;
    public float cooldown = 1f;
    private float time = 0f;
    private Rigidbody2D rb;

    public float laserSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0f){
            time -= Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.Space)){
            GameObject laserObj = Instantiate(laser, transform.TransformPoint(Vector3.forward * 2), transform.rotation);
            laserObj.GetComponent<Rigidbody2D>().velocity = transform.up * laserSpeed * Time.deltaTime;
            time = cooldown;
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
            transform.Rotate(-transform.forward * rotSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.D))
            transform.Rotate(transform.forward * rotSpeed * Time.deltaTime);
    }
}
