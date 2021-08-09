using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 自分手裏剣配置クラス
/// </summary>
public class MyShurikenArrangement : MonoBehaviour
{
    /// <summary>
    /// 自分手裏剣1の座標
    /// </summary>
    private Vector2 shuriken1Position;

    /// <summary>
    /// 自分手裏剣1の座標のプロパティ
    /// </summary>
    public Vector2 Shuriken1Position
    {
        get { return this.shuriken1Position; }
        set { this.shuriken1Position = value; }
    }

    private void Start()
    {
        this.Shuriken1Position = this.transform.position;
    }

    private void Update()
    {

    }

    private void OnMouseDrag()
    {

    }

    /// <summary>
    /// 自エリア(MyArea_Panel)内で自分手裏剣をドラッグで移動する
    /// </summary>
    void OnMyShurikenArranging()
    {
        // 時間測定開始

        // ↓の処理にif文でMyArea_Panelの4隅の座標以内であれば処理をするように作る
        // 最後の作業：MyAreaInfo.heightとwidth作成完了

        // タッチ情報の取得
        Touch[] myTouches = Input.touches;

        // タッチ数ぶんループ
        for (int i = 0; i < myTouches.Length; i++)
        {
            // タッチ座標のワールド座標を取得
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(myTouches[i].position);

            // 自分手裏剣の座標にタッチ座標を設定する
            this.transform.position = touchPosition;
        }
    }
}