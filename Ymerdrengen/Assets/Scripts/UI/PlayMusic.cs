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

    /// <summary>
    /// scene music array 
    /// </summary>
    public AudioClip[] SceneMusic;

    public AudioMixerSnapshot volumeDown;
    public AudioMixerSnapshot volumeUp;

    /// <summary>
    /// audio source
    /// </summary>
    private AudioSource audioSource;

    /// <summary>
    /// delay to start playing music
    /// </summary>
    private float resetTime = 1.01f;

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
    public void PlaylevelMusic()
    {

        //play music according to the current scene
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            audioSource.clip = MenuMusic;
            audioSource.loop = true;       
        }
        else
        {
            audioSource.clip = SceneMusic[SceneManager.GetActiveScene().buildIndex - 1];
            audioSource.loop = true;
        }

        //fade
        FadeUp(resetTime);
        audioSource.Play();
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
