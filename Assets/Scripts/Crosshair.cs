using Annotations;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [UsedImplicitly] private void Start()
    {
        Screen.showCursor = false;
    }

    [UsedImplicitly] private void Update()
    {
        var z = this.transform.position.z;
        var p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        p.z = z;
        this.transform.position = p;
    }
}
