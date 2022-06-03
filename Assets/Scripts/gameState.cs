using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class gameState : MonoBehaviour
{
    [SerializeField] private GameObject pause;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject levelComplete;
    private bool paused = false;
    private int outofBounds = -9;
    Vector2 playerPos;
    Vector2 startPos;

    PlayerMovement player_m;
    SoundManager soundmanager;

    // Start is called before the first frame update
    void Start()
    {
        
        player_m = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        soundmanager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        startPos = gameObject.transform.position;

        pause.SetActive(false);
        gameOver.SetActive(false);
        levelComplete.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = gameObject.transform.position;
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused) // if we are paused, then the game continues
            { 
                paused = false;
                Time.timeScale = 1;
                pause.SetActive(false);
                soundmanager.UnpauseBgMusic();
            }
            else // is we are not paused, freeze the time and toggle on the pause screen
            {
                paused = true;
                Time.timeScale = 0;
                pause.SetActive(true);
                soundmanager.PauseBgMusic();
            }    
        }

        if (playerPos.y < outofBounds)
        {
            outsideTheMap();
            SoundManager.instance.Audio.PlayOneShot(SoundManager.instance.fall);
            
        }

        //if (playerPos.x > 24f && playerPos.y < -5f)
        //{
        //    float newLimit = -2.3f;
        //    CameraFollow2D cam = new CameraFollow2D();
        //    cam.ResetBottomLimit(newLimit);
        //}
    }



    public void levelCompleted()
    {
        gameObject.SetActive(false);
        Time.timeScale = 0;
        levelComplete.SetActive(true);
        SoundManager.instance.Audio.PlayOneShot(SoundManager.instance.lvl_complete);
    }

    public void ResetLevel()
    {    
        player_m.resetCoins();
        Time.timeScale = 1;
        SceneManager.LoadScene("FirstLevel");
        soundmanager.StartBgMusic();
        
    }

    void outsideTheMap()
    {
        gameObject.SetActive(false);
        Time.timeScale = 0;
        gameOver.SetActive(true);
        soundmanager.StopBgMusic();
    }
}
