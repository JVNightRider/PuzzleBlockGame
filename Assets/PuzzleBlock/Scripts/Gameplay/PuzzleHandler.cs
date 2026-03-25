using System;
using UnityEngine;

namespace PuzzleBlock.Gameplay
{
    public class PuzzleHandler : MonoBehaviour
    {
        public event Action<int, int> OnScoreAndMovementsUpdated;

        public event Action OnGameFinished;

        private const int ADDED_SCORE_PER_BLOCK = 10;

        private int score;
        private int movements;

        public void Setup(int startsMovements)
        {
            score = 0;
            movements = startsMovements;

            OnScoreAndMovementsUpdated?.Invoke(score, movements);
        }

        public void MakeMovement()
        {
            score += ADDED_SCORE_PER_BLOCK;
            UpdateMovements();

            OnScoreAndMovementsUpdated?.Invoke(score, movements);
        }

        private void UpdateMovements()
        {
            movements--;
            if (movements <= 0)
            {
                movements = 0;
                OnGameFinished?.Invoke();
            }
        }
    }
}