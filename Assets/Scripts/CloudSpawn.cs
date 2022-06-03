using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawn : MonoBehaviour
{
    [SerializeField] GameObject[] clouds;
    [SerializeField] float spawnInterval;

    Vector3 spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = transform.position;

        InvokeRepeating("SpawnCloud", 0.1f, spawnInterval);
    }

    private void Update()
    {
        spawnPoint = transform.position;
    }
    // Update is called once per frame
    void SpawnCloud()
    {
        // Instantiating cloud objects from the array
        GameObject cloud = Instantiate(clouds[Random.Range(0, clouds.Length)]);

        // Setting spawn heights for clouds
        spawnPoint.y = Random.Range(spawnPoint.y - 1f, spawnPoint.y + 2f);
        cloud.transform.position = spawnPoint;

    }

}
