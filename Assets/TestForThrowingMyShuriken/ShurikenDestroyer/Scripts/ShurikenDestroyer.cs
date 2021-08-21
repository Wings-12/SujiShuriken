using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 手裏剣のHPが0になったらDestroyするスクリプト
/// </summary>
public class ShurikenDestroyer : MonoBehaviour
{
    [SerializeField] EnemyShurikenDamageTaker enemyShurikenDamageTaker = default;

    // Start is called before the first frame update
    void Start()
    {
        enemyShurikenDamageTaker.OnShurikenHpGotTo0 += enemyShuriken =>
        {
            Destroy(enemyShuriken);
        };
    }
}
