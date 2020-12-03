using UnityEngine;
using CloudOnce;

public class LeaderboardHandler : MonoBehaviour
{
    public void SubmitScore()
    {
        Leaderboards.MyLeaderboard.SubmitScore(LevelGeneratorController.TotalStarCount);
#if UNITY_EDITOR
                Debug.Log("SubmitScore");
#endif
    }
}
