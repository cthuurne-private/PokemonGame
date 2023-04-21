using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleLog : MonoBehaviour
{
    [SerializeField]
    private Text dialogText;

    [SerializeField]
    private int lettersPerSecond;

    [SerializeField]
    private GameObject actionSelector;

    [SerializeField]
    private GameObject moveSelector;

    [SerializeField]
    private GameObject moveDetails;

    [SerializeField]
    private List<Text> actionTexts;

    [SerializeField]
    private List<Text> moveTexts;

    [SerializeField]
    private Text ppText;

    [SerializeField]
    private Text typeText;

    [SerializeField]
    private Color highlightedColor;

    public void SetDialog(string dialog)
    {
        dialogText.text = dialog;
    }

    public IEnumerator TypeDialog(string dialog)
    {
        dialogText.text = "";

        foreach (var letter in dialog)
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }

        yield return new WaitForSeconds(1f);
    }

    public void EnableDialogBoxText(bool enabled)
    {
        dialogText.enabled = enabled;
    }

    public void EnableActionSelector(bool enabled)
    {
        actionSelector.SetActive(enabled);
    }

    public void EnableMoveSelector(bool enabled)
    {
        moveSelector.SetActive(enabled);
        moveDetails.SetActive(enabled);
    }

    public void UpdateActionSelection(int selectedAction)
    {
        for (var i = 0; i < actionTexts.Count; i++)
        {
            if (i == selectedAction)
            {
                actionTexts[i].color = highlightedColor;
            }
            else
            {
                actionTexts[i].color = Color.black;
            }
        }
    }

    public void UpdateMoveSelection(int selectMove, Move move)
    {
        for (var i = 0; i < moveTexts.Count; i++)
        {
            if (i == selectMove)
            {
                moveTexts[i].color = highlightedColor;
            }
            else
            {
                moveTexts[i].color = Color.black;
            }

            ppText.text = $"PP {move.PP}/{move.Base.Pp}";
            typeText.text = move.Base.Type.ToString();
        }
    }

    public void SetMoveNames(List<Move> moves)
    {
        for (var i = 0; i < moveTexts.Count; i++)
        {
            moveTexts[i].text = i < moves.Count ? moves[i].Base.Name : "-";
        }
    }
}
