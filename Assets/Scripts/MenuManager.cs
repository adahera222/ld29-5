using Annotations;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public int GameScene;

    [UsedImplicitly] private void Update()
    {
        if (Input.GetKeyDown("return")) {
            Application.LoadLevel(this.GameScene);
        }
    }
}
