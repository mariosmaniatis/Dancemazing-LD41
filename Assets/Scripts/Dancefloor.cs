using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Dancefloor : MonoBehaviour {
    public bool is_dancing = false;
    private string key_to_press = "";
    private string[] keys = new string[]
    {
        "down",
        "left",
        "right",
        "top",
    };
    public Sprite[] keys_sprites;
    public Sprite[] seconds_sprites;
    public Image ui_image;
    public int failed_tries = 0;
    public int success = 0;
    private Animator animator;
    private bool key_pressed = true;
    private AudioSource aud;
    public GameObject handler;
    private int count;
    public GameObject crow_model;
    public GameObject monster;
    public GameObject success_obj;
    public GameObject failed_obj;
    private AudioSource success_source;
    private AudioSource failed_source;

    private void Start()
    {
        animator = ui_image.gameObject.GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
        success_source = success_obj.GetComponent<AudioSource>();
        failed_source = failed_obj.GetComponent<AudioSource>();
        //ui_image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update () {
		if (is_dancing == true && count == 0)
        {
            if (key_to_press == "")
            {
                int key = randomKey();
                key_to_press = keys[key];
                keyHandler(key);             
            } else
            {
                if (!key_pressed)
                {
                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        key_pressed = true;
                        if (key_to_press != "down")
                        {
                            failed_tries++;
                            failed_source.Play();
                        }
                        else
                        {
                            success++;
                            success_source.Play();
                        }
                    }

                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        key_pressed = true;
                        if (key_to_press != "top")
                        {
                            failed_tries++;
                            failed_source.Play();
                        }
                        else
                        {
                            success++;
                            success_source.Play();
                        }
                    }

                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        key_pressed = true;
                        if (key_to_press != "left")
                        {
                            failed_tries++;
                            failed_source.Play();
                        }
                        else
                        {
                            success++;
                            success_source.Play();
                        }
                    }

                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        key_pressed = true;
                        if (key_to_press != "right")
                        {
                            failed_tries++;
                            failed_source.Play();
                        }
                        else
                        {
                            success++;
                            success_source.Play();
                        }
                    }
                }
            }
        }

        if (failed_tries >= 3)
        {
            Lost();
        }
        if (success >= 8)
        {
            Success();
        }

	}

    void Lost()
    {
        //fader.GetComponent<Animator>().SetBool("Fade", true);
        StartCoroutine(endGame());
    }

    private IEnumerator endGame()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("gameplay", LoadSceneMode.Single);
    }

    void Success()
    {
        animator.SetBool("Play", false);
        success = 0;
        failed_tries = 0;
        //monster.transform.position = new Vector3(15f, 2.19f, 2f);
        monster.GetComponent<NavMeshAgent>().Warp(new Vector3(15f, 2.19f, 2f));
        crow_model.GetComponent<Animator>().SetBool("dancing", false);
        aud.Stop();
        handler.GetComponent<AudioSource>().Stop();
        handler.GetComponent<AudioSource>().Play();
        
        is_dancing = false;
        monster.GetComponent<PatrolCore>().relocate();
    }

    void keyHandler(int key)
    {
        Image image = ui_image.gameObject.GetComponent<Image>();
        image.sprite = keys_sprites[key];
        animator.SetBool("Play", true);
    }

    public void Event_CastAgain()
    {
        if (key_pressed == false)
        {
            failed_tries++;
            failed_source.Play();
        }
        key_pressed = false;
        key_to_press = "";
    }

    private int randomKey()
    {
        int key = Random.Range(0, keys.Length);
        return key;
    }

    public void startDancing()
    {
        handler.GetComponent<AudioSource>().Stop();
        aud.Play();
        aud.Play(44100);
        StartCoroutine(Countdown(5));
    }

    void startKeyPressing()
    {
        crow_model.GetComponent<Animator>().SetBool("dancing", true);
        success = 0;
        failed_tries = 0;
        key_to_press = "";
        is_dancing = true;
    }

    IEnumerator Countdown(int seconds)
    {
        count = seconds;

        while (count > 0)
        {
            Debug.Log(count);
            if (count < 4)
            {
                Image image = ui_image.gameObject.GetComponent<Image>();
                image.sprite = seconds_sprites[count - 1];
                animator.SetBool("Play", true);
            }
            // display something...
            yield return new WaitForSeconds(1);
            count--;
        }

        // count down is finished...
        startKeyPressing();
    }
}
