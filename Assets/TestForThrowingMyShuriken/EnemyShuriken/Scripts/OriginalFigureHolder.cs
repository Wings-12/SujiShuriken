using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class OriginalFigureHolder : MonoBehaviour
{
    [FormerlySerializedAs("fullHp")]
    [FormerlySerializedAs("num")]
    public int maxHp = 0;
}
