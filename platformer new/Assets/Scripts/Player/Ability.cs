using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    public string Name;
    public LayerMask EnemyLayer;
    public float ActiveTime;
    public float CooldownTime;

    public virtual void Activate(GameObject parrent) {}
    public virtual void BeginCoolDonw(GameObject parrent) { }
}
