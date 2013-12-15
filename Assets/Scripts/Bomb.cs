using System.Collections;
using Annotations;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameManager Manager;

    public void Detonate()
    {
        this.GetComponent<Animator>().SetTrigger("Exploding");
        this.transform.parent = null;
        this.Manager.IsStopped = true;
    }

    // Called by animation event
    [UsedImplicitly] private void DetonationFinished()
    {
        this.Manager.IncrementLevel();
    }

    [UsedImplicitly] private void OnTriggerEnter2D(Collider2D other)
    {
        var enemyScript = other.GetComponent<Enemy>();
        enemyScript.Destroy(other.transform.position - this.transform.position);
    }
}
