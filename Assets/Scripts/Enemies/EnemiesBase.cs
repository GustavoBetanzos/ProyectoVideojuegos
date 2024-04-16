using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="Enemie", menuName ="Enemie/Create new enemie" )]
public class EnemiesBase : ScriptableObject{
    [SerializeField] string name;

    [TextArea]
    [SerializeField] string description;

    

    [SerializeField] int maxHp;
    [SerializeField] int attack;
    [SerializeField] int defense;

    public string Name{
        get{return name;}
    }

    public string Description{
        get{return description;}
    }

   
    public int MaxHp{
        get{return maxHp;}
    }

    public int Attack{
        get{return attack;}
    }

    public int Defense{
        get{return defense;}
    }

    
}