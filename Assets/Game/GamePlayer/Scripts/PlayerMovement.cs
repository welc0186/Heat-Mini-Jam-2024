using System;
using System.Collections;
using System.Collections.Generic;
using Alf.Utils;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public const float BASE_SPEED = 4f;
    float _speed = BASE_SPEED;
    bool _moving = true;
    Vector3 _direction = Vector3.right;

    void OnEnable()
    {
        PlayerEvents.onPlayerStop.Subscribe(HandlePlayerStop);
        PlayerEvents.onPlayerMove.Subscribe(HandlePlayerMove);
        PlayerEvents.onPlayerSwitchDirection.Subscribe(SwitchDirection);
        PlayerEvents.onPlayerSpeedSet.Subscribe(SpeedBoost);
    }

    void OnDisable()
    {
        PlayerEvents.onPlayerStop.Unsubscribe(HandlePlayerStop);
        PlayerEvents.onPlayerMove.Unsubscribe(HandlePlayerMove);
        PlayerEvents.onPlayerSwitchDirection.Unsubscribe(SwitchDirection);
        PlayerEvents.onPlayerSpeedSet.Unsubscribe(SpeedBoost);
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

    private void HandlePlayerMove()
    {
        _moving = true;
    }

    private void HandlePlayerStop()
    {
        _moving = false;
    }

    public void Move(bool move)
    {
        _moving = move;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMove();
    }

    private void HandleMove()
    {
        if (!_moving)
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
    }
}
