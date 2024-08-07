using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public List<AudioClip> tunes;
    private AudioSource audioSource;

    static MusicManager instance;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        ManageSingleton();
    }

    private void ManageSingleton()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayRandomSong();
    }

    public void PlayRandomSong()
    {
        if (tunes.Count == 0)
        {
            Debug.LogWarning("No tunes available to play.");
            return;
        }

        int randomIndex = Random.Range(0, tunes.Count);
        audioSource.clip = tunes[randomIndex];
        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
}
