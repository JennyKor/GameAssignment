using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioSource Audio;
    public AudioClip clink;
    public AudioClip fall;
    public AudioClip lvl_complete;
    public AudioSource bgmusic;

   // bool isMusicPlaying = false;

    public static SoundManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != null)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void playSoundEffect(AudioClip clip)
    {
        Audio.clip = clip;
        Audio.Play();
    }

    public void StopBgMusic()
    {
        bgmusic.Stop();    
    }

    public void StartBgMusic()
    {
        bgmusic.Play();
       // isMusicPlaying = true;
    }

    public void PauseBgMusic()
    {

        bgmusic.Pause();
        //if (isMusicPlaying)
        //{
        //    bgmusic.Pause();
        //    isMusicPlaying = false;
        //}
        //if (!isMusicPlaying)
        //{ 
        //    bgmusic.UnPause();
        //    isMusicPlaying = true;
        //}
    }

    public void UnpauseBgMusic()
    {
        bgmusic.UnPause();
    }

}
