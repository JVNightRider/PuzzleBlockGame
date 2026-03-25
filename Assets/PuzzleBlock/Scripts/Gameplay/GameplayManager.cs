using PuzzleBlock.Settings;
using PuzzleBlock.UI;
using UnityEngine;

namespace PuzzleBlock.Gameplay
{
    /// <summary>
    /// Manages core gameplay logic, including puzzle setup, event binding, and UI updates.
    /// </summary>
    /// <remarks>Coordinates interactions between puzzle handling and user interface components during
    /// gameplay.</remarks>
    public class GameplayManager : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField]
        private PuzzleBlockSettings puzzleBlockSettings;

        [Header("Handlers")]
        [SerializeField]
        private UIController uiController;

        [SerializeField]
        private PuzzleHandler puzzleHandler;

        private void Start()
        {
            BindEvents();

            puzzleHandler.Setup(puzzleBlockSettings.PlayerMovements, puzzleBlockSettings.DelayAfterMovement);
        }

        private void BindEvents()
        {
            puzzleHandler.OnScoreAndMovementsUpdated += UpdateScoreAndMovements;
            puzzleHandler.OnGameFinished += uiController.ShowGameOver;
        }

        private void UpdateScoreAndMovements(int score, int movements)
        {
            uiController.UpdateScore(score);
            uiController.UpdateMovements(movements);
        }
    }
}