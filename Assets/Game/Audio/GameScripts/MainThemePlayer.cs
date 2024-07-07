using System.Collections;
using System.Collections.Generic;
using Alf.Utils;
using UnityEngine;

namespace Alf.AudioUtils
{

public class MainThemePlayer : PersistentSingleton<MainThemePlayer>
{
    
    [SerializeField] AudioClipSO mainThemeSO;

    void OnEnable()
    {
        new AudioClipPlayer(mainThemeSO).Play();
    }

}
}