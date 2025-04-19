using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoftHub.AceOfShadows
{
    /// <summary>
    /// Manages the deck logic, including distributing cards across stacks and moving them during gameplay.
    /// </summary>
    public class DeckManager : MonoBehaviour
    {
        [SerializeField]private int _totalCards = 144;
        [SerializeField] private List<CardStack> _stacks = new List<CardStack>();

        private void Start()
        {
            DistributeCards();
            StartCoroutine(MoveCardRoutine());
        }

        /// <summary>
        /// Evenly distributes cards across all available stacks.
        /// </summary>
        private void DistributeCards()
        {
            int cardsPerStack = _totalCards / _stacks.Count;

            foreach (var stack in _stacks)
            {
                stack.InitCards(cardsPerStack);
            }
        }

        /// <summary>
        /// Continuously moves cards from one stack to another at timed intervals.
        /// </summary>
        private IEnumerator MoveCardRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);

                int fromIndex = GetRandomNonEmptyStackIndex();
                if (fromIndex == -1) yield break;

                int toIndex = GetRandomTargetStackIndex(fromIndex);
                var fromStack = _stacks[fromIndex];
                var toStack = _stacks[toIndex];

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

        /// <summary>
        /// Returns the index of a randomly selected non-empty stack.
        /// </summary>
        /// <returns>Index of a non-empty stack, or -1 if none found.</returns>
        private int GetRandomNonEmptyStackIndex()
        {
            List<int> valid = new List<int>();
            for (int i = 0; i < _stacks.Count; i++)
            {
                if (!_stacks[i].IsEmpty())
                    valid.Add(i);
            }

            return valid.Count > 0 ? valid[Random.Range(0, valid.Count)] : -1;
        }

        /// <summary>
        /// Returns a random stack index that is not equal to the excluded index.
        /// </summary>
        /// <param name="excludeIndex">Index to exclude from selection.</param>
        /// <returns>Random target stack index.</returns>
        private int GetRandomTargetStackIndex(int excludeIndex)
        {
            List<int> options = new List<int>();
            for (int i = 0; i < _stacks.Count; i++)
            {
                if (i != excludeIndex)
                    options.Add(i);
            }

            return options[Random.Range(0, options.Count)];
        }
    }
}
