using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FadeAudioSouce
{
    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

    public static IEnumerator SetVolume(AudioSource audioSource, float targetVolume){
        yield return new WaitForSeconds(3.0f);
        audioSource.volume = targetVolume;
    }
}
