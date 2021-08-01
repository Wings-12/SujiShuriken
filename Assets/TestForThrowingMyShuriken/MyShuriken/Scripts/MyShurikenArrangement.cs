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
    Vector2 shuriken1Position;
    /// <summary>
    ///// 自分手裏剣1の座標のプロパティ
    ///// </summary>
    public Vector2 Shuriken1Position
    {
        get
        {
            return this.shuriken1Position;
        }
        set
        {
            this.shuriken1Position = value;
        }
    }

    ///<summary>
        ///MyArea_Imageのワールド座標の幅
        ///</summary>
    public static float world_MyArea_ImageWidth;

    ///<summary>
    ///MyArea_Imageのワールド座標の高さ
    ///</summary>
    public static float world_MyArea_ImageHeight;

    private void Start()
    {
        this.Shuriken1Position = this.transform.position;
    }

    /// <summary>
    /// 自エリア(MyArea_Panel)内で自分手裏剣をドラッグで移動する
    /// </summary>
    public void OnMyShurikenArranging()
    {
        // 時間測定開始

        // ↓の処理にif文でMyArea_Panelの4隅の座標以内であれば処理をするように作る

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

    /// <summary>
        /// 機能：world_playerWidthにワールド座標でuGUIのPlayer(Image)の幅を設定する。
        ///
        /// 引数：なし
        ///
        /// 戻り値：なし
        ///
        /// 備考：参考サイト：
        /// ①＜ワールド座標とローカル座標の変換＞：https://dkrevel.com/unity-explain/space/
        /// ②【Unity】GameObjectの幅と高さを取得・変更する方法（RectTransform）：https://techno-monkey.hateblo.jp/entry/2018/05/12/150845
        /// ③RectTransformからワールド座標に変換する方法：http://alien-program.hatenablog.com/entry/2017/08/06/164258
        /// </summary>
    private static void Set_world_playerWidth()
    {
        // ワールド座標の幅を取得したいimageゲームオブジェクト(Player)を取得する
        GameObject player = GameObject.Find("Player");

        // PlayerのRectTransformを取得する
        RectTransform playerRectTransform = (RectTransform)player.transform;

        // Playerの幅(Width)を取得するローカル変数
        Vector3 playerWidth = Vector3.zero;

        // Playerの幅を取得する
        playerWidth = new Vector3(player.GetComponent<RectTransform>().sizeDelta.x, 0.0f, 0.0f);

        // 親のゲームオブジェクト(Canvas)
        GameObject canvas = GameObject.Find("Canvas");

        // ローカル座標からワールド座標に変換
        Vector3 temp_world_playerWidth = canvas.transform.TransformPoint(playerWidth);

        // メンバ変数のworld_playerWidthを設定
        world_MyArea_ImageWidth = temp_world_playerWidth.x;
    }

    /// <summary>
    /// 機能：world_playerHeightにワールド座標でuGUIのPlayer(Image)の高さを設定する。
    ///
    /// 引数：なし
    ///
    /// 戻り値：なし
    ///
    /// 備考：Set_world_playerWidthを参考に作成した。
    /// </summary>
    private static void Set_world_playerHeight()
    {
        // ワールド座標の高さを取得したいimageゲームオブジェクト(Player)を取得する
        GameObject player = GameObject.Find("Player");

        // PlayerのRectTransformを取得する
        RectTransform playerRectTransform = (RectTransform)player.transform;

        // Playerの高さ(Height)を取得するローカル変数
        Vector3 playerHeight = Vector3.zero;

        // Playerの高さを取得する
        playerHeight = new Vector3(0.0f, player.GetComponent<RectTransform>().sizeDelta.y, 0.0f);

        // 親のゲームオブジェクト(Canvas)
        GameObject canvas = GameObject.Find("Canvas");

        // ローカル座標からワールド座標に変換
        Vector3 temp_world_playerHeight = canvas.transform.TransformPoint(playerHeight);

        // メンバ変数のworld_playerHeightを設定
        world_MyArea_ImageHeight = temp_world_playerHeight.y;
    }
}
