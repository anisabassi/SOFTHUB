using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoftHub.AceOfShadows
{
    public class DeckManager : MonoBehaviour
    {
        public int totalCards = 144;

        [Tooltip("Assign stack transforms with CardStack component in the scene")]
        public List<CardStack> stacks;

        private void Start()
        {
            DistributeCards();
            StartCoroutine(MoveCardRoutine());
        }

        private void DistributeCards()
        {
            int cardsPerStack = totalCards / stacks.Count;

            foreach (var stack in stacks)
            {
                stack.InitCards(cardsPerStack);
            }
        }

        private IEnumerator MoveCardRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);

                int fromIndex = GetRandomNonEmptyStackIndex();
                if (fromIndex == -1) yield break;

                int toIndex = GetRandomTargetStackIndex(fromIndex);
                var fromStack = stacks[fromIndex];
                var toStack = stacks[toIndex];

                Card topCard = fromStack.GetTopCard();
                if (topCard != null)
                {
                    Vector3 targetPos = toStack.GetNextCardPosition();

                    topCard.MoveTo(targetPos, 2f, () => {
                        toStack.AddNewTopCard(topCard);
                        fromStack.RemovedTopCard(topCard);
                    });

                    yield return new WaitForSeconds(2f);
                }
            }
        }


        private int GetRandomNonEmptyStackIndex()
        {
            List<int> valid = new List<int>();
            for (int i = 0; i < stacks.Count; i++)
            {
                if (!stacks[i].IsEmpty())
                    valid.Add(i);
            }

            return valid.Count > 0 ? valid[Random.Range(0, valid.Count)] : -1;
        }

        private int GetRandomTargetStackIndex(int excludeIndex)
        {
            List<int> options = new List<int>();
            for (int i = 0; i < stacks.Count; i++)
            {
                if (i != excludeIndex)
                    options.Add(i);
            }

            return options[Random.Range(0, options.Count)];
        }
    }
}
