using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteFlipper : MonoBehaviour
{
    
    [SerializeField] private SpriteRenderer spriteRenderer;

    void OnEnable()
    {
        PlayerEvents.onPlayerDirectionSet.Subscribe(HandleDirection);
    }

    void OnDisable()
    {
        PlayerEvents.onPlayerDirectionSet.Unsubscribe(HandleDirection);
    }

    private void HandleDirection(Vector3 direction)
    {
        spriteRenderer.flipX = direction == Vector3.right;
    }
}
