// <copyright file="ParticleGenerator.cs" company="Team4">
// Company copyright tag.
// </copyright>
using System.Collections;
using UnityEngine;

/// <summary>
/// Class that serves as the base for the particle spawner object
/// </summary>
public class ParticleGenerator : MonoBehaviour
{
    /// <summary>
    /// Amount of particles to be spawned.
    /// </summary>
    [SerializeField]
    public int MaxParticles = 250;


    /// <summary>
    /// Minimal mass variable
    /// </summary>
    [Range(1f, 50f)]
    public float MinMass = 1f;

    /// <summary>
    /// Max mass variable
    /// </summary>
    [Range(1f, 50f)] public float MaxMass = 50f;

    /// <summary>
    /// Drag multiplier for particles
    /// </summary>
    [Range(1f, 10f)] public float DragMultiplier = 3f;

    /// <summary>
    /// Minimum scale multiplier.
    /// </summary>
    [Range(0.1f, 3f)]
    public float MinScaleMultiplier = 0.5f;

    /// <summary>
    /// Maximal scale multiplier.
    /// </summary>
    [Range(0.1f, 3f)]
    public float MaxScaleMultiplier = 1.3f;

    /// <summary>
    /// Prefab for the particle object.
    /// </summary>
    public Transform Particle;

    /// <summary>
    /// Coroutine iterator.
    /// </summary>
    private WaitForEndOfFrame waitForEndOfFrame;

    /// <summary>
    /// Transform of the current particle.
    /// </summary>
    private Transform currentParticle;

    /// <summary>
    /// Rigid body of the current particle.
    /// </summary>
    private Rigidbody currentRB;

    /// <summary>
    /// Grab references and allocate variables
    /// </summary>
    public void Start()
    {
        waitForEndOfFrame = new WaitForEndOfFrame();
        StartCoroutine(GenerateParticles(MaxParticles));
    }

    /// <summary>
    /// Coroutine for generating particles.
    /// </summary>
    /// <param name="ammount">The amount of particles to generate</param>
    /// <returns>Waits for the end of frame in order to go to the next iteration of the spawning loop</returns>
    public IEnumerator GenerateParticles(int ammount)
    {
        for (int i = 0; i < ammount; i++)
        {
            currentParticle = Instantiate(Particle, this.transform.position, Quaternion.Euler(90, 0, 0)) as Transform;
            currentRB = currentParticle.GetComponent<Rigidbody>();
            currentParticle.transform.localScale *= Random.Range(MinScaleMultiplier, MaxScaleMultiplier);
            currentRB.mass = Random.Range(MinMass, MaxMass);
            currentRB.drag = currentParticle.transform.localScale.sqrMagnitude * DragMultiplier;
            currentParticle.parent = this.transform;
            yield return waitForEndOfFrame;
        }
    }
}
