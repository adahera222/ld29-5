using System.Collections.Generic;
using Annotations;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public GameObject[] SpawnPoints;
    public GameObject Enemies;

    public float Cooldown;
    private float lastSpawned;

    [UsedImplicitly] private void Update()
    {
        if (this.lastSpawned > this.Cooldown) {
            var rand = Random.Range(1, this.SpawnPoints.Length) - 1;
            this.CreateSpawnPoint(this.SpawnPoints[rand]);
            this.lastSpawned = 0;
        }

        this.lastSpawned += Time.deltaTime;
    }

    private void CreateSpawnPoint(GameObject spawnPoint)
    {
        const float ppm = 100;
        const float padding = 0.3f;

        var boundsX = (Screen.width / 2f) / ppm;
        var boundsY = (Screen.height / 2f) / ppm;

        var x = Random.Range(-boundsX + padding, boundsX - padding);
        var y = Random.Range(-boundsY + padding, boundsY - padding);

        Object.Instantiate(spawnPoint, new Vector3(x, y, 0), Quaternion.identity);
    }
}
