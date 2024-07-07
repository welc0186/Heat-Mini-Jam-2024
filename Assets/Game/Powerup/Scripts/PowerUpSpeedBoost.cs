using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpeedBoost : PowerUp
{
    
    public float NewSpeed = 6f;
    public float Duration = 5;
    
    protected override void Perform()
    {
        PlayerEvents.onPlayerSpeedSet.Invoke(new PlayerSpeed{
            Speed = NewSpeed,
            Duration = Duration
        });
        Destroy(gameObject);
    }
}
