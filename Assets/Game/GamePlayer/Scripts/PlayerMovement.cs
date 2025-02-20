using System;
using System.Collections;
using System.Collections.Generic;
using Alf.GameManagement;
using Alf.Utils;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    const float SWITCH_THRESHOLD = 0.15f;
    
    public const float BASE_SPEED = 4f;
    float _speed = BASE_SPEED;
    float _switchTimer = 0;
    bool _pressed = false;
    bool _moving = true;
    Vector3 _direction = Vector3.right;

    void OnEnable()
    {
        PlayerEvents.onPlayerInputPressed.Subscribe(HandleInputPressed);
        PlayerEvents.onPlayerInputReleased.Subscribe(HandleInputReleased);
        PlayerEvents.onPlayerSwitchDirection.Subscribe(SwitchDirection);
        PlayerEvents.onPlayerSpeedSet.Subscribe(SpeedBoost);
        PlayerEvents.onPlayerDeath.Subscribe(HandlePlayerDeath);
    }

    void OnDisable()
    {
        PlayerEvents.onPlayerInputPressed.Unsubscribe(HandleInputPressed);
        PlayerEvents.onPlayerInputReleased.Unsubscribe(HandleInputReleased);
        PlayerEvents.onPlayerSwitchDirection.Unsubscribe(SwitchDirection);
        PlayerEvents.onPlayerSpeedSet.Unsubscribe(SpeedBoost);
        PlayerEvents.onPlayerDeath.Unsubscribe(HandlePlayerDeath);
    }

    private void HandleInputReleased()
    {
        _pressed = false;
    }

    private void HandleInputPressed()
    {
        _pressed = true;
    }

    private void HandlePlayerDeath()
    {
        _speed = 0;
        CoroutineTimer.Init(0.5f).Timeout += () => Time.timeScale = 0;
        CoroutineTimer.Init(1, true).Timeout += () => GameEvents.onGameOver.Invoke();
    }

    private void SpeedBoost(PlayerSpeed speed)
    {
        _speed = speed.Speed;
        CoroutineTimer.Init(speed.Duration).Timeout += () => _speed = BASE_SPEED;
    }

    void Start()
    {
        PlayerEvents.onPlayerDirectionSet.Invoke(_direction);
    }

    // Update is called once per frame
    void Update()
    {
        HandlePressed();
        HandleMove();
    }

    private void HandlePressed()
    {
        if(!_pressed)
        {
            if (_switchTimer > 0 && _switchTimer < SWITCH_THRESHOLD)
                SwitchDirection();
            _switchTimer = 0;
            _moving = true;
            return;
        }

        _switchTimer += Time.deltaTime;
        _moving = false;
    }

    private void HandleMove()
    {        
        if(!_moving)
            return;
        transform.Translate(_direction * _speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag != "Wall")
            return;
        SwitchDirection();
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.name == "LeftWall")
        {
            _direction = Vector3.right;
        }
        if(collider.gameObject.name == "RightWall")
        {
            _direction = Vector3.left;
        }
        PlayerEvents.onPlayerDirectionSet.Invoke(_direction);
    }
    private void SwitchDirection()
    {
        _direction = _direction == Vector3.right? Vector3.left : Vector3.right;
        PlayerEvents.onPlayerDirectionSet.Invoke(_direction);
        PlayerEvents.onPlayerSwitchedDirection.Invoke();
    }
}
