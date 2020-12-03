using System;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "SkinScriptableObject", menuName = "SkinScriptableObject", order = 0)]
    public class SkinScriptableObject : ScriptableObject
    {
        public Sprite obstacle;
        public Sprite box;
        public Sprite changingBox;
        public Sprite bot;
        public Sprite filledBox;
        public Sprite changedBox;
        public Sprite background;

        public JamSprites[] jamSpriteses;

        public GameObject[] decor;
    
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
        }
    }
}