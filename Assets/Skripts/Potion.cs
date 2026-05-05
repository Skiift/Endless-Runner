using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public int healAmount = 1;
    public int coinBonusIfFullHealth = 5;

    [Header("Effects")]
    public GameObject healEffect;
    public GameObject coinEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            PlayerScore score = other.GetComponent<PlayerScore>();

            if (health != null)
            {
                if (health.currentHealth < health.maxHealth)
                {
                    health.Heal(healAmount);
                    SpawnEffect(healEffect);
                }
                else if (score != null)
                {
                    score.AddCoins(coinBonusIfFullHealth);
                    SpawnEffect(coinEffect);
                }
            }

            Destroy(gameObject);
        }
    }

    private void SpawnEffect(GameObject effectPrefab)
    {
        if (effectPrefab != null)
        {
            Instantiate(effectPrefab, transform.position, Quaternion.identity);
        }
    }
}
