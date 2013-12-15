using System;
using System.Collections.Generic;
using Annotations;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    // The player game object
    public Transform Player;

    // Bomb prefab
    public GameObject Bomb;

    // Whether to spawn any enemies
    public bool IsStopped;

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

    // The calculated spawn frequency (before variance) in seconds
    private float spawnFrequency;

    // The time elapsed since a spawn point was created
    private float elapsed;

    // Calculate the frequency at which spawn points are created
    public void CalculateSpawnFrequency()
    {
        var freq = this.SpawnPointFrequencyCurve.Evaluate(this.seed);
        var variance = this.SpawnPointFrequencyVarianceCurve.Evaluate(this.seed);
        this.spawnFrequency = Random.Range(freq - variance, freq + variance);
        //Debug.Log("calculating spawn frequency");
        //Debug.Log("frequency: " + freq);
        //Debug.Log("variance: " + variance);
        //Debug.Log("result: " + this.spawnFrequency);
    }

    /*
     * Spawn amount
     */

    // The curve that determines how many spawn points will be created in this level
    public AnimationCurve SpawnPointAmountCurve;

    // The variance in amount (percentage 0..1)
    public AnimationCurve SpawnPointAmountVarianceCurve;

    // The calculated number of spawn points to create
    private int spawnAmount;

    // The amount of spawn points that have been created
    private int currentAmount;

    // Calculate the number of spawn points to create
    public void CalculateSpawnAmount()
    {
        float amount = this.SpawnPointAmountCurve.Evaluate(this.seed);

        var variancePercent = this.SpawnPointAmountVarianceCurve.Evaluate(this.seed);
        var variance = amount * variancePercent;

        //Debug.Log("calculating spawn amount");
        //Debug.Log("amount: " + amount);

        amount = Random.Range(amount - variance, amount + variance);
        this.spawnAmount = Mathf.RoundToInt(amount);

        //Debug.Log("variance: " + variancePercent + "% (" + variance + ")");
        //Debug.Log("result: " + amount);
        //Debug.Log("result (rounded): " + this.spawnAmount);
    }

    /*
     * Enemy amounts
     */

    // The curve that determines how many enemies will spawn from each spawn point
    public AnimationCurve EnemyAmountCurve;

    // The variance in amount (percentage 0..1)
    public AnimationCurve EnemyAmountVarianceCurve;

    // Calculate the number of enemies to create
    public int CalculateEnemyAmount()
    {
        float amount = this.EnemyAmountCurve.Evaluate(this.seed);

        var variancePercent = this.EnemyAmountVarianceCurve.Evaluate(this.seed);
        var variance = amount * variancePercent;

        //Debug.Log("calculating enemy amount");
        //Debug.Log("amount: " + amount);

        amount = Random.Range(amount - variance, amount + variance);
        var result = Mathf.RoundToInt(amount);

        //Debug.Log("variance: " + variancePercent + "% (" + variance + ")");
        //Debug.Log("result: " + amount);
        //Debug.Log("result (rounded): " + result);

        return result;
    }

    /*
     * Enemy types
     */

    public AnimationCurve PointEnemyCurve;
    public AnimationCurve LineEnemyCurve;
    public AnimationCurve TriangleEnemyCurve;
    public AnimationCurve SquareEnemyCurve;
    private List<GameObject> weightedEnemyTypes;

    private void GenerateWeightedEnemyTypes()
    {
        var enemyCurves = new List<AnimationCurve> { this.PointEnemyCurve, this.LineEnemyCurve, this.TriangleEnemyCurve, this.SquareEnemyCurve };
        this.weightedEnemyTypes = new List<GameObject>();
        for (var i = 0; i < enemyCurves.Count; i++) {
            var curve = enemyCurves[i];
            var t = curve.Evaluate(this.seed);
            var n = Mathf.RoundToInt(t);
            //Debug.Log(i + ": " + n);
            for (var j = 0; j < n; j++) {
                this.weightedEnemyTypes.Add(this.SpawnPoints[i]);
            }
        }
    }

    private GameObject RandomEnemyType()
    {
        var i = Random.Range(1, this.weightedEnemyTypes.Count) - 1;
        return this.weightedEnemyTypes[i];
    }

    /*
     * Proximity
     */

    /*
     * 
     */

    private void Initialize()
    {
        this.CalculateDifficulty();
        this.CalculateSpawnFrequency();
        this.CalculateSpawnAmount();
        this.GenerateWeightedEnemyTypes();

        this.IsStopped = false;
    }

    public void IncrementLevel()
    {
        // Reset bomb transform
        this.Bomb.transform.position = this.Player.transform.position;
        this.Bomb.transform.parent = this.Player.transform;
        this.Bomb.transform.localScale = new Vector3(0, 0, 1);

        this.Level += 1;
        this.Initialize();
    }

    [UsedImplicitly] private void Start()
    {
        Application.targetFrameRate = 60;
        this.Initialize();
    }

    [UsedImplicitly] private void Update()
    {
        if (this.IsStopped) return;

        if (this.elapsed > this.spawnFrequency && this.spawnAmount > this.currentAmount) {
            this.CreateSpawnPoint(this.RandomEnemyType());
            this.CalculateSpawnFrequency();
            this.elapsed = 0;
            this.currentAmount += 1;
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

        // Calculate the amount of enemies to spawn
        var spawnPointObject = (GameObject)Object.Instantiate(spawnPoint, new Vector3(x, y, 0), Quaternion.identity);
        var spawnPointScript = spawnPointObject.GetComponent<SpawnPoint>();
        spawnPointScript.EnemyCount = this.CalculateEnemyAmount();
        spawnPointScript.StartSpawning();
    }
}
