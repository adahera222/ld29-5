using Annotations;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player player;
    public Transform[] Children;

    [UsedImplicitly] private void Start()
    {
        this.player = Object.FindObjectOfType<Player>();
    }

    [UsedImplicitly] private void Update()
    {
        var target = Vector3.Lerp(this.transform.position, this.player.transform.position, Time.deltaTime);
        this.transform.position = target;
    }

    [UsedImplicitly] private void OnCollisionEnter2D()
    {
        this.Destroy();
    }

    [UsedImplicitly] private void OnTriggerEnter2D()
    {
        this.Destroy();
    }

    private void Destroy()
    {
        foreach (var child in this.Children) {
            child.parent = null;
            child.gameObject.SetActive(true);
        }

        Object.Destroy(this.gameObject);
    }
}
