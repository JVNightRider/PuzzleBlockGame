using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using Random = UnityEngine.Random;

namespace PuzzleBlock.Gameplay
{
    /// <summary>
    /// Manages puzzle gameplay logic, including grid initialization, brick interactions, score and movement tracking,
    /// and game completion events.
    /// </summary>
    /// <remarks>Attach to a Unity GameObject to handle user interactions, update the puzzle grid, and notify
    /// listeners of score, movement, and game completion changes.</remarks>
    public class PuzzleHandler : MonoBehaviour
    {
        /// <summary>
        /// Occurs when the score and number of movements are updated.
        /// </summary>
        public event Action<int, int> OnScoreAndMovementsUpdated;

        /// <summary>
        /// Occurs when the game has finished.
        /// </summary>
        public event Action OnGameFinished;

        [Header("References")]
        [SerializeField]
        [Tooltip("Current Event System which handles UI events")]
        private EventSystem eventSystem;

        [Header("Gameplay")]
        [SerializeField]
        [Tooltip("List of objects that represent bricks involved in gameplay")]
        private List<GameBrick> bricks;

        [SerializeField]
        [Tooltip("List of available images that will represent brick types in gameplay")]
        private List<Sprite> brickImages;

        private GameBrick[,] bricksMatrix;

        private int score;
        private int movements;

        private float delayTimeAfterMovement;

        private const int GRID_ROWS = 6;
        private const int GRID_COLUMNS = 5;

        /// <summary>
        /// Initializes the score, movements, and delay time, then prepares the grid and notifies listeners of the
        /// updated score and movements.
        /// </summary>
        /// <param name="startsMovements">The initial number of movements available.</param>
        /// <param name="delayTimeAfterMovement">The delay, in seconds, to apply after each movement.</param>
        public void Setup(int startsMovements, float delayTimeAfterMovement)
        {
            score = 0;
            movements = startsMovements;
            this.delayTimeAfterMovement = delayTimeAfterMovement;

            InitializeGrid();
            OnScoreAndMovementsUpdated?.Invoke(score, movements);
        }

        private void InitializeGrid()
        {
            bricksMatrix = new GameBrick[GRID_ROWS, GRID_COLUMNS];

            int index = 0;
            for (int row = 0; row < bricksMatrix.GetLength(0); row++)
            {
                for (int column = 0; column < bricksMatrix.GetLength(1); column++)
                {
                    int brickId = Random.Range(0, brickImages.Count);

                    bricks[index].OnBrickTouched += BrickTouched;
                    bricks[index].SetPositionInMatrix(row, column);
                    bricks[index].SetBrickType(brickId, brickImages[brickId]);

                    bricksMatrix[row, column] = bricks[index];
                    index++;
                }
            }
            bricks.Clear();
        }

        private void BrickTouched(GameBrick touchedBrick)
        {
            bool[,] visited = new bool[bricksMatrix.GetLength(0), bricksMatrix.GetLength(1)];
            List<GameBrick> cardinalBricks = new();

            SearchCardinalBricks(touchedBrick.Row, touchedBrick.Column, touchedBrick, ref visited, ref cardinalBricks);

            if (cardinalBricks.Count > 1)
            {
                eventSystem.enabled = false; //Disable event system to prevent bugs caused by more user movements before grid reconfiguration

                foreach (GameBrick aBrick in cardinalBricks)
                {
                    aBrick.Clear();
                    score++;
                }
                Invoke(nameof(ApplyGravityToBricks), delayTimeAfterMovement);
            }
            UpdateMovements();
        }

        private void SearchCardinalBricks(int row, int col, GameBrick brick, ref bool[,] visited, ref List<GameBrick> cardinalBricks)
        {
            if (row < 0 || row >= bricksMatrix.GetLength(0) || col < 0 || col >= bricksMatrix.GetLength(1))
                return;

            if (visited[row, col])
                return;

            if (bricksMatrix[row, col].ID != brick.ID)
                return;

            cardinalBricks.Add(bricksMatrix[row, col]);
            visited[row, col] = true;

            SearchCardinalBricks(row - 1, col, brick, ref visited, ref cardinalBricks);
            SearchCardinalBricks(row + 1, col, brick, ref visited, ref cardinalBricks);
            SearchCardinalBricks(row, col - 1, brick, ref visited, ref cardinalBricks);
            SearchCardinalBricks(row, col + 1, brick, ref visited, ref cardinalBricks);
        }

        private void ApplyGravityToBricks()
        {
            for (int col = 0; col < bricksMatrix.GetLength(1); col++)
            {
                int writeRow = bricksMatrix.GetLength(0) - 1;

                for (int newRow = bricksMatrix.GetLength(0) - 1; newRow >= 0; newRow--)
                {
                    if (bricksMatrix[newRow, col].IsUsed)
                    {
                        int prevId = bricksMatrix[newRow, col].ID;
                        bricksMatrix[writeRow, col].SetBrickType(prevId, brickImages[prevId]);

                        if (writeRow != newRow)
                            bricksMatrix[newRow, col].Clear();

                        writeRow--;
                    }
                }
                CreateNewBricks(writeRow, col);
            }
            eventSystem.enabled = true; //Enable event system after grid reconfiguration
        }

        private void CreateNewBricks(int writeRow, int col)
        {
            for (int newRow = writeRow; newRow >= 0; newRow--)
            {
                int brickId = Random.Range(0, brickImages.Count);
                bricksMatrix[newRow, col].SetBrickType(brickId, brickImages[brickId]);
            }
        }

        private void UpdateMovements()
        {
            movements--;
            OnScoreAndMovementsUpdated(score, movements);

            if (movements <= 0)
                Invoke(nameof(CallGameOverEvent), delayTimeAfterMovement);
        }

        private void CallGameOverEvent()
        {
            OnGameFinished?.Invoke();
        }
    }
}