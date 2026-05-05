using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    public int coins;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    private float startTime;
    private bool isDead = false;

    void Start()
    {
        coins = 0;
        startTime = Time.time;
        UpdateScoreUI();
    }

    void Update()
    {
        if (!isDead)
        {
            float t = Time.time - startTime;
            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f2");

            if (timerText != null)
                timerText.text = " " + minutes + ":" + seconds;
        }
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = " " + coins;
    }

    public void StopTimer()
    {
        isDead = true;
    }
}
