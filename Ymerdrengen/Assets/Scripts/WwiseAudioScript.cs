using UnityEngine;

public class WwiseAudioScript : MonoBehaviour
{
    /// <summary>
    /// Minimum music clip length in seconds
    /// </summary>
    public float MinMusicTime = 5f;

    /// <summary>
    /// Maximum music clip length in seconds
    /// </summary>
    public float MaxMusicTime = 10f;

    /// <summary>
    /// Minimum pause time in seconds
    /// </summary>
    public float MinPauseTime = 3f;

    /// <summary>
    /// Maximum pause time in seconds
    /// </summary>
    public float MaxPauseTime = 6f;

    public void Start()
    {
        AkSoundEngine.PostEvent("start", gameObject);
    }

    void Update() 
    {
    }
}
