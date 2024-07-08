using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCMoveBehaviour
{
    
    protected GameObject _npc;

    public NPCMoveBehaviour(GameObject npc)
    {
        _npc = npc;
    }

    public abstract Vector3 Move();

}
