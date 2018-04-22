using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //fader.GetComponent<Animator>().SetBool("Fade", true);
        StartCoroutine(startGame());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator startGame()
    {
        yield return new WaitForSeconds(12f);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
