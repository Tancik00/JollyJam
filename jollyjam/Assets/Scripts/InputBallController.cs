using UnityEngine;

public class InputBallController : MonoBehaviour
{
    public MovableBallController movableBallController;
    public SwipeInputController swipeInputController;
    void Update()
    {
        if (AppStateData.IsPause == false)
        {
            if (movableBallController.IsMoving)
                return;

            if (swipeInputController.isSwipeRight)
            {
                SetPositionWithCheck(Vector3.right);
            }
            else if (swipeInputController.isSwipeLeft)
            {
                SetPositionWithCheck(Vector3.left);
            }
            else if (swipeInputController.isSwipeUp)
            {
                SetPositionWithCheck(Vector3.up);
            }
            else if (swipeInputController.isSwipeDown)
            {
                SetPositionWithCheck(Vector3.down);
            }
        }
    }

    private void SetPositionWithCheck(Vector3 dir)
    {
        if (movableBallController.IsMoving == false)
        {
            movableBallController.SetMoveBallDirection(dir);
        }
    }
}
