using UnityEngine;
using UnityEngine;
using DG.Tweening;
public class NewMonoBehaviourScript : MonoBehaviour
{
    public RectTransform card;

    void Start()
    {
        card.DOAnchorPos(new Vector2(200, -200), 1f).SetEase(Ease.InOutSine);
    }
}
