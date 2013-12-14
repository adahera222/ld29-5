using UnityEngine;

public class LineEnemy : Enemy
{
    public float Velocity;

    internal override void Move()
    {
        base.Move();
        //this.rigidbody2D.velocity = new Vector2();
        //var d =  this.Player.transform.position - this.transform.position;
        //this.transform.position += (d.normalized * this.Velocity) * Time.deltaTime;

    }
}
