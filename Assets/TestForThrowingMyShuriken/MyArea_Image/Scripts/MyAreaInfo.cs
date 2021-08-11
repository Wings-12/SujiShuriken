using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

/// <summary>
/// MyArea_Imageゲームオブジェクトの幅と高さを外部で使えるようにするクラス
/// </summary>
public static class MyAreaInfo
{
    public static Vector3 topLeftPos = Vector3.zero;
    public static Vector3 bottomRightPos = Vector3.zero;

    public static float width = 0.0f;
    public static float height = 0.0f;

    static MyAreaInfo()
    {
        SetWidth();
        SetHeight();

        SetBottomRightPos();
        SetTopLeftPos();
    }

    static void SetTopLeftPos()
    {
        GameObject myArea_Image = GameObject.Find(GameObjectName.myArea_Image);

        // topLeftPosX = (MyAreaのposX - (MyAreaの幅 / 2))
        float topLeftPosX = myArea_Image.transform.position.x - (width / 2);

        // topLeftPosY = (MyAreaのposY + (MyAreaの高さ / 2))
        float topLeftPosY = myArea_Image.transform.position.y + (height / 2);

        topLeftPos = new Vector3(topLeftPosX, topLeftPosY, 0.0f);
    }

    static void SetBottomRightPos()
    {
        GameObject myArea_Image = GameObject.Find(GameObjectName.myArea_Image);

        // bottomRightPosX = (MyAreaのposX + (MyAreaの幅 / 2))
        float bottomRightPosX = myArea_Image.transform.position.x + (width / 2);

        // BottomRightPosY = (MyAreaのposY - (MyAreaの高さ / 2))
        float bottomRightPosY = myArea_Image.transform.position.y - (height / 2);

        bottomRightPos = new Vector3(bottomRightPosX, bottomRightPosY, 0.0f);
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
