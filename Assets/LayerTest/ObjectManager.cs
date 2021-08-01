using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    // Inspector
    [SerializeField] private GameObject testObject;

    // ゲームオブジェクトのレイヤーを取得する
    public void GetLayer()
    {
        Debug.Log(testObject.layer);
    }

    // ゲームオブジェクトのレイヤーを変更する
    public void SetLayer(int layerNumber)
    {
        testObject.layer = layerNumber;
    }
}