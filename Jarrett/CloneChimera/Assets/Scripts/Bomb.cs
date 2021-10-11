using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explodepf;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(explode());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator explode(){
        yield return new WaitForSeconds(3.0f);
        Instantiate(explodepf, transform.position, explodepf.transform.rotation);
        Destroy(this.gameObject);
    }
}
