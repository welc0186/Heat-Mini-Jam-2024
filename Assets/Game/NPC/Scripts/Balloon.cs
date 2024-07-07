using System;
using System.Collections;
using System.Collections.Generic;
using Alf.Utils;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Balloon : MonoBehaviour
{

    [SerializeField] float startingImpulse = 50f;
    [SerializeField] float startForce = 1f;
    [SerializeField] float endForce = 0.2f;
    [SerializeField] float maxVelocity = 10f;
    [SerializeField] private GameObject deathAnimation;
    [SerializeField] private TagDetectorCollider2D _playerDetector;
    [SerializeField] private TagDetectorCollider2D _floorDetector;
    float _force;
    float _forceTimer = 0;
    Rigidbody2D _rigidbody2D;
    Vector3 _prevPosition;

    void OnEnable()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _force = startForce;
    }

    void Start()
    {
        _rigidbody2D.AddForce(Vector3.up * UnityEngine.Random.Range(0, startingImpulse), ForceMode2D.Impulse);
    }

    void Update()
    {
        if(_floorDetector.Detected)
        {
            HandleDying();
            return;
        }
        HandleLift();
        CapVelocity();
    }

    private void CapVelocity()
    {
        var direction = transform.position.y > _prevPosition.y ? Vector3.up : Vector3.down;
        if (_rigidbody2D.velocity.magnitude > maxVelocity)
            _rigidbody2D.velocity = direction * maxVelocity;
        _prevPosition = transform.position;
    }

    private void HandleDying()
    {
        PlayerEvents.onPlayerLoseLife.Invoke();
        Instantiate(deathAnimation, transform.position, Quaternion.identity);
        deathAnimation.AddComponent<AnimatorSelfDestruct>();
        Destroy(gameObject);
    }

    private void HandleLift()
    {
        if (!_playerDetector.Detected)
        {
            _forceTimer = 0;
            return;
        }

        _forceTimer += Time.deltaTime;
        _force = Mathf.Lerp(startForce, endForce, _forceTimer);
        _rigidbody2D.AddForce(Vector2.up * _force, ForceMode2D.Force);
    }
}
