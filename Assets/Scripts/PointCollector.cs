using Annotations;
using UnityEngine;

public class PointCollector : MonoBehaviour
{
    public GameManager Manager;

    [UsedImplicitly] private void OnTriggerEnter2D(Collider2D other)
    {
        this.Manager.Points += 1;
        Object.Destroy(other.gameObject);
    }
}
