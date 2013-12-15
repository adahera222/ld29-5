using Annotations;
using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
    public GameManager Manager;
    public tk2dTextMesh PortalCounter;

    [UsedImplicitly] private void LateUpdate()
    {
        this.PortalCounter.text =
            "LEVEL: " + this.Manager.Level +
            " | PORTALS: " + (this.Manager.SpawnAmount - this.Manager.CurrentAmount);
    }
}
