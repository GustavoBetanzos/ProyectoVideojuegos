using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; 

public class BattleHud : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text levelText;
    [SerializeField] HPBar hpBar;
    [SerializeField] GameObject expBar;

    Enemie _enemie;
    public void SetData(Enemie enemie){
        _enemie = enemie;
        nameText.text = enemie.Base.NameEnemy;
        SetLevel();
        hpBar.SetHP((float)enemie.HP / enemie.MaxHp);
        SetExp();
        
    }

    public IEnumerator UpdateHP(){
        yield return hpBar.SetHPSlow((float)_enemie.HP / _enemie.MaxHp);
    }

    public void SetLevel(){
        levelText.text="Nivel " + _enemie.Level;
    }

    public void SetExp(){
        if(expBar == null) return;


        float normalizedExp = GetNormalizedExp();
        expBar.transform.localScale = new Vector3(normalizedExp, 1, 1);
    }

    public IEnumerator SetExpSmooth(bool reset=false){
        if(expBar == null) yield break;
        
        if(reset==true){
            expBar.transform.localScale = new Vector3(0, 1, 1);
        }

        float normalizedExp = GetNormalizedExp();
        yield return expBar.transform.DOScaleX(normalizedExp, 1.5f).WaitForCompletion();
    }

    float GetNormalizedExp(){
        int currentLevelExp = _enemie.Base.GetExpForLevel(_enemie.Level);
        int nextLevelExp = _enemie.Base.GetExpForLevel(_enemie.Level + 1);
        float normalizedExp = (float)(_enemie.Exp - currentLevelExp) / (nextLevelExp - currentLevelExp);
        return Mathf.Clamp01(normalizedExp);
    }

}