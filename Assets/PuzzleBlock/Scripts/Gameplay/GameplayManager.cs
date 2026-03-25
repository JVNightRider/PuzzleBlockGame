using PuzzleBlock.Settings;
using PuzzleBlock.UI;
using UnityEngine;

namespace PuzzleBlock.Gameplay
{
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
            uiController.OnMakeMovementButtonPressed += puzzleHandler.MakeMovement;

            puzzleHandler.OnScoreAndMovementsUpdated += uiController.UpdateScoreAndMovements;
            puzzleHandler.OnGameFinished += uiController.ShowGameOver;

            puzzleHandler.Setup(puzzleBlockSettings.PlayerMovements);
        }
    }
}