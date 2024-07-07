using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanTweenFloat : MonoBehaviour
{

    // Height to float up and down
    public float floatHeight = 1.0f;
    // Duration for one complete up and down cycle
    public float floatDuration = 2.0f;

    // Initial position
    private Vector3 _initialPosition;

    void Start()
    {
        _initialPosition = transform.position;
        MoveUp();
    }

    void MoveUp()
    {
        // Move the GameObject up
        LeanTween.moveY(gameObject, _initialPosition.y + floatHeight, floatDuration / 2).setEaseInOutSine().setOnComplete(MoveDown);
    }

    void MoveDown()
    {
        // Move the GameObject down
        LeanTween.moveY(gameObject, _initialPosition.y - floatHeight, floatDuration / 2).setEaseInOutSine().setOnComplete(MoveUp);
    }

}
