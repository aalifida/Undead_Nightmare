using System.Collections;
using UnityEngine;

public class RandomZombieGeneration : MonoBehaviour
{
    public GameObject[] zombiePrefabs; // Array of zombie prefabs
    public Transform[] randomLocations;

    void Start()
    {
        StartCoroutine(ZombieGenerationCoroutine());
    }

    IEnumerator ZombieGenerationCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            ZombieGeneration();
        }
    }

    void ZombieGeneration()
    {
        int randomIndex = Random.Range(0, randomLocations.Length);
        Transform spawnPoint = randomLocations[randomIndex];

        // Choose a random zombie prefab from the array
        int randomZombieType = Random.Range(0, zombiePrefabs.Length);
        GameObject prefabToSpawn = zombiePrefabs[randomZombieType];

        Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);
    }
}
