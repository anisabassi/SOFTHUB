using System.Collections.Generic;
using UnityEngine;

namespace SoftHub.AceOfShadows
{
    /// <summary>
    /// Manages a stack of cards, including initialization, visual updates, and stack operations.
    /// </summary>
    public class CardStack : MonoBehaviour
    {
        [SerializeField]private GameObject _cardPrefab;
        private List<Card> cards = new List<Card>();

        /// <summary>
        /// Initializes the card stack with a given number of cards.
        /// </summary>
        /// <param name="cardsPerStack">Number of cards to instantiate in the stack.</param>
        public void InitCards(int cardsPerStack)
        {
            if (cards == null)
                cards = new List<Card>();

            for (int i = 0; i < cardsPerStack; i++)
            {
                GameObject cardGO = Instantiate(_cardPrefab,this.transform);
                Card card = cardGO.GetComponent<Card>();
                cards.Insert(0, card);
            }

            UpdateCardVisuals();
        }

        /// <summary>
        /// Adds a new card to the top of the stack.
        /// </summary>
        /// <param name="arrivedTopCard">The card to be added to the top.</param>
        public void AddNewTopCard(Card arrivedTopCard)
        {
            cards.Add(arrivedTopCard);
            arrivedTopCard.transform.parent = this.transform;
            UpdateCardVisuals();
        }

        /// <summary>
        /// Removes the specified card from the stack.
        /// </summary>
        /// <param name="departedCard">The card to be removed from the stack.</param>
        public void RemovedTopCard(Card departedCard)
        {
            cards.Remove(departedCard);
            UpdateCardVisuals();
        }

        /// <summary>
        /// Gets the current top card in the stack.
        /// </summary>
        /// <returns>The top card if available, otherwise null.</returns>
        public Card GetTopCard()
        {
            return cards.Count > 0 ? cards[0] : null;
        }

        /// <summary>
        /// Gets the world position where the next card should be placed.
        /// </summary>
        /// <returns>Position for the next card.</returns>
        public Vector3 GetNextCardPosition()
        {
            return transform.position;
        }

        /// <summary>
        /// Updates the visual representation of the cards in the stack.
        /// Only the top 2 cards are shown; the rest are hidden.
        /// </summary>
        private void UpdateCardVisuals()
        {
            for (int i = 0; i < cards.Count; i++)
            {
                if (i == 0 || i == 1)
                {
                    cards[i].SetVisualAndOffset(i);
                }
                else
                {
                    cards[i].Hide();
                }
            }
        }

        /// <summary>
        /// Checks whether the card stack is empty.
        /// </summary>
        /// <returns>True if no cards in stack, otherwise false.</returns>
        public bool IsEmpty()
        {
            return cards == null || cards.Count == 0;
        }

    }
}
