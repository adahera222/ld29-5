using System.Collections;
using Annotations;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public Transform Player;
    public GameManager Manager;

    public void Detonate()
    {
        this.GetComponent<Animator>().SetTrigger("Exploding");
        this.transform.parent = null;
        this.Manager.IsStopped = true;
        this.audio.Play();
    }

    // Called by animation event
    [UsedImplicitly] private void DetonationFinished()
    {
        this.Manager.IsStopped = false;
        GameObject.Destroy(this.gameObject);
    }

    [UsedImplicitly] private void OnTriggerEnter2D(Collider2D other)
    {
        var enemyScript = other.GetComponent<Enemy>();
        if (enemyScript) {
            enemyScript.Destroy(other.transform.position - this.transform.position);
        }
        else {
            Object.Destroy(other.gameObject);
        }
    }
}
