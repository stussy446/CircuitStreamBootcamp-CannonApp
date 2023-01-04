using System;
using UnityEngine;

namespace CannonApp
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField]
        protected UIGraphics uiGraphics;

        public Action levelEnded;
        protected int remainingTargets;

        public void TargetDestroyed()
        {
            remainingTargets--;

            if (remainingTargets <= 0)
                EndLevel();

            uiGraphics.UpdateRemainingTargets(remainingTargets);
        }

        public virtual void RegisterTarget()
        {
            remainingTargets++;
            uiGraphics.UpdateRemainingTargets(remainingTargets);
        }

        public void OnFinishedEndLevelAnimation()
        {
            ProgressionController.NextLevel();
        }

        private void EndLevel()
        {
            levelEnded?.Invoke();

            if (ProgressionController.IsLastLevel())
            {
                uiGraphics.EndGame();
                return;
            }

            uiGraphics.EndLevel(ProgressionController.CurrentLevel);
        }

        protected virtual void Awake()
        {
            GameServices.RegisterService(this);

            ProgressionController.InitializeLevelCount();
            ProgressionController.SetCurrentLevel();
        }

        private void OnDestroy()
        {
            GameServices.DeregisterService(this);
        }
    }
}