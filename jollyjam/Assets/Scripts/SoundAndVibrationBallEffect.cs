using UnityEngine;

public class SoundAndVibrationBallEffect : MonoBehaviour
{
    public MovableBallController movableBallController;
    
    void Start()
    {
        movableBallController.OnStartMove += OnStartMove;
        movableBallController.OnFinishMove += OnFinishMove;
    }

    private void OnFinishMove(Vector3 obj)
    {
       // LevelGeneratorController.GetInstance().hitSound.Play();
        if (AppStateData.isVibrationOn)
        {
#if  UNITY_IOS
            Handheld.Vibrate();
#endif
        }
    }

    private void OnStartMove(Vector3 obj)
    {

    }
}
