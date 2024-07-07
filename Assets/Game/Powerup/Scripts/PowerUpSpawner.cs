using System;
using System.Collections;
using System.Collections.Generic;
using Alf.Utils;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    
    [SerializeField] private float spawnSeconds;
    [SerializeField] private float spawnDeviationSeconds;
    [SerializeField] private GameObject[] powerUpPrefabs;

    List<GameObject> _spawnSlots;
    
    // Start is called before the first frame update
    void Awake()
    {
        InitSpawnSlots();
        CoroutineTimer.Init(spawnSeconds).Timeout += SpawnPowerUp;
    }

    private void SpawnPowerUp()
    {
        var s = RandomUnoccupiedSlot();
        var p = UnityEngine.Random.Range(0, powerUpPrefabs.Length);
        Instantiate(powerUpPrefabs[p], _spawnSlots[s].transform);

        var time = UnityEngine.Random.Range(spawnSeconds - spawnDeviationSeconds, spawnSeconds + spawnDeviationSeconds);
        CoroutineTimer.Init(time).Timeout += SpawnPowerUp;
    }

    private int RandomUnoccupiedSlot()
    {
        var ret = UnityEngine.Random.Range(0, _spawnSlots.Count);
        var playerDetector = _spawnSlots[ret].GetComponent<TagDetectorCollider2D>();
        if(_spawnSlots[ret].transform.childCount > 0 || playerDetector.Detected)
            return RandomUnoccupiedSlot();
        return ret;
    }

    private void InitSpawnSlots()
    {
        _spawnSlots = new List<GameObject>();
        foreach (Transform childTransform in transform)
        {
            _spawnSlots.Add(childTransform.gameObject);
        }
    }

}
