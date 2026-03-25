using System;
using UnityEngine;
using UnityEngine.UI;

namespace PuzzleBlock.Gameplay
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]
    public class GameBrick : MonoBehaviour
    {
        public event Action<GameBrick> OnBrickTouched;

        public int ID { get; private set; }
        public int Row { get; private set; }
        public int Column { get; private set; }

        public bool IsUsed { get; private set; }

        private Image image;

        private Button button;

        private void Awake()
        {
            image = GetComponent<Image>();
            button = GetComponent<Button>();

            button.onClick.AddListener(BrickTouched);
        }

        public void SetMatrixPosition(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public void SetBrickType(int id, Sprite brickSprite)
        {
            ID = id;

            image.sprite = brickSprite;
            button.interactable = true;

            IsUsed = true;
        }

        private void BrickTouched()
        {
            OnBrickTouched?.Invoke(this);
        }

        public void Clear()
        {
            image.sprite = null;
            button.interactable = false;

            IsUsed = false;
        }
    }
}