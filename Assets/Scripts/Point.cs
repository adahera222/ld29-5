using Annotations;
using UnityEngine;

public class Point : MonoBehaviour
{
    public float Velocity;
    public float Variance;

    [UsedImplicitly] private void Start()
    {
        this.transform.parent = GameObject.Find("Points").transform;
        this.MoveRandomly();
    }

    private void MoveRandomly()
    {
        var x = Random.Range(-1f, 1f);
        var y = Random.Range(-1f, 1f);

        var v = Random.Range(this.Velocity - this.Variance, this.Velocity + this.Variance);
        this.rigidbody2D.velocity = new Vector2(x, y).normalized * v;
    }
}
