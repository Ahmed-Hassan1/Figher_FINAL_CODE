using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField]private float current_hp = 100f;
    // Start is called before the first frame update

    public void ReduceHP(float damage)
    {
        current_hp -= damage;
        if(current_hp<=0)
        {
            current_hp = 0;
        }
    }

    public bool isDead()
    {
        return current_hp <= 0;
    }

    public float GetCurrentHealth()
    {
        return current_hp;
    }
}
