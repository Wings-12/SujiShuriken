using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndStatementManager : MonoBehaviour
{
    //それぞれインスタンスはインスペクタビューから設定

    [SerializeField] private TimeCounter timeCounter;
    [SerializeField] private GameObject endStatementText;

    void Start()
    {
        timeCounter.OnTimeIs0 += () =>
        {
            endStatementText.SetActive(true);
        };
    }
}
