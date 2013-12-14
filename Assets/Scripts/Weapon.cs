using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject Bullet;
    public float BulletSpeed;
    public float Cooldown;
    private float lastFired;

    public void Fire(Vector2 direction)
    {
        if (this.lastFired > this.Cooldown) {
            var bullet = (GameObject) Object.Instantiate(this.Bullet, this.transform.position, Quaternion.identity);
            bullet.rigidbody2D.velocity = direction * this.BulletSpeed;
            this.lastFired = 0;
        }

        this.lastFired += Time.deltaTime;
    }
}
