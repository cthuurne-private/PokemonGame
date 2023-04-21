using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
    [SerializeField]
    private bool isPlayerUnit;

    public Pokemon Pokemon { get; set; }

    private Image image;
    private Vector3 originalPos;
    private Color originalColor;

    private void Awake()
    {
        image = GetComponent<Image>();
        originalPos = image.transform.localPosition;
        originalColor = image.color;
    }

    public void Setup(Pokemon pokemon)
    {
        Pokemon = pokemon;

        image.sprite = isPlayerUnit ? Pokemon.Base.BackSprite : Pokemon.Base.FrontSprite;

        image.color = originalColor;
        PlayEnterAnimation();
    }

    public void PlayEnterAnimation()
    {
        image.transform.localPosition = isPlayerUnit ? new Vector3(-500f, originalPos.y) : new Vector3(500f, originalPos.y);

        image.transform.DOLocalMoveX(originalPos.x, 1f);
    }

    public void PlayAttackAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(isPlayerUnit ? image.transform.DOLocalMoveX(originalPos.x + 50f, 0.25f) : image.transform.DOLocalMoveX(originalPos.x - 50f, 0.25f));

        sequence.Append(image.transform.DOLocalMoveX(originalPos.x, 0.5f));
    }

    public void PlayHitAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(image.DOColor(Color.gray, 0.1f));
        sequence.Append(image.DOColor(originalColor, 0.1f));
    }

    public void PlayFaintAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(image.transform.DOLocalMoveY(originalPos.y - 150f, 0.5f));
        sequence.Join(image.DOFade(0f, 0.5f));
    }
}
