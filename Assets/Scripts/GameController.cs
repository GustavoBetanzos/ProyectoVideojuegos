using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState{ FreeRoam, Battle, Dialog }
public class GameController : MonoBehaviour {
    [SerializeField] PlayerController playerController;
    [SerializeField] BattleSystem battleSystem;
    [SerializeField] Camera worldCamera;
    GameState state;

    private void Start () {
        playerController.OnEncountered += StartBattle;
        battleSystem.OnBattleOver += EndBattle;
        DialogManager.Instance.OnShowDialog += () =>{
            state=GameState.Dialog;
        };

        DialogManager.Instance.OnCloseDialog += () =>{
            if(state==GameState.Dialog){
                state=GameState.FreeRoam;
            }
        };
    }

    void StartBattle (){
        state=GameState.Battle;
        battleSystem.gameObject.SetActive(true);
        worldCamera.gameObject.SetActive(false);
        var wildEnemie = FindObjectOfType<BaseMapArea>().GetComponent<BaseMapArea>().GetRandomEnemie();
        var team=playerController.GetComponent<TeamDrew>();
        battleSystem.StartBattle(team, wildEnemie);
    }

    void EndBattle (bool won){
        state=GameState.FreeRoam;
        battleSystem.gameObject.SetActive(false);
        worldCamera.gameObject.SetActive(true);
    }

    private void Update(){
        if(state == GameState.FreeRoam){
            playerController.HandleUpdate();
        }else if(state == GameState.Battle){
            battleSystem.HandleUpdate();
        }else if(state == GameState.Dialog){
            DialogManager.Instance.HandleUpdate();
        }
    }

}