using UnityEngine;

public class MainMenuBackgroundSize : MonoBehaviour
{
    private void Start()
    {
        transform.localScale =
            new Vector3(Camera.main.orthographicSize * 0.12f, Camera.main.orthographicSize * 0.12f, 1);
    }
}
