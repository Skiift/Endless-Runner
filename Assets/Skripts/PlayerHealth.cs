using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public GameObject[] heartImages;

    [Header("Game Over UI")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI finalTimeText;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
        gameOverPanel.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].SetActive(i < currentHealth);
        }
    }

    void Die()
    {
        Time.timeScale = 0;

        gameOverPanel.SetActive(true);

        PlayerScore scoreScript = GetComponent<PlayerScore>();
        scoreScript.StopTimer();

        finalScoreText.text = "Total Coins: " + scoreScript.coins;
        finalTimeText.text = "Survived " + scoreScript.timerText.text;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
