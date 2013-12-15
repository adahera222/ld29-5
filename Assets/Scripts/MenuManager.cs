using Annotations;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public int GameScene;

    [UsedImplicitly] private void Update()
    {
        if (Input.anyKeyDown) {
            Application.LoadLevel(this.GameScene);
        }
    }
}
