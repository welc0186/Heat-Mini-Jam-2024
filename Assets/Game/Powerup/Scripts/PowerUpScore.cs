using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScore : PowerUp
{
    
    public int scoreAmount;

    protected override void Perform()
    {
        PlayerEvents.onPlayerScore.Invoke(scoreAmount);
        Destroy(gameObject);
    }

}
