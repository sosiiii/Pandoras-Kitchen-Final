using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void Damage(float attackDemage, Vector3 knockbackDir);

    Vector3 GetPosition();
}
