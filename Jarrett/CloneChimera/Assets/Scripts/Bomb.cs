using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explodepf;
    SpriteRenderer sprite;
    double fuse = 3.0f;
    double blinkTimer = 2.7f;
    Color spriteColor;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(explode());
        spriteColor = sprite.color;
    }

    // Update is called once per frame
    void Update()
    {
        fuse -= Time.deltaTime;
        if(fuse > 0.05f && fuse < blinkTimer){
            Debug.Log("Bomb is blinking!");
            Color.RGBToHSV(sprite.color, out float H, out float S, out float V);
            float negativeHue = (H + 0.5f) % 1f;
            sprite.color = Color.HSVToRGB(negativeHue, S, V);
            sprite.color = spriteColor;
            blinkTimer = 0.9 * fuse;
        }
    }

    IEnumerator explode(){
        yield return new WaitUntil(() => (fuse <= 0));
        Instantiate(explodepf, transform.position, explodepf.transform.rotation);
        Destroy(this.gameObject);
    }
}
