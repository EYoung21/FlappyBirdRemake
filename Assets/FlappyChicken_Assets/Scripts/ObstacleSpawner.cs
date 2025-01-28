using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    public GameObject bottom;
    public GameObject top;

    public GameObject scoreTracker;

    private float _currentTimer;
    public float spawnTimer;

    private void SpawnNewObstacles() {
        var distanceMin = 4.2f;
        var distanceMax = 6.5f;
        Vector3 downVector = Vector3.down;
        downVector.z = 0;
        downVector.y = -1 * Random.Range(distanceMin, distanceMax);

        Vector3 upVector = Vector3.down;
        upVector.z = 0;
        upVector.y = 1 * Random.Range(distanceMin, distanceMax);

        Vector3 spawnPositionDown = transform.position + downVector;
        Vector3 spawnPositionUp = transform.position + upVector;
        
        Instantiate(bottom, spawnPositionDown, Quaternion.identity);
        Instantiate(top, spawnPositionUp, Quaternion.identity);

        Vector3 shiftRight = new Vector3(1, 0, 0);
        Vector3 scoreTrackerSpawnPosition = transform.position + downVector + upVector + shiftRight; //shift right by 1

        Instantiate(scoreTracker, scoreTrackerSpawnPosition, Quaternion.identity);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _currentTimer += Time.deltaTime;
        if (_currentTimer >= spawnTimer) {
            SpawnNewObstacles();
            _currentTimer = 0;
        }
    }
}
