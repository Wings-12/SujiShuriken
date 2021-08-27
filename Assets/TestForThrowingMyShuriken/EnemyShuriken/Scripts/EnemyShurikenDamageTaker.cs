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

    public delegate void Hp0ShurikenEventHandler(GameObject hp0Shuriken);
    public event Hp0ShurikenEventHandler OnShurikenHp0;

    public delegate void EnemyShurikenDamagedEventHandler(int damageAmount);
    public event EnemyShurikenDamagedEventHandler OnEnemyShurikenDamaged;

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

            var d = shuriken.GetComponent<IApplicableDamage>();

            if (d != null)
            {
                damageAount = d.ApplyDamage();

                int remainingHp = CalculateDamage(damageAount);

                DestroyShurikenIfHp0(remainingHp, this.gameObject);

                if (this.gameObject != null)
                {
                    ApplyDamageToText(remainingHp);
                }

                // ダメージ量を自分手裏剣にも反映する
                OnEnemyShurikenDamaged(damageAount);
            }
            else
            {
                Debug.Log("IapplicableDamageが取得できませんでした");
            }
        }
    }

    void DestroyShurikenIfHp0(int remainingHp, GameObject enemyShuriken)
    {
        if (remainingHp == 0)
        {
            OnShurikenHp0(enemyShuriken);
        }
    }

    int CalculateDamage(int damageAｍount)
    {
        int remainingHp = int.Parse(figureText.text);

       return remainingHp -= damageAｍount;
    }

    void ApplyDamageToText(int remainingHp)
    {
        figureText.text = remainingHp.ToString();
    }
}
