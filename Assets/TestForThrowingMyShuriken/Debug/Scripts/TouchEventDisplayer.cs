using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Textゲームオブジェクトを取得するために必要

/// <summary>
/// タッチイベントをテキストに表示するデバッグ用のクラス
/// </summary>
public class TouchEventDisplayer : MonoBehaviour
{
    /// <summary>
    /// 画面上でどのイベントが呼ばれたのか画面のテキストに表示する
    /// </summary>
    /// <remarks>例：OnMouseEnterが呼ばれた　→　"コライダに入った"とTextに表示する</remarks>
    public void DisplayWhichEventCalled(string eventName)
    {
        this.gameObject.GetComponent<Text>().text = eventName;
    }

    //　※OnMouseEnterはコライダがアタッチされていないと呼ばれないはず
    //private void OnMouseEnter()
    //{
    //    string eventName = "コライダに入った";

    //    DisplayWhichEventCalled(eventName);
    //}
}
