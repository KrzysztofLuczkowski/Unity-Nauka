using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotSpawner : MonoBehaviour
{
    public GameObject carrotPrefab; // Prefab marchewki
    public int carrotCount = 20; // Liczba marchewek do rozmieszczenia
    public Vector2 spawnAreaX = new Vector2(-3, 70); // Zakres spawnów na osi X
    public Vector2 spawnAreaZ = new Vector2(-20, 20); // Zakres spawnów na osi Z

    void Start()
    {
        for (int i = 0; i < carrotCount; i++)
        {
            SpawnCarrot();
        }
    }

    void SpawnCarrot()
    {
        float randomX = Random.Range(spawnAreaX.x, spawnAreaX.y);
        float randomZ = Random.Range(spawnAreaZ.x, spawnAreaZ.y);
        Vector3 spawnPosition = new Vector3(randomX, 0.5f, randomZ);

        if (IsWithinBounds(spawnPosition))
        {
            Instantiate(carrotPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Poza wyznaczonym obszarem, ponawiam próbê.");
            SpawnCarrot();
        }
    }

    bool IsWithinBounds(Vector3 position)
    {
        return position.x >= spawnAreaX.x && position.x <= spawnAreaX.y &&
               position.z >= spawnAreaZ.x && position.z <= spawnAreaZ.y;
    }
}
