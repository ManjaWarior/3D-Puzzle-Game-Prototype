using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private AudioSource BGM = null; //background music audio source
    [SerializeField] private AudioClip MainTheme = null; //main theme song
    [SerializeField] private AudioClip TimeOutTheme = null; //time out theme song

    private bool mainThemePlaying; //used for seeing if the main theme is playing

    void Start()
    {
        mainThemePlaying = true;//main theme should always play first
    }

    // Update is called once per frame
    void Update()
    {
        if(mainThemePlaying == true)
        {
            if(GameController.TimeRemaining <= 15)//player running out of time
            {
                mainThemePlaying = false;
                BGM.volume = 0.4f;
                StartCoroutine(StartFadeOut(BGM, 2.0f, TimeOutTheme));
            }
        }
        if (mainThemePlaying == false)
        {
            if (GameController.GamePaused == true || GameController.TimeRemaining > 15)//game has ended or player has got a time increase item that put them above 10 seconds
            {
                mainThemePlaying = true;
                BGM.volume = 0.4f;
                StartCoroutine(StartFadeOut(BGM, 2.0f, MainTheme));
            }
        }
    }

    private void changeBGM(AudioClip music)
    {
        BGM.Stop();//stop the current track
        BGM.clip = music;//set the new track
        BGM.Play();//play the new track, added fade in to audio files so music will fade in instead of sudden 
    }

    private IEnumerator StartFadeOut(AudioSource BGM, float timeToFade, AudioClip music)
    {
        float currentTime = 0;
        float start = BGM.volume;
        while (currentTime < timeToFade)
        {
            currentTime += Time.deltaTime;//increments per frame
            BGM.volume = Mathf.Lerp(start, 0.0f, currentTime / timeToFade);//linearly interpolates the volume to gradually lower it by frame to 0
            //some issues with this since it is by frame so at lower FPS the fade is very clear and easy to hear, at higher FPS works fine
            if (currentTime >= timeToFade)//if the fadeout has finished
            {
                changeBGM(music);//change the background music
                BGM.volume = 0.4f;//fade added to audio files so the volume can go straight back to full, will make the music fade in
            }
            yield return null;

        }
        yield break;
    }
}