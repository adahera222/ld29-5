using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject Bullet;
    public float BulletSpeed;
    public float Cooldown;

    public void Fire(Vector2 direction)
    {
        var bullet = (GameObject)Object.Instantiate(this.Bullet, this.transform.position, Quaternion.identity);
        bullet.rigidbody2D.velocity = direction * this.BulletSpeed;
    }
}
