using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChimeraBomb : MonoBehaviour
{
    public ChimeraExplosion chimeraExplosion;
    SpriteRenderer sprite;
    double fuse = 3.0f;
    double blinkTimer = 2.7f;
    Color spriteColor;
    Color red = new Color (1, 0, 0, 1);

    void Start(){
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(explode());
        spriteColor = sprite.color;
    }

    void Update(){
        fuse -= Time.deltaTime;
        if(fuse > 0.05f && fuse < blinkTimer){
            Debug.Log("Bomb is blinking!");
            /* 
            // Code intends to turn the bomb to its negative color. Broken.
            Color.RGBToHSV(sprite.color, out float H, out float S, out float V);
            float negativeHue = (H + 0.5f) % 1f;
            sprite.color = Color.HSVToRGB(negativeHue, S, V); 
            */
            sprite.color = (sprite.color == red) ? spriteColor : red;
            blinkTimer = 0.9 * fuse;
        }
    }

    IEnumerator explode(){
        yield return new WaitUntil(() => (fuse <= 0));
        Instantiate(chimeraExplosion, gameObject.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
