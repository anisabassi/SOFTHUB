using System;
using UnityEngine;

namespace SoftHub.AceOfShadows
{
    public class Card : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        public Sprite defaultSprite;
        public Sprite topCardSprite;

        public int defaultSortingOrder;
        public int topSortingOrder;

        public Vector3 offset = Vector3.zero;

        private Action onMoveComplete;

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

        private void OnMoveComplete()
        {
            if (spriteRenderer && defaultSprite)
                spriteRenderer.sprite = defaultSprite;

            spriteRenderer.sortingOrder = defaultSortingOrder;

            onMoveComplete?.Invoke();
        }

        public void SetVisualAndOffset(int index)
        {
            gameObject.SetActive(true);

            float x = (offset.x * index);
            float y = (offset.y * index);
            float z = (offset.y * index);

            transform.localPosition = new Vector3(x, y, z);

            spriteRenderer.sprite = (index == 0) ? topCardSprite : defaultSprite;
            spriteRenderer.sortingOrder = (index == 0) ? topSortingOrder : defaultSortingOrder;
        }

        public void Hide()
        {
            gameObject.SetActive(false); 
        }
    }
}
