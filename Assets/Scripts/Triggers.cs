using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Triggers : MonoBehaviour {

    public GameObject starting_door;
    public GameObject ending_door;
    private bool game_started = false;
    public bool lever_open = false;
    public GameObject lever;
    public GameObject final_monitor;
    public GameObject final_monitor2;
    public Material dot_material;
    public GameObject door_sound;
    private AudioSource door_sound_audio;
    public GameObject fader;
    // Use this for initialization
    void Start () {
        door_sound_audio = door_sound.GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (game_started == false)
            {
                if (gameObject.name == "EnterTrigger")
                {
                    starting_door.GetComponent<Animator>().SetBool("open", true);
                    door_sound_audio.Play();
                }
                if (gameObject.name == "LeaveTrigger")
                {
                    starting_door.GetComponent<Animator>().SetBool("open", false);
                    game_started = true;
                    door_sound_audio.Play();
                }
            }
            if (gameObject.name == "lever" && lever_open == false)
            {
                lever_open = true;
                gameObject.GetComponent<Animator>().SetBool("open", true);
                final_monitor.GetComponent<Renderer>().material = dot_material;
                final_monitor2.GetComponent<Renderer>().material = dot_material;
                door_sound_audio.Play();
            }

            if (gameObject.name == "EnterEnd")
            {
                if(lever.GetComponent<Triggers>().lever_open == true)
                {
                    ending_door.GetComponent<Animator>().SetBool("open", true);
                    door_sound_audio.Play();
                }
            }

            if(gameObject.name == "MonitorEnd")
            {
                fader.GetComponent<Animator>().SetBool("Fade", true);
                StartCoroutine(endGame());
            }
        }
    }

    private IEnumerator endGame()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("credits", LoadSceneMode.Single);
    }
}
