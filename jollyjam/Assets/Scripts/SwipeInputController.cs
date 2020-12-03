using UnityEngine;

public class SwipeInputController : MonoBehaviour
{
    private Vector3 _startTouch, _currentPos;
    private const float deltaSwipe = 7f;
    
    public bool isSwipeRight;
    public bool isSwipeLeft;
    public bool isSwipeUp;
    public bool isSwipeDown;
    
    private void Update()
    {
        isSwipeRight = isSwipeLeft = isSwipeUp = isSwipeDown = false;
        if (Input.GetMouseButtonDown(0))
        {
            _startTouch = Input.mousePosition;
        }
        
        if (Input.GetMouseButton(0))
        {
            _currentPos = Input.mousePosition;

            var dis = Vector3.Distance(_startTouch, _currentPos);

            if (dis < 10f)
                return;
            
            if ((Mathf.Abs(_startTouch.x - _currentPos.x)) > (Mathf.Abs(_startTouch.y - _currentPos.y)))
            {
                if (_startTouch.x - _currentPos.x < -deltaSwipe)
                {
                    _startTouch = Input.mousePosition;
                    isSwipeRight = true;
                }
                else if (_startTouch.x - _currentPos.x > deltaSwipe)
                { 
                    _startTouch = Input.mousePosition;
                    isSwipeLeft = true;
                }
            }

            else
            {
                if (_startTouch.y - _currentPos.y < -deltaSwipe)
                {
                    _startTouch = Input.mousePosition;
                    isSwipeUp = true;

                }
                else if (_startTouch.y - _currentPos.y > deltaSwipe)
                {
                    _startTouch = Input.mousePosition;
                    isSwipeDown = true;
                }
            }
        }
    }
}
