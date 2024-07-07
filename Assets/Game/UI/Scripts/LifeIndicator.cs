using System.Collections;
using System.Collections.Generic;
using Alf.Utils;
using Unity.VisualScripting;
using UnityEngine;

public class LifeIndicator : MonoBehaviour
{
    
    [SerializeField] GameObject[] lifeIndicators;

    int _maxLife;
    int _lifeTotal;

    void OnEnable()
    {
        PlayerEvents.onPlayerLoseLife.Subscribe(LoseLife);
        PlayerEvents.onPlayerGainLife.Subscribe(AddLife);
    }

    void OnDisable()
    {
        PlayerEvents.onPlayerLoseLife.Unsubscribe(LoseLife);
        PlayerEvents.onPlayerGainLife.Unsubscribe(AddLife);
    }

    void Start()
    {
        _lifeTotal = _maxLife = lifeIndicators.Length;
        foreach(var indicator in lifeIndicators)
            indicator.SetActive(true);
    }
    
    void LoseLife()
    {
        _lifeTotal = _lifeTotal - 1;
        if (_lifeTotal <= 0)
        {
            Debug.Log("No more lives!");
            return;
        }
        lifeIndicators[_lifeTotal].SetActive(false);
    }

    void AddLife()
    {
        if(_lifeTotal >= _maxLife)
            return;
        lifeIndicators[_lifeTotal].SetActive(true);
        _lifeTotal++;
        if (_lifeTotal > _maxLife)
            _lifeTotal = _maxLife;
    }

}
