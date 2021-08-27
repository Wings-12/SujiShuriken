using System.Collections;
using UnityEngine;

/// <summary>
/// 100からカウントダウンし値を通知するサンプル
/// </summary>
public class TimeCounter : MonoBehaviour
{
    public delegate void TimerEventHandler(int time);
    public event TimerEventHandler OnTimeChanged;

    public delegate void Timer0EventHandler();
    public event Timer0EventHandler OnTimeIs0;

    void Start()
    {
        //タイマ起動
        StartCoroutine(TimerCoroutine());
    }

    IEnumerator TimerCoroutine()
    {
        var time = 60;
        while (time >= 0)
        {
            //イベント通知
            OnTimeChanged(time);

            //1秒待つ
            yield return new WaitForSeconds(1);

            time--;

            if (time == 0)
            {
                OnTimeIs0();
            }

        }
    }
}