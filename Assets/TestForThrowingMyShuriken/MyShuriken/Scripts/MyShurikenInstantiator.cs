using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyShurikenInstantiator : MonoBehaviour
{
    /// <summary>
    /// 自分手裏剣1のゲームオブジェクト
    /// </summary>
    [SerializeField] GameObject myShuriken1;

    /// <summary>
    /// 自分手裏剣1のプレハブ
    /// </summary>
    GameObject myShuriken1_prefab;

    MyShurikenThrower myShurikenThrower;

    [SerializeField] GameObject canvas;

    [SerializeField] MyShurikenArrangement myShurikenArrangement;

    // Start is called before the first frame update
    private void Start()
    {
        this.myShurikenThrower = this.myShuriken1.GetComponent<MyShurikenThrower>();

        this.myShurikenThrower.ThrewShuriken += OnMyShuriken1Instantiated;
    }

    /// <summary>
    /// テスト
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <remarks>
    /// C#の命名規則に従うとThrewShurikenイベントのハンドラはOnThrewShuriken(On+<イベント名>)とすべきだが、
    /// 処理内容がわかりにくくなるので、以下とした。
    /// 参考：C# CODING GUIDELINES
    /// URL：https://qiita.com/Ted-HM/items/67eddbe36b88bf2d441d
    /// </remarks>
    void OnMyShuriken1Instantiated(object sender, EventArgs e)
    {
        //this.myShuriken1_prefab = Instantiate(this.myShuriken1, this.myShuriken1.transform.position, Quaternion.identity);
        this.myShuriken1_prefab = Instantiate(this.myShuriken1, this.myShurikenArrangement.Shuriken1Position, Quaternion.identity);

        // これでも原点に生成された。
        //this.myShuriken1_prefab = Instantiate(this.myShuriken1, new Vector2(-1.848f, -3.945f), Quaternion.identity);

        // ↓こいつのせい？？　SetParentしているときに座標に何か起きてる？

        // UIをInstantiateするときはcanvasをSetParentしないと期待しない座標でInstantiateされることがある模様。
        // 参考：https://qiita.com/tibe/items/5e1ae977c31cdbec1e60
        this.myShuriken1_prefab.transform.SetParent(this.canvas.transform, false);

        // SetParentのcanvas座標にInstantiateされているなら、これで座標修正できる？　→　できた
        this.myShuriken1_prefab.transform.position = this.myShurikenArrangement.Shuriken1Position;
    }
}
