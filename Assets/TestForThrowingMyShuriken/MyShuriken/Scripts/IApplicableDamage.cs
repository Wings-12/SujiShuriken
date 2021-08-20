using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IApplicableDamage
{
    /// <summary>
    /// 敵手裏剣にダメージを与える
    /// </summary>
    /// <returns></returns>
    int ApplyDamage();
}
