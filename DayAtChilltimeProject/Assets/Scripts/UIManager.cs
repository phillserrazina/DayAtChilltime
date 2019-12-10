using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text playerNameText = null;
    [SerializeField] private Text triesText = null;
    [SerializeField] private Text timerText = null;

    [Space(10)]
    [SerializeField] private GameObject cardBoardObject = null;
    [SerializeField] private GameObject winnerScreenObject = null;
    [SerializeField] private Text scoreText = null;

    private GameManager gameManager;

    private void Update() {
        triesText.text = string.Format("Tries: {0}", gameManager.totalTries);
        UpdateTimerText();
    }

    public void Initialize(GameManager gm) {
        gameManager = gm;
        playerNameText.text = gm.playerName;
    }

    public void TriggerWinnerScreen(float score) {
        scoreText.text = string.Format("Score: {0}", score.ToString("F0"));
        cardBoardObject.SetActive(false);
        winnerScreenObject.SetActive(true);
    }

    private void UpdateTimerText() {
        float minutes = Mathf.Floor(gameManager.timeElapsedInSeconds / 60);
        float seconds = Mathf.RoundToInt(gameManager.timeElapsedInSeconds % 60);
        
        string minutesText = minutes.ToString("F0");
        string secondsText = seconds.ToString("F0");

        if (seconds == 60) {
            minutes += 1;
            minutesText = minutes.ToString("F0");
            secondsText = "00";
        }

        if(seconds < 10) {
            secondsText = "0" + Mathf.RoundToInt(seconds).ToString();
        }

        timerText.text = string.Format("Time elapsed: {0}:{1}", minutesText, secondsText);
    }
}
