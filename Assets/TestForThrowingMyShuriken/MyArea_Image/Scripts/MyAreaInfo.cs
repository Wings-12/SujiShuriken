using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MyArea_Imageゲームオブジェクトの幅と高さを外部で使えるようにするクラス
/// </summary>
public static class MyAreaInfo
{
    public static float width = 0.0f;
    public static float height = 0.0f;

    static MyAreaInfo()
    {
        SetWidth();
        SetHeight();
    }

    static void SetWidth()
    {
        GameObject myArea_Image = GameObject.Find(GameObjectName.myArea_Image);

        // MyArea_Imageの幅(Width)を取得するローカル変数
        Vector3 myArea_ImageLocalWidth = Vector3.zero;

        // MyArea_Imageの幅を取得する
        // ※Anchorの設定がstretchの際に正常に値が取得できない
        // 参考：https://tofu-doon.hatenablog.com/entry/recttransform-width-height
        myArea_ImageLocalWidth = new Vector3(myArea_Image.GetComponent<RectTransform>().sizeDelta.x, 0.0f, 0.0f);

        // 親のゲームオブジェクト(Canvas)
        GameObject canvas = GameObject.Find("Canvas");

        // ローカル座標からワールド座標に変換
        Vector3 temp_world_myArea_ImageHeight = canvas.transform.TransformPoint(myArea_ImageLocalWidth);

        // メンバ変数のwidthを設定
        width = temp_world_myArea_ImageHeight.x;
    }

    static void SetHeight()
    {
        GameObject myArea_Image = GameObject.Find(GameObjectName.myArea_Image);

        // MyArea_Imageの高さ(Height)を取得するローカル変数
        Vector3 myAreaImageLocalHeight = Vector3.zero;

        // MyArea_Imageの高さを取得する
        // ※Anchorの設定がstretchの際に正常に値が取得できない
        // 参考：https://tofu-doon.hatenablog.com/entry/recttransform-width-height
        myAreaImageLocalHeight = new Vector3(0.0f, myArea_Image.GetComponent<RectTransform>().sizeDelta.y, 0.0f);

        // 親のゲームオブジェクト(Canvas)
        GameObject canvas = GameObject.Find("Canvas");

        // ローカル座標からワールド座標に変換
        Vector3 temp_world_myArea_ImageHeight = canvas.transform.TransformPoint(myAreaImageLocalHeight);

        // メンバ変数のheightを設定
        height = temp_world_myArea_ImageHeight.y;
    }
}
