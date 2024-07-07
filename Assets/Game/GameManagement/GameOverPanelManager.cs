using System;
using System.Collections;
using System.Collections.Generic;
using Alf.Utils;
using UnityEngine;

namespace Alf.GameManagement
{
public class GameOverPanelManager : MonoBehaviour
{

    const float FLICKER_SECONDS = 0.5f;
    const float DELAY_SECONDS = 5;
    
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject gameOverText;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        gameOverText.SetActive(true);
        panel.SetActive(false);
        GameEvents.onGameOver.Subscribe(HandleGameOver);
    }

    void OnDisable()
    {
        GameEvents.onGameOver.Unsubscribe(HandleGameOver);
    }

    private void HandleGameOver()
    {
        panel.SetActive(true);
        gameOverText.SetActive(true);
        CoroutineTimer.Init(DELAY_SECONDS, true).Timeout += () => 
        {
            GameEvents.onMainMenuEvent.Invoke();
        };
        CoroutineTimer.Init(FLICKER_SECONDS, true).Timeout += FlickerText;
    }

    private void FlickerText()
    {
        gameOverText.SetActive(!gameOverText.activeInHierarchy);
        CoroutineTimer.Init(FLICKER_SECONDS, true).Timeout += FlickerText;
    }

}
}