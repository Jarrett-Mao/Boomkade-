using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(delete());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider){
        if (collider.gameObject.CompareTag("Player")){
            
            collider.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            collider.GetComponent<Rigidbody2D>().angularVelocity = 0.0f;

            collider.gameObject.SetActive(false);

            FindObjectOfType<GameManager>().playerDied();
            Camera.main.transform.position = new Vector3(0, 0, -9.917426f);
        }
        else if (collider.gameObject.CompareTag("asteroid")){
            // if ((collider.GetComponent<Asteroid>().size / 2) >= collider.GetComponent<Asteroid>().minSize){
            //     collider.GetComponent<Asteroid>().makeSplit();
            //     collider.GetComponent<Asteroid>().makeSplit();
            // }

            Destroy(collider.gameObject);
        }
    }

    IEnumerator delete(){
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
