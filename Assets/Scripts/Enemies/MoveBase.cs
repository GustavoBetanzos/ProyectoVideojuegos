using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="Move", menuName ="Enemie/Create new move" )]
public class MoveBase : ScriptableObject{
    [SerializeField] string name;
    
    [TextArea]
    [SerializeField] string description;
    [SerializeField] int power;
    [SerializeField] int pp;
}