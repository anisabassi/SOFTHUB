using System;
using UnityEngine;

namespace SoftHub.AceOfShadows
{
    /// <summary>
    /// Represents a card in the game, handling its visual representation and movement.
    /// </summary>
    public class Card : MonoBehaviour
    {
        [SerializeField]private SpriteRenderer _spriteRenderer;
        [SerializeField]private Sprite _defaultSprite;
        [SerializeField]private Sprite _topCardSprite;
        [SerializeField] private int _defaultSortingOrder;
        [SerializeField] private int _topSortingOrder;
        [SerializeField] private Vector3 _offset = Vector3.zero;

        private Action onMoveComplete;

        /// <summary>
        /// Moves the card to a target position over a specified duration using easing.
        /// </summary>
        /// <param name="target">The target world position.</param>
        /// <param name="duration">Duration of the movement.</param>
        /// <param name="onComplete">Callback to invoke when movement finishes.</param>
        public void MoveTo(Vector3 target, float duration, Action onComplete)
        {
            transform.SetParent(null); // Detach from parent before world movement
            onMoveComplete = onComplete;

            iTween.MoveTo(gameObject, iTween.Hash(
                "position", target,
                "time", duration,
                "easetype", iTween.EaseType.easeInOutCubic,
                "oncomplete", "OnMoveComplete",
                "oncompletetarget", gameObject
            ));
        }

        /// <summary>
        /// Called by iTween when movement is completed.
        /// Resets the sprite and sorting order to default.
        /// </summary>
        private void OnMoveComplete()
        {
            if (_spriteRenderer && _defaultSprite)
                _spriteRenderer.sprite = _defaultSprite;

            _spriteRenderer.sortingOrder = _defaultSortingOrder;

            onMoveComplete?.Invoke();
        }

        /// <summary>
        /// Sets the card’s visual and position based on its index in a stack.
        /// </summary>
        /// <param name="index">Index of the card in the stack (0 = top).</param>
        public void SetVisualAndOffset(int index)
        {
            gameObject.SetActive(true);

            float x = (_offset.x * index);
            float y = (_offset.y * index);
            float z = (_offset.y * index);

            transform.localPosition = new Vector3(x, y, z);

            _spriteRenderer.sprite = (index == 0) ? _topCardSprite : _defaultSprite;
            _spriteRenderer.sortingOrder = (index == 0) ? _topSortingOrder : _defaultSortingOrder;
        }

        /// <summary>
        /// Hides the card by disabling its GameObject.
        /// </summary>
        public void Hide()
        {
            gameObject.SetActive(false); 
        }
    }
}
