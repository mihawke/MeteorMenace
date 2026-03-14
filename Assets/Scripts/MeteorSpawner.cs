using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField] private Meteor meteor;

    private float timeInterval = 3f;
    private float currentTime;

    private void Start()
    {
        currentTime = timeInterval;
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0f)
        {
            SpawnMeteor();
            currentTime = timeInterval;
        }
    }

    private void SpawnMeteor()
    {
        Instantiate(meteor, new Vector2(Random.Range(-8f, 8f), transform.position.y), Quaternion.identity);
    }
}
