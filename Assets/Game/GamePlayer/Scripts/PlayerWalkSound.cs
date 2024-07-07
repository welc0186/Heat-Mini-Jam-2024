using System;
using System.Collections;
using System.Collections.Generic;
using Alf.AudioUtils;
using Alf.Utils;
using UnityEngine;

public class PlayerWalkSound : MonoBehaviour
{
    CoroutineTimer _timer;
    bool _playing;
    AudioClipPlayer _player;

    [SerializeField] float periodSeconds;
    [SerializeField] AudioClipSO walkAudio;
    // Start is called before the first frame update
    void OnEnable()
    {
        _playing = true;
        PlayerEvents.onPlayerMove.Subscribe(HandlePlayerMove);
        PlayerEvents.onPlayerStop.Subscribe(HandlePlayerStop);
        _timer = CoroutineTimer.Init(periodSeconds);
        _timer.Timeout += PlayAudio;
        _player = new AudioClipPlayer(walkAudio);
    }

    void OnDisable()
    {
        PlayerEvents.onPlayerMove.Unsubscribe(HandlePlayerMove);
        PlayerEvents.onPlayerStop.Unsubscribe(HandlePlayerStop);
        _timer.Timeout -= PlayAudio;
    }

    private void HandlePlayerStop()
    {
        _playing = false;
    }

    private void HandlePlayerMove()
    {
        _playing = true;
    }

    private void PlayAudio()
    {
        _timer = CoroutineTimer.Init(periodSeconds);
        _timer.Timeout += PlayAudio;

        if(!_playing)
            return;
        
        _player.Play();
    }

}
