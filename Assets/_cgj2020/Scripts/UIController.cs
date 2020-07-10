using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIController : MonoBehaviour
{
    public GameObject GameCanvas;
    public GameObject GameOverMenu; 
    public GameObject MenuCanvas;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI gameOverScore;

    public void StartGame()
    {
        GameManager.Instance.GameStart();
    }

    //public void UpdateScore()
    //{
    //    scoreText.text = "REBOOTED TOTEMS: " + GameManager.Instance.score;
    //}

    public void UpdateTimer()
    {
        TimeSpan time = TimeSpan.FromSeconds(GameManager.Instance.timer);
        timerText.text = string.Format("{0:D2}:{1:D2}", time.Minutes, time.Seconds);
    }

    public void SetGameOver()
    {
        timerText.text = "";
        GameOverMenu.SetActive(true);
        TimeSpan totalTime = TimeSpan.FromSeconds(GameManager.Instance.totalSeconds);
        string seconds = string.Format("{0:D2}:{1:D2}", totalTime.Minutes, totalTime.Seconds);

        gameOverScore.text = "IN " + seconds + " YOU MADE " + GameManager.Instance.rounds + " ROUNDS AND "  + GameManager.Instance.score + " ROBOTS WERE REBOOTED!";
        //GameCanvas.SetActive(false);
    }

    public void GoBackToMenu()
    {
        Destroy(GameManager.Instance.currentTotem.gameObject);
        Destroy(GameManager.Instance.currentTotemGuide.gameObject);
        GameManager.Instance.currentTotem = null;
        GameManager.Instance.currentTotemGuide = null;
    }
}
