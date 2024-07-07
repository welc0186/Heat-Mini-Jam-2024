using System;
using Alf.Utils;
using Alf.Utils.SerializableTypes;
using UnityEngine;

namespace Alf.AudioUtils
{
[System.Serializable]
public class AudioClipTrigger
{
    public AudioClipSO audioClip;

    [TypeFilter(typeof(ICustomEvent))]
    [SerializeField] SerializableType[] startTriggers;

    [TypeFilter(typeof(ICustomEvent))]
    [SerializeField] SerializableType[] stopTriggers;

    public void RegisterTriggers()
    {
        var player = new AudioClipPlayer(audioClip);
        for(int i = 0; i < startTriggers.Length; i++)
        {
            var trigger = (ICustomEvent) Activator.CreateInstance(startTriggers[i]);
            trigger.Event.Subscribe(() => player.Play());
        }
        for(int i = 0; i < stopTriggers.Length; i++)
        {
            var trigger = (ICustomEvent) Activator.CreateInstance(stopTriggers[i]);
            trigger.Event.Subscribe(() => player.Stop());
        }
    }
}
}
