// <copyright file="WwiseAudioEditor.cs" company="Team4">
// Copyright(c) 2016 All Rights Reserved
// </copyright>
// <author>Alexander Kirk Jørgensen</author>
// <date>12-09-2016</date>
// <summary>Editor extension for WwiseAudioScript objects.</summary>
using UnityEditor;
using UnityEngine;

/// <summary>
/// Custom editor for WwiseAudioScript game objects.
/// </summary>
[CustomEditor(typeof(WwiseAudioScript))]
[CanEditMultipleObjects]
public class WwiseAudioEditor : Editor
{
    /// <summary>
    /// Minimum amount of time the music should be playing.
    /// Should not exceed 7 seconds.
    /// </summary>
    private SerializedProperty minimumMusicTime;

    /// <summary>
    /// Maximum amount of time the music should be playing.
    /// Should not exceed 7 seconds.
    /// </summary>
    private SerializedProperty maximumMusicTime;

    /// <summary>
    /// Minimum amount of time the music should be paused.
    /// </summary>
    private SerializedProperty minimumPauseTime;

    /// <summary>
    /// Maximum amount of time the music should be paused.
    /// </summary>
    private SerializedProperty maximumPauseTime;

    /// <summary>
    /// Creates an editor that warns the user if MinimumMusicTime or MaximumMusicTime is more than 7 seconds.
    /// This is due to one of the soundbytes having a duration of ~7 seconds.
    /// Should the user choose to use times larger than 7, a soundbyte might stop while the character can continue walking.
    /// </summary>
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(this.minimumMusicTime);
        if (this.minimumMusicTime.floatValue > 7f)
        {
            EditorGUILayout.HelpBox("WARNING: Duration of shortest soundbyte is 7 seconds. Consider lowering the minimum music time.", MessageType.Warning);
        }

        EditorGUILayout.PropertyField(this.maximumMusicTime);
        if (this.maximumMusicTime.floatValue > 7f)
        {
            EditorGUILayout.HelpBox("WARNING: Duration of shortest soundbyte is 7 seconds. Consider lowering the maximum music time.", MessageType.Warning);
        }

        EditorGUILayout.PropertyField(this.minimumPauseTime);
        EditorGUILayout.PropertyField(this.maximumPauseTime);
        serializedObject.ApplyModifiedProperties();
    }

    /// <summary>
    /// Initialization of the custom editor.
    /// </summary>
    private void OnEnable()
    {
        this.minimumMusicTime = serializedObject.FindProperty("MinimumMusicTime");
        this.maximumMusicTime = serializedObject.FindProperty("MaximumMusicTime");
        this.minimumPauseTime = serializedObject.FindProperty("MinimumPauseTime");
        this.maximumPauseTime = serializedObject.FindProperty("MaximumPauseTime");
    }
}