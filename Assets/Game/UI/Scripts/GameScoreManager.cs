using System;
using Alf.Utils;
using TMPro;
using UnityEngine;

public class GameScoreManager : MonoBehaviour
{
    
    [SerializeField] TMP_Text scoreText;
    [SerializeField] int scorePerSecond = 5;

    int _score;

    void OnEnable()
    {
        PlayerEvents.onPlayerScore.Subscribe(HandlePlayerScore);
        CoroutineTimer.Init(1).Timeout += AddPeriodicScore;
    }

    private void AddPeriodicScore()
    {
        _score += scorePerSecond;
        UpdateText();
        CoroutineTimer.Init(1).Timeout += AddPeriodicScore;
    }

    void OnDisable()
    {
        PlayerEvents.onPlayerScore.Unsubscribe(HandlePlayerScore);
    }

    private void HandlePlayerScore(int scoreAmount)
    {
        _score += scoreAmount;
        UpdateText();
    }

    // Start is called before the first frame update
    void Start()
    {
        _score = 0;
        UpdateText();
    }

    private void UpdateText()
    {
        scoreText.text = _score.ToString("D6");
    }

}
