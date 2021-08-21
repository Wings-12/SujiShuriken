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

    public event Hp0ShurikenEventHandler OnShurikenHpGotTo0;

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

                DestroyShurikenIfHpGotTo0(remainingHp, this.gameObject);

                ApplyDamageToText(remainingHp);
            }
            else
            {
                Debug.Log("IapplicableDamageが取得できませんでした");
            }
        }
    }

    void DestroyShurikenIfHpGotTo0(int remainingHp, GameObject enemyShuriken)
    {
        if (remainingHp == 0)
        {
            // 本当はイベント処理で手裏剣を1箇所で破棄したいが、バグっているので、ここでDestroyしている
            //Destroy((enemyShuriken));

            // データが発行されてない？
            OnShurikenHpGotTo0(enemyShuriken);
        }
    }

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
