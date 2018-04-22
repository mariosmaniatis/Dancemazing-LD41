using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour {

    public GameObject controls_show;
    public GameObject fader;
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame()
    {

        fader.GetComponent<Animator>().SetBool("Fade", true);
        StartCoroutine(startGame());
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Controls()
    {
        controls_show.SetActive(true);
    }

    private IEnumerator startGame()
    {
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("gameplay", LoadSceneMode.Single);
    }
}
