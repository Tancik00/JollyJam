using UnityEngine;

public class MovableEnemyController : MonoBehaviour
{
   #region Public variables
    
    [HideInInspector] public float distance;
    [HideInInspector] public bool IsMoving;
    public LayerMask LayerMask;
    public RaycastHit2D ray;
    private float _speed = 7f;

    #endregion
    
    private Vector3 _dir;

    public void SetMoveBallDirection(Vector3 dir)
    {
        if (IsMoving == false)
        {
            
            //check if it is already 0steps from wall
            ray = Physics2D.Raycast(transform.position, dir, 100f, LayerMask);
            distance = Vector2.Distance(transform.position, ray.transform.position - dir);
            if (distance<0.4f)
            {
                return;
            }
            IsMoving = true;
            _dir = dir;
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogWarning("Set in wrong state");
#endif
            
        }
    }

    void FixedUpdate()
    {

        if (IsMoving == false)
            return;

        ray = Physics2D.Raycast(transform.position, _dir, 100f, LayerMask);
        distance = Vector2.Distance(transform.position, ray.transform.position - _dir);
        
        Vector3 newPos = transform.position + Time.fixedDeltaTime * _speed * _dir;
        float stepsize = Vector2.Distance(transform.position, newPos);;

        if (distance < 0.1f || stepsize > distance)
        {
            transform.position = ray.transform.position - _dir;
            IsMoving = false;
            return;
        }

        transform.position += Time.fixedDeltaTime * _speed * _dir;

    }
}
