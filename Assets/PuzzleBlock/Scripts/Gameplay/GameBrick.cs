using System;
using UnityEngine;
using UnityEngine.UI;

namespace PuzzleBlock.Gameplay
{
    /// <summary>
    /// Represents a single interactive brick in the game grid, managing its state, appearance, and user interactions.
    /// </summary>
    /// <remarks>Requires an Image and Button component on the same GameObject. Raises the OnBrickTouched
    /// event when the brick is clicked.</remarks>
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    public class GameBrick : MonoBehaviour
    {
        /// <summary>
        /// Occurs when a brick is touched.
        /// </summary>
        public event Action<GameBrick> OnBrickTouched;

        /// <summary>
        /// Gets the brick ID, which is equal to image that represents brick type.
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// Gets the row index associated with the object in the grid.
        /// </summary>
        public int Row { get; private set; }

        /// <summary>
        /// Gets the column index associated with the object in the grid.
        /// </summary>
        public int Column { get; private set; }

        /// <summary>
        /// Indicates whether the object has a brick image and id assigned.
        /// </summary>
        public bool IsUsed { get; private set; }

        private Image image;

        private void Awake()
        {
            image = GetComponent<Image>();

            GetComponent<Button>().onClick.AddListener(BrickTouched);
        }

        /// <summary>
        /// Sets the position in the gameplay grid using the specified row and column indices.
        /// </summary>
        /// <param name="row">The zero-based row index.</param>
        /// <param name="column">The zero-based column index.</param>
        public void SetPositionInMatrix(int row, int column)
        {
            Row = row;
            Column = column;
        }

        /// <summary>
        /// Disables the image and marks the brick as unused.
        /// </summary>
        public void Clear()
        {
            image.enabled = false;
            IsUsed = false;
        }

        /// <summary>
        /// Sets the brick's identifier and updates its brick image type.
        /// </summary>
        /// <param name="id">The unique identifier to assign to the brick.</param>
        /// <param name="brickSprite">The sprite to display for the brick.</param>
        public void SetBrickType(int id, Sprite brickSprite)
        {
            ID = id;
            image.sprite = brickSprite;
            image.enabled = true;

            IsUsed = true;
        }

        private void BrickTouched()
        {
            OnBrickTouched?.Invoke(this);
        }
    }
}