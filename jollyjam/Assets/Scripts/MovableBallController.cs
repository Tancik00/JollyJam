using System;
using System.Collections;
using UnityEngine;
using Spine.Unity;
using Random = UnityEngine.Random;

public class MovableBallController : MonoBehaviour
{
    #region Public variables

    [HideInInspector] public float distance;
    public float distance1;
    public PlayerConfigScriptableObject playerConfig;
    public CharacterAnimationCtrl characterAnimationControllerCtrl;
    [HideInInspector] public bool IsMoving;
    public LayerMask LayerMask;
    public RaycastHit2D ray;
    public SkeletonAnimation anim;
    public Action<Vector3> OnStartMove;
    public Action<Vector3> OnFinishMove;
    [SpineAnimation(dataField: "anim")] public string AnimationIdle;
    [SpineAnimation(dataField: "anim")] public string[] AnimationName;
    public static int count;
    public LineRenderer linePref;

    public Transform parent;
    //public LineRenderer lineRenderer;

    #endregion

    private int k = 0;
    private int counter = 0;
    private Vector3 ballPos;
    private LineRenderer lr;
    private Vector3 _dir;

    private void Start()
    {
        //ballPos = transform.position;
        //lineRenderer.SetPosition(0, new Vector3(ballPos.x, ballPos.y, 0f));
        lr = Instantiate(linePref, parent);
        lr.SetPosition(0, (Vector2) transform.position);
        counter = 1;
    }

    public void SetMoveBallDirection(Vector3 dir)
    {
        if (IsMoving == false)
        {

            //check if it is already 0steps from wall
            ray = Physics2D.Raycast(transform.position, dir, 100f, LayerMask);
            distance1 = Vector2.Distance(transform.position, ray.transform.position - dir);

            if (distance1 < 0.4f)
            {
                return;
            }

            IsMoving = true;
            _dir = dir;
            OnStartMove?.Invoke(dir);
            characterAnimationControllerCtrl.ChangeAnimation();
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

        Vector3 newPos = transform.position + Time.fixedDeltaTime * playerConfig.speedMod * _dir;
        float stepsize = Vector2.Distance(transform.position, newPos);

        if (distance < 0.1f || stepsize > distance)
        {
            k++;
            transform.position = ray.transform.position - _dir;
            IsMoving = false;
            count++;

            //lineRenderer.SetPosition(lineRenderer.positionCount++, new Vector3(transform.position.x, transform.position.y, 0f));

            DrawLine();

            if (!LevelsScriptData.GetInstance().isTimeLimited && SwipeCountPanel.swipeCount > 0)
            {
                SwipeCountPanel.swipeCount--;
            }

            StartCoroutine(PlayAnimation());
            OnFinishMove?.Invoke(_dir);
            //Debug.Log("***countOfSwipe "+ k);
            return;
        }

        transform.position += Time.fixedDeltaTime * playerConfig.speedMod * _dir;

    }

    private void DrawLine()
    {
        switch (++counter)
        {
            case 1:
                lr.SetPosition(lr.positionCount++, (Vector2) transform.position);
                break;

            case 2:
                lr.SetPosition(lr.positionCount++, (Vector2) transform.position);
                break;

            case 3:
                counter = 2;
                var lastPosition = lr.GetPosition(1);
                lr = Instantiate(linePref, parent);
                lr.positionCount = 2;
                lr.SetPosition(0, lastPosition);
                lr.SetPosition(1, (Vector2) transform.position);
                break;
        }
    }

    private IEnumerator PlayAnimation()
    {
        if (k % Random.Range(3, 5) < 0.1f)
        {
            anim.AnimationName = AnimationName[Random.Range(0, AnimationName.Length)];
            yield return new WaitForSeconds(1.5f);
        }

        anim.AnimationName = AnimationIdle;
    }
}
