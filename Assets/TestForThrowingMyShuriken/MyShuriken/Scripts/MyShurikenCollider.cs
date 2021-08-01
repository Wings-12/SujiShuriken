using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyShurikenCollider : MonoBehaviour
{
    // バグ：Not Activeにすると物理挙動も止まってしまう。
    // 対策：レイヤーを変更して最初に衝突しないように作る。

    /// <summary>
    /// 機能：自分手裏剣が複製されたときに自分手裏剣に当たってフリックした方向に手裏剣がいかないことがないようにする
    /// </summary>
    /// <param name="collision">衝突したオブジェクト</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "MyShuriken1_Button")
        //{
        //    this.gameObject.SetActive(false);
        //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "MyShuriken1_Button")
        //{
        //    this.gameObject.SetActive(true);
        //}
    }
}
