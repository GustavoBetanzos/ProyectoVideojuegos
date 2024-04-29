using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="Move", menuName ="Enemie/Create new move" )]
public class MoveBase : ScriptableObject{
    [SerializeField] string moveName;
    
    [SerializeField] int power;
    [SerializeField] int accuracy;
    [SerializeField] int pp;

    public string NameMove{
        get{return moveName;}
    }

    public int Power{
        get{return power;}
    }

    public int Accuracy{
        get{return accuracy;}
    }

    public int Pp{
        get{return pp;}
    }
}