using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MyShurikenThrower : MonoBehaviour
{
    #region フィールド
    /// <summary>
    /// 自分手裏剣タッチ開始時の座標
    /// </summary>
    Vector2 touchBeganPosition;

    /// <summary>
    /// 自分手裏剣タッチ終了時の座標
    /// </summary>
    Vector2 touchEndedPosition;

    /// <summary>
    /// フリック判定するためのタッチ開始時間
    /// </summary>
    float touchBeganTime;

    /// <summary>
    /// フリック判定するためのタッチ終了時間
    /// </summary>
    float touchEndedTime;

    ///<summary>
    ///手裏剣のrigidbody
    ///</summary>
    new Rigidbody2D rigidbody2D;

    /// <summary>
    /// 自分手裏剣が投げられたときのイベント
    /// </summary>
    public event EventHandler ThrewShuriken;

    #endregion

    [SerializeField] TouchEventDisplayer touchEventDisplayer = default;

    private void Start()
    {
        this.rigidbody2D = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// 自分手裏剣野中にタッチが入ったら自分手裏剣を投げる判定準備をするイベントハンドラ
    /// </summary>
    private void OnMouseEnter()
    {
        touchEventDisplayer.DisplayWhichEventCalled("コライダに入った");

        // 自分手裏剣をタッチし始めたときの座標を取得する
        // 理由：この座標と自分手裏剣のタッチ終了した座標を使って自分手裏剣を投げる方向を演算するため
        Set_touchBeganPosition();
    }

    /// <summary>
    /// 自分手裏剣をタッチし始めたときの座標を取得する
    /// </summary>
    private void Set_touchBeganPosition()
    {
        Touch[] myTouches = Input.touches;

        for (int i = 0; i < myTouches.Length; i++)
        {
            // タッチ開始座標取得
            // 理由：タッチ距離測定準備のため　→　タッチ距離によってフリック判定するため
            this.touchBeganPosition = Camera.main.ScreenToWorldPoint(myTouches[i].position);

            // タッチ開始時間測定
            // 理由：測定時間によってフリック判定するため
            this.touchBeganTime = Time.time;
        }
    }

    private void OnMouseDown()
    {
        Set_touchBeganPosition();
    }

    //---------------------------OnIfShurikenThrownを2回呼んでしまわないように修正中--------------------------------------------
    bool isOnIfShurikenThrown = false;

    /// <summary>
    /// フリックされたらフリック方向に手裏剣を投げるPointerExitイベントハンドラ
    /// </summary>
    private void OnMouseExit()
    {
        // touchphase.Movedの場合にタッチ終了座標更新
        // 理由：PointerExitで手裏剣のタッチが終わる直前のタッチ座標を取得したいから
        //Touch[] myTouches = Input.touches;

        //for (int i = 0; i < myTouches.Length; i++)
        //{
        //    this.touchEndedPosition = Camera.main.ScreenToWorldPoint(myTouches[i].position);
        //}

        //Debug.Log("コライダ外に離れた");

        //if (isOnIfShurikenThrown == false)
        //{
        //    JudgeIfThrowingShuriken();

        //    isOnIfShurikenThrown = true;
        //}
        //else
        //{
        //    isOnIfShurikenThrown = false;
        //}
    }

    private void OnMouseUp()
    {
        // touchphase.Movedの場合にタッチ終了座標更新
        // 理由：PointerExitで手裏剣のタッチが終わる直前のタッチ座標を取得したいから
        Touch[] myTouches = Input.touches;

        for (int i = 0; i < myTouches.Length; i++)
        {
            this.touchEndedPosition = Camera.main.ScreenToWorldPoint(myTouches[i].position);
        }

        JudgeIfThrowingShuriken();
    }

    private void OnMouseUpAsButton()
    {
        //// touchphase.Movedの場合にタッチ終了座標更新
        //// 理由：PointerExitで手裏剣のタッチが終わる直前のタッチ座標を取得したいから
        //Touch[] myTouches = Input.touches;

        //for (int i = 0; i < myTouches.Length; i++)
        //{
        //    this.touchEndedPosition = Camera.main.ScreenToWorldPoint(myTouches[i].position);
        //}

        //Debug.Log("コライダ内で離れた");

        //if (isOnIfShurikenThrown == false)
        //{
        //    JudgeIfThrowingShuriken();

        //    isOnIfShurikenThrown = true;
        //}
        //else
        //{
        //    isOnIfShurikenThrown = false;
        //}
    }
    //---------------------------OnIfShurikenThrownを2回呼んでしまわないようにバグ修正中--------------------------------------------

    private void JudgeIfThrowingShuriken()
    {
        // タッチ距離取得
        // 理由：フリック距離判定のため
        float touchDistance = Vector2.Distance(this.touchEndedPosition, this.touchBeganPosition);
        //Debug.Log("タッチ距離：" + touchDistance);

        // タッチ終了時間取得
        this.touchEndedTime = Time.time;

        // タッチ時間取得
        // 理由：フリック時間判定のため
        float touchingTime = this.touchEndedTime - this.touchBeganTime;
        //Debug.Log("タッチ時間：" + touchingTime);

        // ①タッチ距離が0.2以上0.4以下でかつ
        // ②タッチ時間がの場合、フリックと判定する
        // ※①、②ともどれくらいの値がよいか調整中。
        //if (0.1f <= touchDistance && touchDistance <= 4.5f
        //    &&
        //    0.01f <= touchingTime && touchingTime <= 0.5f)
        if (0.1f <= touchDistance && touchDistance <= 4.5f)
        {
            // フリックした方向の角度を求める

            // 問題：思った方向に手裏剣が飛ばない
            // 推測される原因：オブジェクトからPointerExitしたときにやや右側からExitしているため。

            float flickedDirection = GetFlickedDirection(this.touchBeganPosition, this.touchEndedPosition);
            //float flickedDirection = GetFlickedDestination(this.touchBeganPosition, this.touchEndedPosition);

            // フリック方向が60°から90°であった場合は90°(前方向)にフリックしたと判定する
            //if (60.0f <= flickedDirection && flickedDirection <= 90.0f)
            //{
            //    flickedDirection = 90.0f;
            //}

            if (0 <= flickedDirection && flickedDirection <= 180)
            {
                ThrowShuriken(flickedDirection, 20.0f); 
            }
        }
    }

    /// <summary>
    /// 機能：現在のゲームオブジェクト座標から移動先座標への角度を求める
    /// </summary>
    /// <param name="touchBeganPosition">タッチ開始座標</param>
    /// <param name="touchEndedPosition">タッチ終了座標</param>
    /// <returns>現在のタッチ開始座標からタッチ終了座標への方向</returns>
    /// <remarks>
    /// 備考：参考サイト：https://qiita.com/2dgames_jp/items/60274efb7b90fa6f986a
    /// </remarks>
    public float GetFlickedDirection(Vector2 touchBeganPosition, Vector2 touchEndedPosition)
    {
        // 隣辺
        float adjacent = touchEndedPosition.x - touchBeganPosition.x;

        // 対辺
        float opposite = touchEndedPosition.y - touchBeganPosition.y;

        // 角度(ラジアン)
        float rad = Mathf.Atan2(opposite, adjacent);

        // 角度(°)
        return rad * Mathf.Rad2Deg;
    }

    /// <summary>
    /// 機能：ゲームオブジェクトを指定方向の指定スピードで動かす
    /// </summary>
    /// <param name="flickedDirection">現在のゲームオブジェクト座標から移動先座標への移動方向</param>
    /// <param name="speed">キャラクターの移動スピード</param>
    /// <remarks>備考：参考サイト：https://qiita.com/2dgames_jp/items/60274efb7b90fa6f986a</remarks>
    void ThrowShuriken(float flickedDirection, float speed)
    {
        // 移動先までの速度
        Vector2 velocityOfMoveDestination;

        // 移動先座標への角度におけるcosを設定
        velocityOfMoveDestination.x = Mathf.Cos(Mathf.Deg2Rad * flickedDirection) * speed;

        // 移動先座標への角度におけるsinを設定
        velocityOfMoveDestination.y = Mathf.Sin(Mathf.Deg2Rad * flickedDirection) * speed;

        // tan(斜辺の長さ(大きさ)とその向き == (cos(x座標), sin(y座標)))
        this.rigidbody2D.velocity = velocityOfMoveDestination;

        if (this.ThrewShuriken != null)
        {
            this.ThrewShuriken(this, EventArgs.Empty);
        }
    }
}
