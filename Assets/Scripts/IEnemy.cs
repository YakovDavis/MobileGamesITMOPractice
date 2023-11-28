using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    public void TakeDamage(int damage);
    public void DestroyEnemy();
}
