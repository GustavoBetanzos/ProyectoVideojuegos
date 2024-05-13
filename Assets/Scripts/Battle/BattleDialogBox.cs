using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleDialogBox : MonoBehaviour
{
    [SerializeField] Text dialogText;
    [SerializeField] int lettersPerSecond;
    [SerializeField] GameObject actionSelector;
    [SerializeField]  GameObject moveSelector;
    [SerializeField] GameObject moveDetails;
    [SerializeField] List<Text> actionTexts;       
    [SerializeField] List<Text> moveTexts;
    [SerializeField] Text ppText;        

    public void SetDialog(string dialog){
            dialogText.text = dialog;
    }

    public IEnumerator TypeDialog(string dialog){
        dialogText.text= "";
        foreach (var letter in dialog.ToCharArray()){
            dialogText.text += letter;
            yield return new WaitForSeconds(1f/lettersPerSecond);
        }
    }

    public void EnableDialogText(bool enabled){
        dialogText.enabled = enabled;
    }

    public void EnableActionSelector(bool enabled){
        actionSelector.SetActive(enabled);
    }

    public void EnableMoveSelector(bool enabled){
        moveSelector.SetActive(enabled);
        moveDetails.SetActive(enabled);
    }

}