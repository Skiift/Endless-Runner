using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public enum ObjectType { Obstacle }
    public ObjectType type;

    [Header("Effects")]
    public GameObject hitEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();

            if (health != null)
            {
                if (type == ObjectType.Obstacle)
                {
                    health.TakeDamage(1);

                    if (hitEffect != null)
                    {
                        Instantiate(hitEffect, transform.position, Quaternion.identity);
                    }
                }
            }

            Destroy(gameObject);
        }
    }
}
