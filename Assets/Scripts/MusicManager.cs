using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public List<AudioClip> tunes;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
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
