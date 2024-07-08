using System.Collections;
using System.Collections.Generic;
using Alf.Game.NPC;
using UnityEngine;

public class NPCMoveInDirection : NPCMoveBehaviour
{
    
    Vector3 _direction;
    float _speed;

    public NPCMoveInDirection(GameObject npc, Vector3 direction, float unitsPerSecond) : base(npc)
    {
        _direction = direction.normalized;
        _speed = unitsPerSecond;
    }

    public override Vector3 Move()
    {
        var currPos = _npc.transform.position;
        return currPos + _direction * (_speed * Time.deltaTime);
    }
}
