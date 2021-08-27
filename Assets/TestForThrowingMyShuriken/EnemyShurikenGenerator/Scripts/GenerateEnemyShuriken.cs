﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

/// <summary>
/// 敵手裏剣を自エリアに一定間隔で生成するクラス
/// </summary>
public class GenerateEnemyShuriken : MonoBehaviour
{
    #region フィールド
    /// <summary>
    /// 敵手裏剣時にWaitForSecondsで待機処理がされているかどうか判定するbool変数
    /// </summary>
    /// <remarks>
    /// 用途：true：WaitForSecondsで待機処理がされている / false：されていない
    /// 意図：Enemyに遠距離攻撃が当たっても連射できないようにCoroutineFireBallメソッドを停止するために追加</remarks>
    bool isIntervalForThrowingEnemyShuriken;

    /// <summary>
    /// 敵手裏剣のゲームオブジェクト
    /// </summary>
    GameObject instantiatedEnemyShuriken = default;

    /// <summary>
    /// インスタンシエイト元の敵手裏剣のゲームオブジェクト
    /// </summary>
    [SerializeField] GameObject original_enemyShuriken = default;

    /// <summary>
    /// インスタンシエイトした敵手裏剣の親ゲームオブジェクト
    /// </summary>
    [SerializeField] GameObject canvas = default;

    [SerializeField] Text figureText = default;

    private Vector2 initialEnemyShuriken = default;

    public delegate void PointEventHandler(int gottenPoint);
    public event PointEventHandler OnPointGotten;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // 以下初期化
        isIntervalForThrowingEnemyShuriken = false;

        initialEnemyShuriken = new Vector2(0.0f, 6.0f);
    }

    // Update is called once per frame
    void Update()
    {
        StartThrowEnemyShuriken();
    }

    /// <summary>
    /// 敵手裏剣の処理を開始する
    /// </summary>
    public void StartThrowEnemyShuriken()
    {
        // 敵手裏剣のインターバルでないかつ敵手裏剣が生成されていない場合
        if (!this.isIntervalForThrowingEnemyShuriken)
        {
            // 敵手裏剣の処理開始
            StartCoroutine(CoroutineThrowEnemyShuriken());
        }
    }

    /// <summary>
    /// 敵手裏剣のメイン処理
    /// </summary>
    IEnumerator CoroutineThrowEnemyShuriken()
    {
        // 敵手裏剣を複製する
        this.instantiatedEnemyShuriken = Instantiate(
            this.original_enemyShuriken,
            initialEnemyShuriken,
            Quaternion.identity);



        // 生成した敵手裏剣の親をキャンバスにし、座標を発射座標に設定する
        // 理由：インスタンシエイトしたUIは親をキャンバスに設定しないと表示されず、
        // また、発射座標を再設定しないと、発射座標から敵手裏剣が飛ばなくなるから
        this.instantiatedEnemyShuriken.transform.SetParent(canvas.transform, false);
        this.instantiatedEnemyShuriken.transform.position = initialEnemyShuriken;

        // インスタンシエイトされた敵手裏剣を自分手裏剣よりも下に描画する
        // ※Indexを1にしているのは、自エリアよりも上に描画するため
        this.instantiatedEnemyShuriken.transform.SetSiblingIndex(1);

        // 敵手裏剣の数字をランダムに設定
        int randomNum = (int)Math.Floor(Random.Range(1.0f, 9.0f));
        this.figureText.text = randomNum.ToString();

        int originalHp = int.Parse(instantiatedEnemyShuriken.GetComponentInChildren<Text>().text);

        instantiatedEnemyShuriken.GetComponent<OriginalFigureHolder>().maxHp = originalHp;

        this.instantiatedEnemyShuriken.GetComponent<EnemyShurikenDamageTaker>().OnShurikenHp0 += enemyShuriken =>
        {
            // 敵手裏剣を破壊した時にもともとのHPぶん得点する
            int gottenPoint = enemyShuriken.GetComponent<OriginalFigureHolder>().maxHp;
            OnPointGotten(gottenPoint);

            // インスタンシエントされた手裏剣のHPが0になった時に手裏剣を破棄登録
            Destroy(enemyShuriken);
        };

        // 敵手裏剣へ瞬間的に力を加えて右方向に飛ばす
        Vector2 force = new Vector2(Random.Range(-0.5f, 0.5f), -1.0f);
        // https://xr-hub.com/archives/10747 調整したX座標最小・最大値をRandom.Rangeで設定する
        this.instantiatedEnemyShuriken.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);

        // CoroutineFireBallの処理をこれ以上呼べないようにする
        // 理由：すぐ下のWaitForSeconds処理がされている間はまたCoroutineFireBallが呼ばれて敵手裏剣を連射できないようにするため
        this.isIntervalForThrowingEnemyShuriken = true;

        // CoroutineFireBall()の処理を指定秒数止める
        // 理由：敵手裏剣を高速で連射できないようにするため
        yield return new WaitForSeconds(1.0f);

        // WaitForSeconds処理が終わったら次の敵手裏剣が撃てるようにする
        this.isIntervalForThrowingEnemyShuriken = false;
    }
}
