using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChimeraBomb : MonoBehaviour
{
    public ChimeraExplosion chimeraExplosion;

    void Awake()
    {
        Destroy(gameObject, 3);
    }

    void OnDestroy()
    {
        Explode();
    }

    void Explode()
    {
        Destroy(gameObject); //calling destroy inside the OnDestroy function seems to work just fine
        Instantiate(chimeraExplosion, gameObject.transform.position, Quaternion.identity);
    }
}
