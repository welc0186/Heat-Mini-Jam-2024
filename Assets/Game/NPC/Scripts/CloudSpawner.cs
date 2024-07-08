using Alf.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

public class CloudSpawner : MonoBehaviour
{
    
    [SerializeField] float minIntervalSeconds = 0.1f;
    [SerializeField] float maxIntervalSeconds = 4;
    [SerializeField] float minCloudSpeed = 2;
    [SerializeField] float maxCloudSpeed = 4;

    Random2DPositionTimer _posTimer;

    void OnEnable()
    {
        var corners = new Vector3[4];
        GetComponent<RectTransform>().GetWorldCorners(corners);
        var bounds = new Bounds();
        bounds.min = new Vector3(corners[0].x, corners[0].y, 0);
        bounds.max = new Vector3(corners[2].x, corners[2].y);
        _posTimer = new Random2DPositionTimer(minIntervalSeconds, maxIntervalSeconds, bounds);
    }

    void Update()
    {
        Vector3 position = Vector3.zero;
        if(_posTimer.GetPosition(ref position, Time.deltaTime))
            SpawnCloud(position);
    }

    void SpawnCloud(Vector3 position)
    {
        var cloudGO = Cloud.Spawn(Random.Range(minCloudSpeed, maxCloudSpeed));
        cloudGO.transform.position = position;
    }
}
