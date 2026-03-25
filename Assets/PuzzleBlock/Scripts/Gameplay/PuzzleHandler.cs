using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PuzzleBlock.Gameplay
{
    public class PuzzleHandler : MonoBehaviour
    {
        public event Action<int, int> OnScoreAndMovementsUpdated;

        public event Action OnGameFinished;

        [SerializeField]
        private List<GameBrick> bricks;

        [SerializeField]
        private List<Sprite> brickImages;

        private GameBrick[,] gameBricksMatrix;

        private int score;
        private int movements;

        private const int ADDED_SCORE_PER_BLOCK = 10;
        private const int GRID_ROWS = 6;
        private const int GRID_COLUMNS = 5;

        public void Setup(int startsMovements)
        {
            score = 0;
            movements = startsMovements;

            InitializeGrid();
            OnScoreAndMovementsUpdated?.Invoke(score, movements);
        }

        public void MakeMovement()
        {
            score += ADDED_SCORE_PER_BLOCK;
            UpdateMovements();

            OnScoreAndMovementsUpdated?.Invoke(score, movements);
        }

        private void InitializeGrid()
        {
            gameBricksMatrix = new GameBrick[GRID_ROWS, GRID_COLUMNS];

            int index = 0;
            for (int row = 0; row < gameBricksMatrix.GetLength(0); row++)
            {
                for (int column = 0; column < gameBricksMatrix.GetLength(1); column++)
                {
                    int brickId = Random.Range(0, brickImages.Count);

                    bricks[index].OnBrickTouched += BrickTouched;
                    bricks[index].SetMatrixPosition(row, column);
                    bricks[index].SetBrickType(brickId, brickImages[brickId]);
                    gameBricksMatrix[row, column] = bricks[index];

                    index++;
                }
            }
        }

        private void BrickTouched(GameBrick touchedBrick)
        {
            bool[,] visited = new bool[gameBricksMatrix.GetLength(0), gameBricksMatrix.GetLength(1)];
            List<GameBrick> adjacentBricks = new();

            SearchAdjacentBricks(touchedBrick.Row, touchedBrick.Column, touchedBrick, ref visited, ref adjacentBricks);

            if (adjacentBricks.Count > 1)
            {
                foreach (GameBrick aBrick in adjacentBricks)
                {
                    aBrick.Clear();
                    score++;
                }
                Invoke(nameof(ApplyGravity), 1);
            }
            UpdateMovements();
        }

        private void SearchAdjacentBricks(int row, int col, GameBrick brick, ref bool[,] visited, ref List<GameBrick> adjacentBricks)
        {
            if (row < 0 || row >= gameBricksMatrix.GetLength(0) || col < 0 || col >= gameBricksMatrix.GetLength(1))
                return;

            if (visited[row, col])
                return;

            if (gameBricksMatrix[row, col].ID != brick.ID)
                return;

            adjacentBricks.Add(gameBricksMatrix[row, col]);
            visited[row, col] = true;

            SearchAdjacentBricks(row - 1, col, brick, ref visited, ref adjacentBricks);
            SearchAdjacentBricks(row + 1, col, brick, ref visited, ref adjacentBricks);
            SearchAdjacentBricks(row, col - 1, brick, ref visited, ref adjacentBricks);
            SearchAdjacentBricks(row, col + 1, brick, ref visited, ref adjacentBricks);
        }

        private void ApplyGravity()
        {
            for (int col = 0; col < gameBricksMatrix.GetLength(1); col++)
            {
                int writeRow = gameBricksMatrix.GetLength(0) - 1;

                for (int fila = gameBricksMatrix.GetLength(0) - 1; fila >= 0; fila--)
                {
                    if (gameBricksMatrix[fila, col].IsUsed)
                    {
                        int oldId = gameBricksMatrix[fila, col].ID;
                        gameBricksMatrix[writeRow, col].SetBrickType(oldId, brickImages[oldId]);

                        if (writeRow != fila)
                            gameBricksMatrix[fila, col].Clear();

                        writeRow--;
                    }
                }

                for (int fila = writeRow; fila >= 0; fila--)
                {
                    int brickId = Random.Range(0, brickImages.Count);
                    gameBricksMatrix[fila, col].SetBrickType(brickId, brickImages[brickId]);
                }
            }
        }

        private void UpdateMovements()
        {
            movements--;

            OnScoreAndMovementsUpdated(score, movements);
            if (movements <= 0)
            {
                movements = 0;
                OnGameFinished?.Invoke();
            }
        }
    }
}