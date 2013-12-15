using Annotations;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    // Spawn point prefabs in ascending difficulty
    public GameObject[] SpawnPoints;

    // The current level of difficulty
    public int Level;

    // The maximum difficulty level
    public int MaxLevel;

    // Seed calculated from current level
    private float seed;

    private void CalculateDifficulty()
    {
        this.seed = (float)this.Level / this.MaxLevel;
    }

    /*
     * Spawn frequency
     */

    // The curve that determines the rate at which spawn points are created
    public AnimationCurve SpawnPointFrequencyCurve;

    // The variance in spawn frequency (percentage 0..1)
    public AnimationCurve SpawnPointFrequencyVarianceCurve;

    // The actual spawn frequency (before variance) in seconds
    private float spawnFrequency;

    // The time elapsed since a spawn point was created
    private float elapsed;

    // Calculate the frequency at which spawn points are created
    public void CalculateSpawnFrequency()
    {
        var freq = this.SpawnPointFrequencyCurve.Evaluate(this.seed);
        var variance = this.SpawnPointFrequencyVarianceCurve.Evaluate(this.seed);
        this.spawnFrequency = Random.Range(freq - variance, freq + variance);
        Debug.Log("frequency: " + freq);
        Debug.Log("variance: " + variance);
        Debug.Log("result: " + this.spawnFrequency);
    }

    /*
     * Spawn amounts
     */

    /*
     * Spawn proximity
     */

    /*
     * Enemy amounts
     */

    /*
     * Enemy types
     */

    [UsedImplicitly] private void Start()
    {
        this.CalculateDifficulty();
        this.CalculateSpawnFrequency();
    }

    [UsedImplicitly] private void Update()
    {
        if (this.elapsed > this.spawnFrequency) {
            this.CreateSpawnPoint(this.SpawnPoints[2]);
            this.CalculateSpawnFrequency();
            this.elapsed = 0;
        }
        this.elapsed += Time.deltaTime;
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
