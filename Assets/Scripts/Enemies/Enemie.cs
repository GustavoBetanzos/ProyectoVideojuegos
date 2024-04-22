using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemie {

    EnemiesBase _base;  
    int level;


    public Enemie(EnemiesBase eBase, int eLevel) {
        _base= eBase;
        level = eLevel;
    }

    public int Attack{
        get{
            return Mathf.FloorToInt((_base.Attack * level)/100f)+5;
        }
    }

    public int Defense{
        get{
            return Mathf.FloorToInt((_base.Defense * level)/100f)+5;
        }
    }

    public int MaxHp{
        get{
            return Mathf.FloorToInt((_base.MaxHp * level)/100f)+10;
        }
    }
}
