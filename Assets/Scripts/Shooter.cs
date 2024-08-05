using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float projectileLifetime = 5f;
    [Header("Firing Rate")]
    [SerializeField] private float minFiringRate = 0.8f;
    [SerializeField] private float maxFiringRate = 2f;
    [SerializeField] private float baseFiringRate = 0.2f; // Time between shots

    private bool isEnemy = false; 
    public bool isFiring = false;
    private Coroutine firingCoroutine;

    private void Start()
    {
        if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            isEnemy = true;
            isFiring = true;
        }
    }

    void Update()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    private IEnumerator FireContinuously()
    {
        if(isEnemy)
        {   
            moveSpeed = -moveSpeed;
        }
        while (isFiring)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, moveSpeed);
            Destroy(projectile, projectileLifetime);
            yield return new WaitForSeconds(GetRandomFiringRate()); // Control the firing rate
        }
    }

    private float GetRandomFiringRate()
    {
        if(!isEnemy)
        {
            return baseFiringRate;
        }
        return Random.Range(minFiringRate, maxFiringRate);
    }
}
