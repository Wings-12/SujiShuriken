using System;
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
        // ↓の処理にif文でMyArea_Panelの4隅の座標以内であれば処理をするように作る

        // 必要情報：
        // 上端のy座標と下端のy座標
        // 左端のｘ座標と右端のx座標

        // 最後の作業：
        // 1 MyAreaのtopLeftPos作成

        // タスク：
        // MyArea内の条件文作成
        // ドラッグ条件下の時間測定作成
        // 目標作業：少なくとも1を作り終える

        // MyArea_Image内に自分手裏剣がある場合
        if (MyAreaInfo.topLeftPos.x <= this.transform.position.x && this.transform.position.x <= MyAreaInfo.bottomRightPos.x
        &&
        MyAreaInfo.bottomRightPos.y <= this.transform.position.y && this.transform.position.y <= MyAreaInfo.topLeftPos.y)
        {
            // 問題：
            // 左下に手裏剣をドラッグすると手裏剣が消える

            // 事実
            // 上端、左端、右端でのドラッグ時では手裏剣は消えない
            // 下端ドラッグしたときに消える

            // 推測
            // エリア外には出ていない 理由：エリア外に出たらelseの処理でshuriken1IniPosに戻るはずだから。

            // 時間測定開始

            // タッチ情報の取得
            Touch[] myTouches = Input.touches;

            // タッチ数ぶんループ
            for (int i = 0; i < myTouches.Length; i++)
            {
                // タッチ座標のワールド座標を取得
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(myTouches[i].position);

                // 自分手裏剣の座標にタッチ座標を設定する
                this.transform.position = touchPosition;

                // 自分手裏剣の座標を更新したのでShuriken1Positionを更新
                Shuriken1Position = this.transform.position;
            }
        }
        else
        {
            this.transform.position = shuriken1IniPos;
        }
    }
}