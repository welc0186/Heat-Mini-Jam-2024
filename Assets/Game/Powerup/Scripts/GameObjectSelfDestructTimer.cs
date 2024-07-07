using System;
using Alf.Utils;
using UnityEngine;

public class GameObjectSelfDestructTimer : MonoBehaviour
{
    GameObject _destroyTarget;
    float _lifeSeconds;

    public static void SelfDestruct(GameObject target, float seconds)
    {
        var newObject = new GameObject(target.name + "Self Destruct", typeof(GameObjectSelfDestructTimer));
        var component = newObject.GetComponent<GameObjectSelfDestructTimer>();
        component._destroyTarget = target;
        component._lifeSeconds = seconds;
    }

    void Start()
    {
        if (_destroyTarget == null)
        {
            Debug.LogWarning("Use 'SelfDestruct' method to initialize Self Destruct Timer");
            Destroy(gameObject);
            return;
        }

        CoroutineTimer.Init(_lifeSeconds).Timeout += Expire;
    }

    private void Expire()
    {
        if (_destroyTarget != null)
            Destroy(_destroyTarget);
        Destroy(gameObject);
    }
}