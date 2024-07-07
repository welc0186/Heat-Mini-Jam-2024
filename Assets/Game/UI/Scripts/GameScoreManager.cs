using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameScoreManager : MonoBehaviour
{
    
    [SerializeField] TMP_Text scoreText;

    int _score;

    void OnEnable()
    {
        PlayerEvents.onPlayerScore.Subscribe(HandlePlayerScore);
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
