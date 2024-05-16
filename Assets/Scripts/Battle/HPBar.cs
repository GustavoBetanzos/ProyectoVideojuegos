using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    [SerializeField] GameObject health;

    public void SetHP(float hpNormalized){
        health.transform.localScale = new Vector3(hpNormalized, 1f);
    }

    public IEnumerator SetHPSlow(float newHP){
        float actHP= health.transform.localScale.x;
        float changeAmt = actHP - newHP;

        while(actHP - newHP > Mathf.Epsilon){
            actHP -= changeAmt * Time.deltaTime;
            health.transform.localScale = new Vector3(actHP, 1f);
            yield return null;
        }

        health.transform.localScale = new Vector3(newHP,1f); 
    }
}