using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakeDuration = 1f;
    [SerializeField] private float shakeMagnitude = 0.5f;
    private Vector3 initialPosition;
    Health health;
    Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
        health = player.GetComponent<Health>();
        initialPosition = transform.position;
        health.OnTakeDamage += OnTakeDamage;
    }

    private void OnTakeDamage(object sender, HealthEventArgs e)
    {
        if(e.IsPlayer)
        {
            StartCoroutine(ShakeCamera()); 
        }
    }

    private IEnumerator ShakeCamera()
    {
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.position = new Vector3(x, y, initialPosition.z);

            elapsed += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
        transform.position = initialPosition;
    }

}
