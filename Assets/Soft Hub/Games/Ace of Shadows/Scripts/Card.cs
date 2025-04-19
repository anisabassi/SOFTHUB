using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace SoftHub.AceOfShadows
{
    /// <summary>
    /// Represents a card in the game, handling its visual representation and movement.
    /// </summary>
    public class Card : MonoBehaviour
    {
        [SerializeField]private Image _cardImage;
        [SerializeField]private Sprite _defaultSprite;
        [SerializeField]private Sprite _topCardSprite;
        [SerializeField] private Vector3 _offset = Vector3.zero;

        private Action onMoveComplete;
        private Transform _transformParent;
        private RectTransform _currentRectTransform;
        private void Start()
        {
            _currentRectTransform = GetComponent<RectTransform>();
        }

        /// <summary>
        /// Moves the card to a target position over a specified duration using easing.
        /// </summary>
        /// <param name="target">The target world position.</param>
        /// <param name="duration">Duration of the movement.</param>
        /// <param name="onComplete">Callback to invoke when movement finishes.</param>
        public void MoveTo(Vector3 targetWorldPos, float duration, Action onComplete)
        {
            onMoveComplete = onComplete;

            _transformParent = this.transform.parent;
            var grandParent = _transformParent.parent;
            _currentRectTransform.SetParent(grandParent);  // Set parent as grandparent
            _currentRectTransform.SetAsLastSibling();  // Ensure it stays on top in the UI hierarchy

            _currentRectTransform.DOMove(targetWorldPos, duration)
                .SetEase(Ease.InOutCubic)
                .OnComplete(() => OnMoveComplete());
        }

        private Vector2 ConvertWorldToAnchoredPosition(Vector3 worldPos, RectTransform uiParent)
        {
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(worldPos);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                uiParent, screenPoint, Camera.main, out Vector2 localPoint
            );
            return localPoint;
        }

        /// <summary>
        /// Called by iTween when movement is completed.
        /// Resets the sprite and sorting order to default.
        /// </summary>
        private void OnMoveComplete()
        {
            if (_cardImage && _defaultSprite)
                _cardImage.sprite = _defaultSprite;

            _currentRectTransform.SetParent(_transformParent);  // Set parent back to the orignal one
            onMoveComplete?.Invoke();
        }

        /// <summary>
        /// Sets the card’s visual and position based on its index in a stack.
        /// </summary>
        /// <param name="index">Index of the card in the stack (0 = top).</param>
        public void SetVisualAndOffset(int index)
        {
            if(_currentRectTransform == null)
                _currentRectTransform = GetComponent<RectTransform>();

            gameObject.SetActive(true);

            if (index == 0)
            {
                // Apply the offset only to the top card
                _currentRectTransform.anchoredPosition = new Vector2(_offset.x, _offset.y);
                _currentRectTransform.SetAsLastSibling(); // Make sure it's visually on top
                _cardImage.sprite = _topCardSprite;
            }
            else
            {
                // No offset for lower cards
                _currentRectTransform.anchoredPosition = Vector2.zero;
                _currentRectTransform.SetSiblingIndex(index); // Keep stacking order correct
                _cardImage.sprite = _defaultSprite;
            }
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
