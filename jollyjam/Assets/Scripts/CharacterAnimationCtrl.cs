using Spine.Unity;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterAnimationCtrl : MonoBehaviour
{
    [SpineAnimation(dataField: "anim")]
    public string[] AnimationName;
    public SkeletonAnimation anim;

    /*private void Start()
    {
        anim.AnimationState.Complete += AnimationStateOnComplete;
    }

    private void OnDestroy()
    {
        anim.AnimationState.Complete -= AnimationStateOnComplete;
    }

    private void AnimationStateOnComplete(TrackEntry trackentry)
    {
    }*/

    public void ChangeAnimation()
    {
        anim.AnimationName = AnimationName[Random.Range(0,AnimationName.Length)];
    }
}
