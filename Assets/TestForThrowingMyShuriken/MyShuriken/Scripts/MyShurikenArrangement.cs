﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

/// <summary>
/// 自分手裏剣配置クラス
/// </summary>
public class MyShurikenArrangement : MonoBehaviour
{
    private readonly Vector3 shuriken1IniPos = new Vector3(0.0f, -3.0f, 0.0f);

    /// <summary>
    /// 自分手裏剣1の座標
    /// </summary>
    private Vector2 shuriken1Position;

    /// <summary>
    /// 自分手裏剣1の座標のプロパティ
    /// </summary>
    /// <remarks>内部でthis.transform.positionを更新する時にこのプロパティも必ず更新すること
    /// 理由：（更新しないと）外部で使う時に実際の自分手裏剣と座標が異なってしまうから</remarks>
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
        OnMyShurikenArranging();
    }

    /// <summary>
    /// 自エリア(MyArea_Panel)内で自分手裏剣をドラッグで移動する
    /// </summary>
    void OnMyShurikenArranging()
    {
        // 最後の作業：
        // MyArea_Image内でタッチした場合に修正

        // タスク：
        // ドラッグ中にフラグを立ててスワイプ処理をしないようにする
        // 目標作業：少なくとも1を作り終える

        // タッチ情報の取得
        Touch[] myTouches = Input.touches;
        Vector2 touchPosition = Vector2.zero;

        // タッチ数ぶんループ
        for (int i = 0; i < myTouches.Length; i++)
        {
            // タッチ座標のワールド座標を取得
            touchPosition = Camera.main.ScreenToWorldPoint(myTouches[i].position);
        }

        // タッチ座標がMyArea_Image内にある場合
        if (MyAreaInfo.topLeftPos.x <= touchPosition.x && touchPosition.x <= MyAreaInfo.bottomRightPos.x
                                                       &&
           MyAreaInfo.bottomRightPos.y <= touchPosition.y && touchPosition.y <= MyAreaInfo.topLeftPos.y)
        {
            // 自分手裏剣の座標にタッチ座標を設定する
            this.transform.position = touchPosition;

            // 自分手裏剣の座標を更新したのでShuriken1Positionを更新
            Shuriken1Position = this.transform.position;
        }
    }
}