using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChimeraExplosion : MonoBehaviour
{
    public Player player;
    public Cargo cargo;
    static float SCALE_MULTIPLIER = 2f;

    void Awake()
    {
        DestroyColliders();
        Destroy(gameObject, 0.2f);
    }

    void DestroyColliders()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, transform.localScale.x * SCALE_MULTIPLIER);
        foreach (var col in colliders)
        {
            if (col.tag == "PlayerShip")
            {
                GameManager.instance.HandleDestroyPlayer(col.GetComponent<Player>());
            }
            else if (col.tag == "Wall")
            {
                Destroy(col.gameObject);
            }
        }
    }
}
