using UnityEngine;
using System.Collections.Generic;
using System;

public class ExplosionFX : MonoBehaviour
{
    [SerializeField] private List<GameObject> particlePrefabs;

    private void Start()
    {
        Health health = GetComponent<Health>();
        if (health != null)
        {
            health.OnTakeDamage += OnTakeDamage;
        }
    }

    private void OnTakeDamage(object sender, HealthEventArgs e)
    {
        PlayExplosion(UnityEngine.Random.Range(0, particlePrefabs.Count), e.Position);
    }

    private void PlayExplosion(int index, Vector2 position)
    {
        if (index >= 0 && index < particlePrefabs.Count)
        {
            GameObject particleInstance = Instantiate(particlePrefabs[index], position, Quaternion.identity);
            Destroy(particleInstance, 2f);
        }
        else
        {
            Debug.LogWarning("Invalid particle index");
        }
    }
}
