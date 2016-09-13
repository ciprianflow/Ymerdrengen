using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

        StartCoroutine(OpenMainMenu());
    }

    /// <summary>
    /// Open main menu after delay
    /// </summary>
    /// <returns></returns>
    private IEnumerator OpenMainMenu()
    {
        yield return new WaitForSeconds(5);

        SceneManager.LoadScene(0);
    }

    
}
