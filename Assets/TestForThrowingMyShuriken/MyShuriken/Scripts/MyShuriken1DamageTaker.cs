using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyShuriken1DamageTaker : MonoBehaviour
{
    [SerializeField]
    private MyShurikenThrower myShurikenThrower = default;

    [SerializeField]
    private EnemyShurikenDamageTaker enemyShurikenDamageTaker = default;

    [SerializeField]
    private Text figureText = default;

    // Start is called before the first frame update
    void Start()
    {
        enemyShurikenDamageTaker.OnEnemyShurikenDamaged += damageAmount =>
        {
            // 自分手裏剣にダメージを与える処理を記述する
            // ステータス：まだテストしてない。後、EnemyshurikneDamageTakerのApplyDamageToTextにif (this.gameObject != null)を追加したので、動作確認
            myShurikenThrower.OnMyShurikenInstantiated += InstantiatedMyShuriken1 =>
            {
                int remainingHp = CalculateDamage(damageAmount);

                DestroyShurikenIfHp0(damageAmount, InstantiatedMyShuriken1);

                if (InstantiatedMyShuriken1 != null)
                {
                    ApplyDamageToText(remainingHp);
                }
            };
        };
    }

    void DestroyShurikenIfHp0(int remainingHp, GameObject shuriken)
    {
        if (remainingHp == 0)
        {
            Destroy(shuriken);
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
