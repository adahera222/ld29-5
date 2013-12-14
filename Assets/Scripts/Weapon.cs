using Annotations;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject Bullet;
    public float BulletSpeed;
    public float Cooldown;
    private float lastFired;
    private Transform bulletParent;

    [UsedImplicitly] private void Start()
    {
        this.bulletParent = GameObject.Find("Bullets").transform;
    }

    public void Fire(Vector2 direction)
    {
        if (this.lastFired > this.Cooldown) {
            var bullet = (GameObject)Object.Instantiate(this.Bullet, this.transform.position, Quaternion.identity);
            bullet.transform.parent = this.bulletParent;
            bullet.rigidbody2D.velocity = direction * this.BulletSpeed;
            this.lastFired = 0;
        }

        this.lastFired += Time.deltaTime;
    }
}
