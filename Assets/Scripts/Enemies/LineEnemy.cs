using UnityEngine;

public class LineEnemy : Enemy
{
    public float Velocity;

    internal override void Move()
    {
        base.Move();
        this.rigidbody2D.velocity = new Vector2();
        this.rigidbody2D.angularVelocity = 0;

        var d = this.Player.transform.position - this.transform.position;
        this.transform.position += (d.normalized * this.Velocity) * Time.deltaTime;

        var a = Quaternion.LookRotation(d);
        a.x = 0;
        a.y = 0;

        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, a, Time.deltaTime);
    }
}
