using UnityEngine;
using System.Collections;

public class NoteSpawner : MonoBehaviour {

    Transform noteList;
    private GameObject[] notePrefabs;

    private float t = 0f;
    private float internalSpawnCount = 0;

    [Range(0.0f, 200f)]
    public float spawnCount = 3f;
    [Range(0.1f, 10f)]
    public float spawnRate = 2f;
    [Range(0.01f, 2f)]
    public float speed = 3f;
    [Range(0.01f, 6f)]
    public float hoverHeight = 0.5f;
    [Range(0.01f, 6f)]
    public float hoverVariance = 0.5f;
    [Range(0.01f, 20f)]
    public float hoverSpeed = 3;
    [Range(0.01f, 6f)]
    public float swingVariance = 0.5f;
    [Range(0.01f, 20f)]
    public float swingSpeed = 3;
    [Range(0f, 200f)]
    public float rotationSpeed = 2f;
    [Range(0.1f, 50f)]
    public float startDistance = 3f;
    [Range(0.01f, 20f)]
    public float endDistance = 0.5f;

    MoveScript boyMoveScript;
    bool isSpawning = false;

    // Use this for initialization
    public void Init () {

        noteList = new GameObject().transform;
        noteList.name = "Note List";
        noteList.SetParent(this.transform);

        notePrefabs = new GameObject[3];
        notePrefabs[0] = Resources.Load("NotePrefabs/Note_a") as GameObject;
        notePrefabs[1] = Resources.Load("NotePrefabs/Note_b") as GameObject;
        notePrefabs[2] = Resources.Load("NotePrefabs/Note_c") as GameObject;

        boyMoveScript = transform.GetComponent<MoveScript>();

    }
	
	// Update is called once per frame
	void Update ()
    {

        if( isSpawning)
        {
            t += Time.deltaTime;
            if(t >= spawnRate)
            {
                spawnNote();
                t = 0;
                internalSpawnCount++;
                if(internalSpawnCount >= spawnCount)
                {
                    isSpawning = false;
                    internalSpawnCount = 0;
                    return;
                }
            }
        }
      
	}

    void spawnNote()
    {

        int index = Random.Range(0, notePrefabs.Length);
        GameObject temp = Instantiate(notePrefabs[index]);

        NoteBehavior note = temp.GetComponent<NoteBehavior>();
        note.init(speed ,hoverHeight, hoverVariance, hoverSpeed, rotationSpeed, boyMoveScript.getT(), 
            startDistance, endDistance, boyMoveScript.getCurrentBezierSpline(), boyMoveScript.transform);

        note.setSwingSpeed(swingSpeed);
        note.setSwingVariance(swingVariance);

        temp.transform.SetParent(noteList);

    }

    public void beginNoteSpawn()
    {
        Debug.Log("I am now supposed to do a thing");
        isSpawning = true;
    }
}
