using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class EnemyShurikenDamageTaker : MonoBehaviour
{
    private int damageAount = 0;

    [FormerlySerializedAs("text")]
    [SerializeField] Text figureText = default;

    [SerializeField] ShurikenDestroyer shurikenDestroyer = default;

    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     GameObject shuriken = other.gameObject;
    //
    //     var d = shuriken.GetComponent<IApplicableDamage>();
    //     if (d != null)
    //     {
    //         damageAount = d.ApplyDamage();
    //
    //         ApplyDamageToText();
    //     }
    //     else
    //     {
    //         Debug.Log("IapplicableDamageが取得できませんでした");
    //     }
    // }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == GameObjectName.myShuriken1_Button)
        {
            GameObject shuriken = other.gameObject;

            var d = shuriken.GetComponent(typeof(IApplicableDamage)) as IApplicableDamage;
            if (d != null)
            {
                damageAount = d.ApplyDamage();

                int remainingHp = CalculateDamage(damageAount);

                ApplyDamageToText(remainingHp);
            }
            else
            {
                Debug.Log("IapplicableDamageが取得できませんでした");
            }
        }
    }

    // void DestroyShurikenIfHpGotTo0(int remainingHp, GameObject enemyShuriken)
    // {
    //     if (remainingHp == 0)
    //     {
    //         shurikenDestroyer.OnShurikenHpGotTo0(enemyShuriken);
    //     }
    // }

    int CalculateDamage(int damageAount)
    {
        int remainingHp = int.Parse(figureText.text);

       return remainingHp -= damageAount;
    }

    void ApplyDamageToText(int remainingHp)
    {
        figureText.text = remainingHp.ToString();
    }
}
