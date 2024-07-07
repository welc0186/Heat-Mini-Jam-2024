using System.Collections;
using System.Collections.Generic;
using Alf.GameManagement;
using Alf.Utils;
using UnityEngine;

public class TestGameOver : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        CoroutineTimer.Init(2).Timeout += () => GameEvents.onGameOver.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
