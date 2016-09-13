// <copyright file="PlayMusic.cs" company="Team4">
// Company copyright tag.
// </copyright>

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

/// <summary>
/// This script handles the music during the menu and game
/// </summary>
public class PlayMusic : MonoBehaviour
{

    /// <summary>
    /// music for the title (intro)
    /// </summary>
    public AudioClip TitleMusic;

    /// <summary>
    ///  music for main menu
    /// </summary>
    public AudioClip MenuMusic;

    public AudioMixerSnapshot volumeDown;
    public AudioMixerSnapshot volumeUp;

    public Sprite SoundOn;
    public Sprite SoundOff;

    /// <summary>
    /// audio source
    /// </summary>
    private AudioSource audioSource;

    /// <summary>
    /// delay to start playing music
    /// </summary>
    private float resetTime = 1.01f;

    private bool menuSoundValue = true;

    /// <summary>
    /// init audio source
    /// </summary>
    public void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// play title music on start
    /// </summary>
    public void Start()
    {
        
        audioSource.clip = TitleMusic;
        FadeUp(resetTime);
        audioSource.Play();
    }

    /// <summary>
    /// pauses music (note: not the same as ToggleMusic)
    /// </summary>
    public void PauseMusicToggle(bool val)
    {
        if (val)
        {
            FadeDown(resetTime);
            audioSource.Pause();
        } 
        else
        {
            FadeUp(resetTime);
            audioSource.UnPause();
        }
    }

    /// <summary>
    /// play music based on the level, main menu music otherwise
    /// </summary>
    public void PlayMenuMusic()
    {

        audioSource.clip = MenuMusic;
        audioSource.loop = true;

        //fade
        FadeUp(resetTime);
        audioSource.Play();

    }

    public void StopPlayMusic()
    {
        audioSource.Stop();
    }

    /// <summary>
    /// toggle musci button
    /// </summary>
    public void ToggleMusic(bool val)
    {
        if (!val)
        {
            FadeDown(resetTime);
            audioSource.mute = true;
        }
        else
        {
            FadeUp(resetTime);
            audioSource.mute = false;
        } 
    }

    public void MenuToggleSound()
    {
        if(menuSoundValue)
        {

            menuSoundValue = false;
            // mute sound
            ToggleMusic(false);

            //mute sound icon
            GameObject.Find("Sound").GetComponent<SpriteRenderer>().sprite = SoundOff;
        }
        else
        {
            menuSoundValue = true;
            ToggleMusic(true);

            //mute sound icon
            GameObject.Find("Sound").GetComponent<SpriteRenderer>().sprite = SoundOn;

        }
    }

    /// <summary>
    /// fade volumes
    /// </summary>
    /// <param name="resetTime"></param>
    public void FadeUp(float resetTime)
    {
        volumeUp.TransitionTo(resetTime);
    }

    /// <summary>
    /// fade down volume
    /// </summary>
    /// <param name="resetTime"></param>
    public void FadeDown(float resetTime)
    {
        volumeDown.TransitionTo(resetTime);
    }
}
