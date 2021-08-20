using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 手裏剣のHPが0になったらDestroyするスクリプト
/// </summary>
public class ShurikenDestroyer : MonoBehaviour
{
    public delegate void Hp0ShurikenEventHandler(GameObject hp0Shuriken);

    public event Hp0ShurikenEventHandler OnShurikenHpGotTo0;

    // Start is called before the first frame update
    void Start()
    {
        OnShurikenHpGotTo0 += DestroyHP0Shuriken;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DestroyHP0Shuriken(GameObject shuriken)
    {

    }
}
