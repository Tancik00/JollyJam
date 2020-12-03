using DG.Tweening;
using UnityEngine;

public class ScalableBallView : MonoBehaviour
{
    public MovableBallController movableBallController;
    public PlayerConfigScriptableObject playerConfig;
    
    private Tweener _tweener;
    void Start()
    {
        movableBallController.OnStartMove += OnStartMove;
        movableBallController.OnFinishMove += OnFinishMove;
    }

    private void OnFinishMove(Vector3 dir)
    {
        _tweener?.Kill();
        if (Mathf.Abs(dir.x) > float.Epsilon)
        {
            _tweener = transform.DOScale(Vector3.one, playerConfig.animationDuration).OnComplete(() =>
            {
                _tweener = transform.DOScale(new Vector3(playerConfig.decScale, 1f), playerConfig.animationDuration).OnComplete(() =>
                {
                    _tweener = transform.DOScale(Vector3.one, playerConfig.animationDuration);
                });
            });
        }
        else
        {

            _tweener = transform.DOScale(Vector3.one, playerConfig.animationDuration).OnComplete(() =>
            {
                _tweener = transform.DOScale(new Vector3(1f, playerConfig.decScale), playerConfig.animationDuration).OnComplete(() =>
                {
                    _tweener = transform.DOScale(Vector3.one, playerConfig.animationDuration);
                });
            });
        }
    }

    private void OnStartMove(Vector3 dir)
    {
        _tweener?.Kill();

        if (Mathf.Abs(dir.x) > float.Epsilon)
        {
            _tweener = transform.DOScale(new Vector3(playerConfig.incScale, 1f), playerConfig.animationDuration);
        }
        else
        {
            _tweener = transform.DOScale(new Vector3(1f, playerConfig.incScale), playerConfig.animationDuration);
        }

    }
}
