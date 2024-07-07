using System;
using System.Collections;
using System.Collections.Generic;
using Alf.Utils;
using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    
    [SerializeField] GameObject balloonPrefab;
    
    const int BASE_MIN_BALLOONS = 3;
    const float RAMP_SECONDS = 60;
    
    List<GameObject> _spawnSlots;
    int _numBalloons;
    int _minBalloons;
    
    void Awake()
    {
        InitSpawnSlots();
        _minBalloons = BASE_MIN_BALLOONS - 1;
        RampTimer();
    }

    private void RampTimer()
    {
        _minBalloons++;
        CoroutineTimer.Init(RAMP_SECONDS).Timeout += RampTimer;
    }

    private void InitSpawnSlots()
    {
        _spawnSlots = new List<GameObject>();
        foreach (Transform childTransform in transform)
        {
            _spawnSlots.Add(childTransform.gameObject);
        }
    }

    void Update()
    {
        CountBalloons();
        HandleSpawnBalloons();
    }

    private void HandleSpawnBalloons()
    {
        if(_numBalloons >= _minBalloons || _numBalloons >= _spawnSlots.Count)
            return;
        var i = GetRandomUnoccupiedSlot();
        Instantiate(balloonPrefab, _spawnSlots[i].transform);
    }

    int GetRandomUnoccupiedSlot()
    {
        if (_numBalloons >= _spawnSlots.Count)
            return -1;
        var ret = UnityEngine.Random.Range(0, _spawnSlots.Count);
        if(_spawnSlots[ret].transform.childCount > 0)
            return GetRandomUnoccupiedSlot();
        return ret;
    }

    private void CountBalloons()
    {
        _numBalloons = 0;
        foreach(var spawnSlot in _spawnSlots)
        {
            if(spawnSlot.transform.childCount > 0)
                _numBalloons++;
        }
    }

    
}
