// <copyright file="WwiseAudioScript.cs" company="Team4">
// Company copyright tag.
// </copyright>

using System.Collections;
using UnityEngine;

/// <summary>
/// Controls the music playback.
/// </summary>
public class WwiseAudioScript : MonoBehaviour
{
    /// <summary>
    /// Minimum music clip length in seconds
    /// </summary>
    public float MinimumMusicTime = 10f;

    /// <summary>
    /// Maximum music clip length in seconds
    /// </summary>
    public float MaximumMusicTime = 15f;

    /// <summary>
    /// Minimum pause time in seconds
    /// </summary>
    public float MinimumPauseTime = 3f;

    /// <summary>
    /// Maximum pause time in seconds
    /// </summary>
    public float MaximumPauseTime = 6f;

    /// <summary>
    /// Remaining time before music should pause playing.
    /// </summary>
    private float musicTime = 0f;

    /// <summary>
    /// Remaining time before music should start playing.
    /// </summary>
    private float pauseTime = 0f;

    /// <summary>
    /// Describes whether music is playing or not.
    /// </summary>
    public bool isPlaying = false;

    /// <summary>
    /// Starts the background sounds and plays the boy and girl cue noises.
    /// </summary>
    public void Start()
    {
        AkSoundEngine.PostEvent("start", this.gameObject);
    }

    /// <summary>
    /// Changes whether the girl is singing or not.
    /// </summary>
    private void Update() 
    {
        if (this.isPlaying)
        {
            this.musicTime -= Time.deltaTime;
            if (this.musicTime <= 0) 
            {
                this.isPlaying = false;
                this.pauseTime = Random.Range(this.MinimumPauseTime, this.MaximumPauseTime);
                Debug.Log(string.Format("Pausing girl's song (IG Pausetime: {0:F2} seconds).", this.pauseTime));
                AkSoundEngine.PostEvent("pigeSangStop", this.gameObject);
            }
        }
        else 
        {
            this.pauseTime -= Time.deltaTime;
            if (this.pauseTime <= 0)
            {
                this.isPlaying = true;
                this.musicTime = Random.Range(this.MinimumMusicTime, this.MaximumMusicTime);
                Debug.Log(string.Format("Playing girl's song (IG Playtime: {0:F2} seconds).", this.musicTime));
                AkSoundEngine.PostEvent("pigeSangStart", this.gameObject);
            }
        }
    }
}
