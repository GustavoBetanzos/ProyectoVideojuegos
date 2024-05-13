using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState{ Start, PlayerAction, PlayerMove, EnemyMove, Busy}
public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHud playerHud;
    [SerializeField] BattleHud enemyHud;
    [SerializeField] BattleDialogBox dialogBox;

    BattleState state;
    private void Start(){
        StartCoroutine(SetUpBattle());
    }

    public IEnumerator SetUpBattle(){
        playerUnit.Setup();
        enemyUnit.Setup();
        playerHud.SetData(playerUnit.Enemie);
        enemyHud.SetData(enemyUnit.Enemie);
        yield return dialogBox.TypeDialog($"Un {enemyUnit.Enemie.Base.NameEnemy} ha aparecido.");
        yield return new WaitForSeconds(1f);
        PlayerAction();
    }

    void PlayerAction(){
        state= BattleState.PlayerAction;
        StartCoroutine(dialogBox.TypeDialog("Selecciona una accion"));
        dialogBox.EnableActionSelector(true);
    }
}