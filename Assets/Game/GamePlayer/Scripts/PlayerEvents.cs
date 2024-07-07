using System;
using System.Collections;
using System.Collections.Generic;
using Alf.Utils;
using UnityEngine;

public static class PlayerEvents
{
    
    public static CustomEvent onPlayerStop = new();
    public static CustomEvent onPlayerMove = new();
    public static CustomEvent onPlayerSwitchDirection = new();
    public static CustomEvent<Vector3> onPlayerDirectionSet = new();
    public static CustomEvent<PlayerSpeed> onPlayerSpeedSet = new();
    public static CustomEvent onPlayerLoseLife = new();
    public static CustomEvent onPlayerGainLife = new();
    public static CustomEvent<int> onPlayerScore = new();
    public static CustomEvent onPlayerDeath = new();

}