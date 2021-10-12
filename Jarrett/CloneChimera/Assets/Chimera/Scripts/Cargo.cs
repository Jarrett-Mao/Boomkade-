using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo : Follower
{
    public Player parentPlayer;
    public Follower target;

    public void InitializeCargo(Player player)
    {
        parentPlayer = player;
        List<Cargo> cargoList = player.cargoes;
        if (cargoList.Count > 0)
        {
            Cargo lastCargo = cargoList[cargoList.Count - 1];
            target = lastCargo;
            transform.position = lastCargo.lastPosition;
            targetPosition = lastCargo.lastPosition;
            lastPosition = transform.position;
            player.cargoes.Add(this);
        }
        else
        {
            target = player;
            transform.position = player.lastPosition;
            targetPosition = player.lastPosition;
            lastPosition = transform.position;
            player.cargoes.Add(this);
        }

        GetComponent<SpriteRenderer>().color = player.GetComponent<SpriteRenderer>().color;

        Invoke("TurnOnCollider", Time.deltaTime * 2);
    }


    public void TurnOnCollider()
    {
        GetComponentInChildren<Collider2D>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (parentPlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, parentPlayer.speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>())
        {
            GameManager.instance.HandleDestroyPlayer(other.GetComponent<Player>());
        }
    }
}
