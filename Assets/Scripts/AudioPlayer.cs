using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField]private float enemyDistanceVolumeDivider = 2f;
    [SerializeField] AudioClip[] shootingClips;
    [SerializeField][Range(0.0f, 1.0f)] float shootingVolume = 1.0f;

    [Header("Explosions")]
    [SerializeField] AudioClip[] explosionsClips;
    [SerializeField][Range(0.0f, 1.0f)] float explosionsVolume = 1.0f;

    float tempVol;

    public void PlayShootingClip(bool isEnemy)
    {
        if (shootingClips.Length == 0)
        {
            return;
        }
        else
        {
            int index = Random.Range(0, shootingClips.Length);
            PlayClip(shootingClips[index], SetVolume(shootingVolume, isEnemy));
        }
    }

    public void PlayExplosionClip(bool isPlayer)
    {
        if (explosionsClips.Length == 0)
        {
            return;
        }
        else
        {
            int index = Random.Range(0, explosionsClips.Length);
            PlayClip(explosionsClips[index], SetVolume(explosionsVolume, !isPlayer));
        }
    }

    private void PlayClip(AudioClip clip, float volume)
    {
        if(clip != null) 
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume);
        }
    }

    private float SetVolume(float volume, bool isEnemy)
    {
        tempVol = volume;
        if(isEnemy)
        {
            tempVol = volume / enemyDistanceVolumeDivider;
        }
        else
        {
            tempVol = volume;
        }
        return tempVol;
    }
}
