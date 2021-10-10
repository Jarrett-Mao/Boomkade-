using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pickup : MonoBehaviour
{
    public Action OnPickedUp;
    public GameObject CargoPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>())
        {
            GameObject g = Instantiate(CargoPrefab);
            g.GetComponent<Cargo>().InitializeCargo(other.GetComponent<Player>());
            OnPickedUp?.Invoke();
            Destroy(this.gameObject);
        }
    }
}
