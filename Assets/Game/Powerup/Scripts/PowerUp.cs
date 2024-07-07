using System;
using System.Collections;
using System.Collections.Generic;
using Alf.Utils;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class PowerUp : MonoBehaviour
{

    public float ExpireSeconds = 5f;
    CoroutineTimer _timerCoroutine;

    void OnEnable()
    {
        _timerCoroutine = CoroutineTimer.Init(ExpireSeconds);
        _timerCoroutine.Timeout += Expire;
    }

    void OnDisable()
    {
        _timerCoroutine.Timeout -= Expire;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
            Perform();
    }

    protected void Expire()
    {
        Destroy(gameObject);
    }

    protected abstract void Perform();
}
