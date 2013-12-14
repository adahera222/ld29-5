using UnityEngine;

public class TriangleEnemy : Enemy
{
    public float MaxDistance;
    public AnimationCurve MovementCurve;
    public float Velocity;

    internal override void Move()
    {
        base.Move();
        this.rigidbody2D.velocity = new Vector2();
        var d = this.Player.transform.position - this.transform.position;
        var t = d.magnitude / this.MaxDistance;
        var v = this.Velocity * this.MovementCurve.Evaluate(t);

        this.transform.position += (d.normalized * v) * Time.deltaTime;
    }
}
