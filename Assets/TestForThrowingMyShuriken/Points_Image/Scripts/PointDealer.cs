using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointDealer : MonoBehaviour
{
    [SerializeField]
    private GenerateEnemyShuriken generateEnemyShuriken = default;

    public int currentPoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        generateEnemyShuriken.OnPointGotten += gottenPoint =>
        {
            currentPoint += gottenPoint;
            this.gameObject.GetComponentInChildren<Text>().text = currentPoint.ToString();
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
