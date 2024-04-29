using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text levelText;
    [SerializeField] HPBar hpBar;

    public void SetData(Enemie enemie){
        nameText.text = enemie.Base.NameEnemy;
        levelText.text = "Nivel" + enemie.Level;
        hpBar.SetHP((float)enemie.HP / enemie.MaxHp);
    }

}