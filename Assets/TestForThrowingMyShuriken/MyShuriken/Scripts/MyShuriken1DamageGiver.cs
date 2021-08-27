using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyShuriken1DamageGiver : MonoBehaviour, IApplicableDamage
{
    public int ApplyDamage()
    {
        int damageAmount = 1;


        // ダメージ量を返す
        return damageAmount;
    }
}
