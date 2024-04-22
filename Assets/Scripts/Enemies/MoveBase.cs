using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="Move", menuName ="Enemie/Create new move" )]
public class MoveBase : ScriptableObject{
    [SerializeField] string name;
    
    [SerializeField] int power;
    [SerializeField] int accuracy;
    [SerializeField] int pp;

    public string Name{
        get{return name;}
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