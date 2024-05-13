using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
    public MoveBase Base { get; set; }
    public int Pp { get; set; }

    public Move(MoveBase eBase)
    {
        if (eBase != null)
        {
            Base = eBase;
            Pp = eBase.Pp;
        }
        else
        {
            Debug.LogError("eBase es null al intentar crear un nuevo objeto Move.");
        }
    }
}
