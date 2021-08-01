using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestForInstantiate : MonoBehaviour
{
    [SerializeField] GameObject button = default;

    [SerializeField] GameObject canvas = default;

    GameObject test;

    // Start is called before the first frame update
    void Start()
    {
        test = Instantiate(this.button, this.button.transform.position + new Vector3(0.0f, -0.2f, 0.0f), Quaternion.identity);

        test.transform.SetParent(this.canvas.transform, false);

        test.transform.position += new Vector3(0.0f, -0.2f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
