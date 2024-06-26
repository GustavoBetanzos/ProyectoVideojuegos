using System;
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

    public event Action<bool> OnBattleOver;
    BattleState state;
    int currentAction;
    int currentMove;

    Enemie wildEnemie;
    TeamDrew team;
    public void StartBattle(TeamDrew team, Enemie wildEnemie){
        this.wildEnemie = wildEnemie;
        this.team = team;
        StartCoroutine(SetUpBattle());
    }

    public IEnumerator SetUpBattle(){
        playerUnit.Setup(team.getMemberTeam());
        enemyUnit.Setup(wildEnemie);
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

        var move = playerUnit.Enemie.Moves[currentMove];
        move.Pp--;
        yield return dialogBox.TypeDialog($"{playerUnit.Enemie.Base.NameEnemy} ha usado {move.Base.Name}");

        playerUnit.PlayAttackAnimation();
        yield return new WaitForSeconds(1f);

        enemyUnit.PlayHitAnimation();

        var damageDetails = enemyUnit.Enemie.TakeDamage(move, playerUnit.Enemie);
        yield return enemyHud.UpdateHP();
        yield return ShowDamageDetails(damageDetails);
        if (damageDetails.Fainted){
            yield return HandlePokemonFainted(enemyUnit);
        }else{
            StartCoroutine(EnemyMove());
        }
    }

    IEnumerator HandlePokemonFainted(BattleUnit faintedUnit){
        yield return dialogBox.TypeDialog($"{faintedUnit.Enemie.Base.NameEnemy} ha sido vencido.");
            faintedUnit.PlayFaintAnimation();
            yield return new WaitForSeconds(2f);
                int expYield = faintedUnit.Enemie.Base.ExpYield;
                int enemyLevel = faintedUnit.Enemie.Level;
                //int expGain = Mathf.FloorToInt((expYield * enemyLevel)/5);
                int expGain = 5;
                playerUnit.Enemie.Exp += expGain;
                yield return dialogBox.TypeDialog($"{playerUnit.Enemie.Base.NameEnemy} ha obtenido {expGain} puntos de experiencia.");
                yield return playerHud.SetExpSmooth();

                while(playerUnit.Enemie.CheckForLevelUp()){
                    playerHud.SetLevel();
                    yield return dialogBox.TypeDialog($"{playerUnit.Enemie.Base.NameEnemy} ha subido a nivel {playerUnit.Enemie.Level}.");
                    yield return playerHud.SetExpSmooth(true);
                }

                yield return new WaitForSeconds(1f);
                OnBattleOver(true);
    }

    IEnumerator EnemyMove(){
        state=BattleState.EnemyMove;
        var move = enemyUnit.Enemie.GetRandomMove();
        move.Pp--;
        yield return dialogBox.TypeDialog($"{enemyUnit.Enemie.Base.NameEnemy} ha usado {move.Base.Name}");

        enemyUnit.PlayAttackAnimation();
        yield return new WaitForSeconds(1f);

        playerUnit.PlayHitAnimation();
        

        var damageDetails = playerUnit.Enemie.TakeDamage(move, playerUnit.Enemie); 
        yield return ShowDamageDetails(damageDetails);
        if (damageDetails.Fainted){
            yield return dialogBox.TypeDialog($"{playerUnit.Enemie.Base.NameEnemy} ha sido vencido.");
            playerUnit.PlayFaintAnimation();
            yield return new WaitForSeconds(2f);
            OnBattleOver(false);
        }else{
            PlayerAction();
        }
    }

    IEnumerator ShowDamageDetails(DamageDetails damageDetails){
        if(damageDetails.Critical > 1f){
            yield return dialogBox.TypeDialog("Ese ataque ha sido más fuerte de lo normal.");
        }
    }   
    public void HandleUpdate(){
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
                OnBattleOver(false);
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