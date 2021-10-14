using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : Cargo
{
    public Player player;
    public Vector2 direct;
    public GameObject bullet;

    // Start is called before the first frame update
    void Awake()
    {
        //Debug.Log(player1.cargoes.Count);
        //Debug.Log(player2.cargoes.Count);
    }
    

    //new Vector2(-1, 0); // up
            
    //new Vector2(0, 1); // right
            
    //new Vector2(1, 0); // down
            
    //new Vector2(0, -1); // left

    void Start()
    {
        direct = player.GetDirection();
        Debug.Log(direct);
        //bullet.GetComponent<ConstantForce2D>().force = new Vector2(0f, 0f);

        // get cargoList from player
        //List<Cargo> cargoLi = player.cargoes;
    }
    // get direction of player
    // fire projectile, remove cargo from list.
    // 
    void Fire(){
        
        direct = player.GetDirection();
        //Instantiate(bullet, player.transform.position, Quaternion.identity);
        //bullet.GetComponent<ConstantForce2D>().force = new Vector2(0f, 0f);
        if (player.GetDirection().x == 0.0 && player.GetDirection().y == 1.0){ // shoot right
            Instantiate(bullet, player.transform.position, Quaternion.identity).GetComponent<ConstantForce2D>().force = new Vector2(50f, 0f);
            //bullet.GetComponent<ConstantForce2D>().force = new Vector2(20f, 0f);
        } else if (player.GetDirection().x == 0.0 && player.GetDirection().y == -1.0) { // shoot left
            Instantiate(bullet, player.transform.position, Quaternion.identity).GetComponent<ConstantForce2D>().force = new Vector2(-50f, 0f);
            //bullet.GetComponent<ConstantForce2D>().force = new Vector2(-20f, 0f);
        } else if (player.GetDirection().y == 0.0 && player.GetDirection().x == -1.0){ // shoot up
            Instantiate(bullet, player.transform.position, Quaternion.identity).GetComponent<ConstantForce2D>().force = new Vector2(0f, 50f);
            //bullet.GetComponent<ConstantForce2D>().force = new Vector2(0f, 20f);
        } else if (player.GetDirection().y == 0.0 && player.GetDirection().x == 1.0) { // shoot down
            Instantiate(bullet, player.transform.position, Quaternion.identity).GetComponent<ConstantForce2D>().force = new Vector2(0f, -50f);
            //bullet.GetComponent<ConstantForce2D>().force = new Vector2(0f, -20f);
        }
    }
    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyUp(KeyCode.F)){
            
            //bullet.GetComponent<ConstantForce2D>().force = new Vector2(0f, 0f);
            // check if cargo available to shoot
            if (player.cargoes.Count > 0){
                Fire();
                Destroy(player.cargoes[player.cargoes.Count - 1].gameObject);
                player.cargoes.RemoveAt(player.cargoes.Count - 1);
                
            }
            
            
        }
        
    }
    
    
}
