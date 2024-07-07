using System.Collections;
using System.Collections.Generic;
using Alf.GameManagement;
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
        if (_lifeTotal < 0)
            _lifeTotal = 0;
        lifeIndicators[_lifeTotal].SetActive(false);
        if (_lifeTotal <= 0)
        {
            PlayerEvents.onPlayerDeath.Invoke();
        }        
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
