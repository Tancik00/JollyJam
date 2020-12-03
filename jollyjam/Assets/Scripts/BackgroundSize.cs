using UnityEngine;

public class BackgroundSize : MonoBehaviour
{
    public MainConfigScriptableObj mainConfigScriptableObj;
    void Start()
    {
        transform.localScale =
            new Vector3(Camera.main.orthographicSize * mainConfigScriptableObj.backgroundSizeCoeficient, Camera.main.orthographicSize * 0.11f, 1);
    }
}
