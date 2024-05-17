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
    int currentAction;
    int currentMove;
    private void Start(){
        StartCoroutine(SetUpBattle());
    }

    public IEnumerator SetUpBattle(){
        playerUnit.Setup();
        enemyUnit.Setup();
        playerHud.SetData(playerUnit.Enemie);
        enemyHud.SetData(enemyUnit.Enemie);
        dialogBox.SetMoveNames(playerUnit.Enemie.Moves);
        yield return dialogBox.TypeDialog($"Un {enemyUnit.Enemie.Base.NameEnemy} ha aparecido.");
        PlayerAction();
    }

    void PlayerAction(){
        state= BattleState.PlayerAction;
        StartCoroutine(dialogBox.TypeDialog("Selecciona una accion"));
        dialogBox.EnableActionSelector(true);
    }

    void PlayerMove(){
        state=BattleState.PlayerMove;
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);
        dialogBox.EnableMoveSelector(true);
    }

    IEnumerator PerformPlayerMove(){

        state =BattleState.Busy;

        var move = playerUnit.Enemie.Moves[currentAction];
        yield return dialogBox.TypeDialog($"{playerUnit.Enemie.Base.NameEnemy} ha usado {move.Base.Name}");

        playerUnit.PlayAttackAnimation();
        yield return new WaitForSeconds(1f);

        enemyUnit.PlayHitAnimation();

        var damageDetails = enemyUnit.Enemie.TakeDamage(move, playerUnit.Enemie);
        yield return enemyHud.UpdateHP();
        yield return ShowDamageDetails(damageDetails);
        if (damageDetails.Fainted){
            yield return dialogBox.TypeDialog($"{enemyUnit.Enemie.Base.NameEnemy} ha sido vencido.");
            enemyUnit.PlayFaintAnimation();
        }else{
            StartCoroutine(EnemyMove());
        }
    }

    IEnumerator EnemyMove(){
        state=BattleState.EnemyMove;
        var move = enemyUnit.Enemie.GetRandomMove();
        yield return dialogBox.TypeDialog($"{enemyUnit.Enemie.Base.NameEnemy} ha usado {move.Base.Name}");

        enemyUnit.PlayAttackAnimation();
        yield return new WaitForSeconds(1f);

        playerUnit.PlayHitAnimation();
        

        var damageDetails = playerUnit.Enemie.TakeDamage(move, playerUnit.Enemie); //cambiar el ultimo player por enemy
        yield return playerHud.UpdateHP();
        yield return ShowDamageDetails(damageDetails);
        if (damageDetails.Fainted){
            yield return dialogBox.TypeDialog($"{playerUnit.Enemie.Base.NameEnemy} ha sido vencido.");
            playerUnit.PlayFaintAnimation();
        }else{
            PlayerAction();
        }
    }

    IEnumerator ShowDamageDetails(DamageDetails damageDetails){
        if(damageDetails.Critical > 1f){
            yield return dialogBox.TypeDialog("Ese ataque ha sido más fuerte de lo normal.");
        }
    }   
    private void Update(){
        if(state==BattleState.PlayerAction){
            HandleActionSelector();
        }
        else if(state==BattleState.PlayerMove){
            HandleMoveSelection();
        }
    }

    void HandleActionSelector(){
        if(Input.GetKeyDown(KeyCode.DownArrow)){
            if(currentAction <1){
                ++currentAction;
            }
        }else if (Input.GetKeyDown(KeyCode.UpArrow)){
            if(currentAction >0){
                --currentAction;
            }
        }

        dialogBox.UpdateActionSelection(currentAction);

        if(Input.GetKeyDown(KeyCode.Return)){
            if(currentAction ==0){
                PlayerMove();
            }
            else if(currentAction ==1){

            }
        }
    }

    void HandleMoveSelection(){
        if(Input.GetKeyDown(KeyCode.RightArrow)){
            if(currentMove <playerUnit.Enemie.Moves.Count-1){
                ++currentMove;
            }
        }else if (Input.GetKeyDown(KeyCode.LeftArrow)){
            if(currentMove >0){
                --currentMove;
            }
        }else if (Input.GetKeyDown(KeyCode.DownArrow)){
            if(currentMove <playerUnit.Enemie.Moves.Count-2){
                currentMove +=2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow)){
            if(currentMove >1){
                currentMove -=2;
            }
        }
        dialogBox.UpdateMoveSelection(currentMove, playerUnit.Enemie.Moves[currentMove]);

        if(Input.GetKeyDown(KeyCode.Return)){
            dialogBox.EnableMoveSelector(false);
            dialogBox.EnableDialogText(true);
            StartCoroutine(PerformPlayerMove());
        }
    }
}