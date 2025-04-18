using System.Collections.Generic;
using UnityEngine;

namespace SoftHub.AceOfShadows
{
    public class CardStack : MonoBehaviour
    {
        public GameObject cardPrefab;
        private List<Card> cards;

        public void InitCards(int cardsPerStack)
        {
            if (cards == null)
                cards = new List<Card>();

            for (int i = 0; i < cardsPerStack; i++)
            {
                GameObject cardGO = Instantiate(cardPrefab,this.transform);
                Card card = cardGO.GetComponent<Card>();
                cards.Insert(0, card);
            }

            UpdateCardVisuals();
        }

        public void AddNewTopCard(Card arrivedTopCard)
        {
            cards.Add(arrivedTopCard);
            arrivedTopCard.transform.parent = this.transform;
            UpdateCardVisuals();
        }

        public void RemovedTopCard(Card departedCard)
        {
            cards.Remove(departedCard);
            UpdateCardVisuals();
        }

        public Card GetTopCard()
        {
            return cards.Count > 0 ? cards[0] : null;
        }

        public Vector3 GetNextCardPosition()
        {
            return transform.position;
        }

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

        public bool IsEmpty()
        {
            return cards == null || cards.Count == 0;
        }

    }
}
