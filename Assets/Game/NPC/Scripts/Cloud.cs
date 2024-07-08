using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cloud : MonoBehaviour
{
    
    const string PATH = "CloudPrefab";
    const float MIN_X = -11;

    [SerializeField] private float speed = -1;
    [SerializeField] Sprite[] sprites;
    [SerializeField] string[] spriteLayers;

    NPCMoveBehaviour _moveBehaviour;
    bool _initialized = false;

    public static GameObject Spawn(float unitsPerSecond)
    {
        var ret = GameObject.Instantiate(Resources.Load<GameObject>(PATH));
        var cloud = ret.GetComponent<Cloud>();
        var spriteRenderer = ret.GetComponent<SpriteRenderer>();
        cloud.speed = unitsPerSecond;
        spriteRenderer.sprite = cloud.sprites[Random.Range(0, cloud.sprites.Length)];
        spriteRenderer.sortingLayerName = cloud.spriteLayers[Random.Range(0,cloud.spriteLayers.Length)];
        return ret;
    }

    void Update()
    {
        if(!_initialized)
            Init();
        gameObject.transform.position = _moveBehaviour.Move();
        if(transform.position.x < MIN_X)
            Destroy(gameObject);
    }

    private void Init()
    {
        if(speed <= 0)
        {
            Destroy(gameObject);
            return;
        }
        _moveBehaviour = new NPCMoveInDirection(gameObject, Vector3.left, speed);
    }
}
