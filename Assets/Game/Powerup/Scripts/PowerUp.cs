using System;
using System.Collections;
using System.Collections.Generic;
using Alf.Utils;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class PowerUp : MonoBehaviour
{

    public float ExpireSeconds = 5f;

    void OnEnable()
    {
        GameObjectSelfDestructTimer.SelfDestruct(gameObject, ExpireSeconds);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
            Perform();
    }

    protected abstract void Perform();
}
