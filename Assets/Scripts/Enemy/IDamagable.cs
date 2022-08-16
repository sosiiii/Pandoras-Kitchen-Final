using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void Damage(int attackDamage, Vector3 knockbackDir);

    Vector3 GetPosition();
}
