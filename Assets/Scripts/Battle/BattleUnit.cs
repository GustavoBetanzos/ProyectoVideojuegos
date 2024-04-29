using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] EnemiesBase _base;
    [SerializeField] int level;
    
    public Enemie Enemie{get; set;}

    public void Setup(){
        Enemie = new Enemie(_base, level);
        GetComponent<Image>().sprite = Enemie.Base.FrontSprite;
    }

}