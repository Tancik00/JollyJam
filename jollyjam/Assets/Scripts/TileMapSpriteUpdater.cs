using Config;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapSpriteUpdater : MonoBehaviour
{

    public static TileMapSpriteUpdater Instance;

    public Tilemap jamTilemap;

    public MainConfigScriptableObj mainConfigScriptableObj;

    private SkinScriptableObject skinScriptableObject;
    
    /*public JamSprites[] jamSpriteses;
    
    [System.Serializable]
    public struct JamSprites
    {
        public SpriteDirection spriteDirection;
        public Sprite jamSprite;
    }
    
    public enum SpriteDirection
    {
        center,
        horizontal,
        vertical,
        topEnd,
        bottomEnd,
        leftEnd,
        rightEnd,
        cornerTopLeft,
        cornerTopRight,
        cornerBottomLeft,
        cornerBottomRight,
        tTop,
        tBottom,
        tLeft,
        tRight,
        cross
    }*/
    
    private bool hasTop;
    private bool hasBottom;
    private bool hasLeft;
    private bool hasRight;
    
    private void Awake()
    {
        Instance = this;
        var skinID = LevelGeneratorController.GetInstance().level.GetComponent<LevelsScriptData>().skinID;
        skinScriptableObject = mainConfigScriptableObj.skins[0];
    }

    public void UpdateTile(Vector3Int tileCoord)
    {
        tileCoord = new Vector3Int(tileCoord.x-1,tileCoord.y-1,0);
        
        UpdateSelectedTile(tileCoord, true);
        
        UpdateSelectedTile(tileCoord + new Vector3Int(1,0,0));
        UpdateSelectedTile(tileCoord + new Vector3Int(-1,0,0));
        UpdateSelectedTile(tileCoord + new Vector3Int(0,1,0));
        UpdateSelectedTile(tileCoord + new Vector3Int(0,-1,0));
    }

    private void UpdateSelectedTile(Vector3Int tileCoord, bool isTriggered = false)
    {
        hasTop = IsTileHasNear(tileCoord + new Vector3Int(0,1,0));
        hasBottom = IsTileHasNear(tileCoord + new Vector3Int(0,-1,0));
        hasLeft = IsTileHasNear(tileCoord + new Vector3Int(-1,0,0));
        hasRight = IsTileHasNear(tileCoord + new Vector3Int(1,0,0));

        if (isTriggered){
            Tile tile = new Tile();
            tile.sprite = GetCorrectSprite();
            jamTilemap.SetTile(tileCoord,tile);
        }else{
            if (jamTilemap.GetTile(tileCoord) != null){
                if (!string.IsNullOrEmpty(jamTilemap.GetSprite(tileCoord).name)){
                    Tile tile = new Tile();
                    tile.sprite = GetCorrectSprite();
                    jamTilemap.SetTile(tileCoord,tile);
                }
            }
        }
    }

    private Sprite GetCorrectSprite()
    {
        Sprite sprite = null;

        sprite = CheckAdvance();
        if (sprite == null){
            sprite = CheckSimple();
        }

        if (sprite == null){
            sprite = GetCorrectSpriteFromEnum(SkinScriptableObject.SpriteDirection.center);
        }

        return sprite;
    }

    private Sprite CheckAdvance()
    {
        Sprite sprite = null;

        if ((!hasTop && hasBottom) && (!hasLeft && !hasRight)){
            sprite = GetCorrectSpriteFromEnum(SkinScriptableObject.SpriteDirection.topEnd);}
        else if ((hasTop && !hasBottom) && (!hasLeft && !hasRight)){
            sprite = GetCorrectSpriteFromEnum(SkinScriptableObject.SpriteDirection.bottomEnd);}
        else if ((!hasTop && !hasBottom) && (!hasLeft && hasRight)){
            sprite = GetCorrectSpriteFromEnum(SkinScriptableObject.SpriteDirection.leftEnd);}
        else if ((!hasTop && !hasBottom) && (hasLeft && !hasRight)){
            sprite = GetCorrectSpriteFromEnum(SkinScriptableObject.SpriteDirection.rightEnd);
        }else if ((!hasTop && hasBottom) && (hasLeft && !hasRight)){
            sprite = GetCorrectSpriteFromEnum(SkinScriptableObject.SpriteDirection.cornerTopLeft);
        }else if ((!hasTop && hasBottom) && (!hasLeft && hasRight)){
            sprite = GetCorrectSpriteFromEnum(SkinScriptableObject.SpriteDirection.cornerTopRight);
        }else if ((hasTop && !hasBottom) && (hasLeft && !hasRight)){
            sprite = GetCorrectSpriteFromEnum(SkinScriptableObject.SpriteDirection.cornerBottomLeft);
        }else if ((hasTop && !hasBottom) && (!hasLeft && hasRight)){
            sprite = GetCorrectSpriteFromEnum(SkinScriptableObject.SpriteDirection.cornerBottomRight);
        }else if ((hasTop && !hasBottom) && (hasLeft && hasRight)){
            sprite = GetCorrectSpriteFromEnum(SkinScriptableObject.SpriteDirection.tTop);
        }else if ((!hasTop && hasBottom) && (hasLeft && hasRight)){
            sprite = GetCorrectSpriteFromEnum(SkinScriptableObject.SpriteDirection.tBottom);
        }else if ((hasTop && hasBottom) && (hasLeft && !hasRight)){
            sprite = GetCorrectSpriteFromEnum(SkinScriptableObject.SpriteDirection.tLeft);
        }else if ((hasTop && hasBottom) && (!hasLeft && hasRight)){
            sprite = GetCorrectSpriteFromEnum(SkinScriptableObject.SpriteDirection.tRight);
        }

        return sprite;
    }

    private Sprite CheckSimple()
    {
        Sprite sprite = null;
        
        if ((!hasTop && !hasBottom) && (!hasLeft && !hasRight)){
            sprite = GetCorrectSpriteFromEnum(SkinScriptableObject.SpriteDirection.center);
        }else if ((hasTop || hasBottom) && (!hasLeft && !hasRight)){
            sprite = GetCorrectSpriteFromEnum(SkinScriptableObject.SpriteDirection.vertical);
        }else if ((!hasTop && !hasBottom) && (hasLeft || hasRight)){
            sprite = GetCorrectSpriteFromEnum(SkinScriptableObject.SpriteDirection.horizontal);
        }else if ((hasTop && hasBottom) && (hasLeft && hasRight)){
            sprite = GetCorrectSpriteFromEnum(SkinScriptableObject.SpriteDirection.cross);
        }

        return sprite;
    }

    private bool IsTileHasNear(Vector3Int tileCoord)
    {
        return jamTilemap.GetSprite(tileCoord) != null;
    }

    private Sprite GetCorrectSpriteFromEnum(SkinScriptableObject.SpriteDirection direction)
    {
        for (int i = 0; i < skinScriptableObject.jamSpriteses.Length; i++)
        {
            if (skinScriptableObject.jamSpriteses[i].spriteDirection == direction)
            {
                return skinScriptableObject.jamSpriteses[i].jamSprite;
            }
        }

        return null;
    }

    public void RemoveTile(Vector3Int tileCoord)
    {
        jamTilemap.SetTile(tileCoord, null);
    }
}
