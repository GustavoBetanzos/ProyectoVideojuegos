using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMapArea : MonoBehaviour {
    [SerializeField] protected List<Enemie> wildEnemies;
    public Enemie GetRandomEnemie() {
        var wildEnemie = wildEnemies[Random.Range(0, wildEnemies.Count)];
        wildEnemie.Init();
        return wildEnemie;
    }
}