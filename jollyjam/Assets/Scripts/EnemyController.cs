using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    public MovableEnemyController movableEnemyController;
    public MainConfigScriptableObj mainConfigScriptableObj;
    
    private Sprite newSprite;
    private int rand;
    private bool _isChecked;

    private void Start()
    {
        var skinID = LevelGeneratorController.GetInstance().level.GetComponent<LevelsScriptData>().skinID;
        newSprite = mainConfigScriptableObj.skins[skinID].box;
    }

    void Update()
    {
        if (AppStateData.IsPause == false)
        {
            if (movableEnemyController.IsMoving)
                return;

            MoveBot();
        }
    }

    private void SetPositionWithCheck(Vector3 dir)
    {
        if (movableEnemyController.IsMoving == false)
        {
            movableEnemyController.SetMoveBallDirection(dir);
        }
    }

    private void MoveBot()
    {
        rand = Random.Range(1, 5);

        if (rand==1)
        {
            SetPositionWithCheck(Vector3.right);
        }
        else if (rand==2)
        {
            SetPositionWithCheck(Vector3.left);
        }
        else if (rand==3)
        {
            SetPositionWithCheck(Vector3.up);
        }
        else if (rand==4)
        {
            SetPositionWithCheck(Vector3.down);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {    
        Vector3Int tileCoordForRemove=new Vector3Int((int) other.transform.position.x-1, (int) other.transform.position.y-1, 0);
        var checker = other.GetComponent<Checker>();
        if (other.CompareTag("box"))
        {
            if (checker.isChecked)
            {
                BallPhysics.countOfBox++;
                //other.GetComponent<SpriteRenderer>().sprite = newSprite;
                TileMapSpriteUpdater.Instance.RemoveTile(tileCoordForRemove);
                checker.isChecked = false;
            }
        }
    }
}
