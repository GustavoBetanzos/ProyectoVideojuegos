using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TeamDrew : MonoBehaviour {
    [SerializeField] List<Enemie> team;

    private void Start () {
        foreach(var enemie in team) {
           enemie.Init();
        }
    }

    public Enemie getMemberTeam(){
        return team.Where(x => x.HP > 0).FirstOrDefault();
    }
}
    