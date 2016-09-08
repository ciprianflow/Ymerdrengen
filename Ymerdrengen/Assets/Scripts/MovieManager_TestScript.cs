/// <copyright file="MovieManager_TestScript.cs" company="DADIU">
/// Copyright (c) 2016 All Rights Reserved
/// </copyright>
/// <author>Alexander Kirk Jørgensen</author>
/// <date>09/08/2016 11:15 AM</date>
using System.Collections;
using UnityEngine;

/// <summary>
/// Test Script to show that PlayFullScreenMovie works.
/// <para>
/// Supported movie formats:
/// - .mov(Tested)
/// - .mp4(Tested)
/// - .mpv
/// - .3gp
/// </para><para>
/// Using compression standards:
/// - H.264 Baseline Profile Level 3.0 video, up to 640 x 480 at 30 fps.
/// - MPEG-4 Part 2 video(Simple Profile).
/// </para>
/// The script uses hardcoded video files to work, and should be used as inspiration for a generic runtime script.
/// </summary>
public class MovieManager_TestScript : MonoBehaviour
{
    /// <summary>
    /// As soon as the manager starts, it will try and play the video referenced as argument.
    /// </summary>
    public void Start() 
    {
        this.StartCoroutine(this.PlayStreamingVideo("Video__0004_Layer-Comp-5.mp4"));
    }

    /// <summary>
    /// Plays the video described by the <paramref name="url"/> parameter.
    /// </summary>
    /// <param name="url">A path (including file extension) to the location of a video file. The video should be stored within the StreamingAssets folder.</param>
    /// <returns>Nothing. This method is a coroutine.</returns> 
    private IEnumerator PlayStreamingVideo(string url) 
    {
        Debug.Log("Starting Movie");
        Handheld.PlayFullScreenMovie(url, Color.black, FullScreenMovieControlMode.Full, FullScreenMovieScalingMode.AspectFit);
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        Debug.Log("Movie stopped");
    }
}
