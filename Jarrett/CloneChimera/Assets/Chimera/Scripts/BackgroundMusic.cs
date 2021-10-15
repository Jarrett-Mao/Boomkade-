using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource levelMusic;
    public AudioSource deathSong;

    public bool bgm = true;
    public bool gom = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playLevelMusic(){
        bgm = true;
        gom = false;
        levelMusic.Play();
    }

    public void playGameOver(){
        if (levelMusic.isPlaying){
            bgm = false;
            levelMusic.Pause();
        }
        if (!deathSong.isPlaying && gom == false){
            deathSong.Play();
            gom = true;
        }
    }
}
