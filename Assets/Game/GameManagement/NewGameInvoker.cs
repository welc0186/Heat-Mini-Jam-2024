using System;
using System.Collections;
using System.Collections.Generic;
using Alf.Utils;
using UnityEngine;

namespace Alf.GameManagement
{
public class NewGameInvoker : MonoBehaviour
{

    [SerializeField] float waitSeconds;

    void OnEnable()
    {
        Time.timeScale = 1;
        CoroutineTimer.Init(waitSeconds).Timeout += () => GameEvents.onNewGame.Invoke();
    }

}
}