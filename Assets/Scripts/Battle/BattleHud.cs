using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text levelText;
    [SerializeField] HPBar hpBar;

    Enemie _enemie;
    public void SetData(Enemie enemie){
        _enemie = enemie;
        nameText.text = enemie.Base.NameEnemy;
        levelText.text = "Nivel " + enemie.Level;
        hpBar.SetHP((float)enemie.HP / enemie.MaxHp);
    }

    public IEnumerator UpdateHP(){
        yield return hpBar.SetHPSlow((float)_enemie.HP / _enemie.MaxHp);
    }

}