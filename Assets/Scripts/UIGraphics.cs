using UnityEngine;
using TMPro;
using CannonApp;

public class UIGraphics : MonoBehaviour
{
    private static readonly int LevelEndedHash = Animator.StringToHash("LevelEnded");
    private static readonly int GameOverHash = Animator.StringToHash("GameOver");

    [SerializeField] private Animator animator;
    [SerializeField] private TMP_Text remainingTargetsText;
    [SerializeField] private TMP_Text levelFinishedText;

    public void EndGame()
    {
        animator.SetTrigger(GameOverHash);
    }

    public void EndLevel(int currentLevel)
    {
        levelFinishedText.text = $"Level {currentLevel} Finished!";
        animator.SetTrigger(LevelEndedHash);
    }

    public void UpdateRemainingTargets(int remainingTargets)
    {
        remainingTargetsText.text = $"Remaining Targets: {remainingTargets}!";
    }

    public void OnRetryClicked()
    {
       ProgressionController.RetryGame();
    }
    public void OnFinishedEndLevelAnimation()
    {
        ProgressionController.NextLevel();
    }
}
