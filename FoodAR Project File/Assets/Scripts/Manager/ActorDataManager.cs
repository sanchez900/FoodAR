using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorDataManager : MonoBehaviour
{
    [SerializeField] private int m_maxHealth = 100;
    private int m_curHealth;

    [SerializeField] private int m_normalDamage = 3;

    public int MaxHealth { get { return m_maxHealth; } }
    public int CurrentHealth { get { return m_curHealth; } }
    public float HealthRatio { get { return m_curHealth / m_maxHealth; } }
    public bool IsDead { get { return m_curHealth <= 0; } }

    public int NormalDamage { get { return m_normalDamage; } }
    
    void Start()
    {
        this.m_curHealth = m_maxHealth;
    }

    public void ModifyHealth(int p_value)
    {
        this.m_curHealth += p_value;
        this.m_curHealth = Mathf.Clamp(this.m_curHealth, 0, m_maxHealth);
    }
}

/*turn based game ,that has 3 buttons , attack , defense , special.
there will be health gauge , it will just be the current health and max health that will 
be shown as just numbers and not health gauges.

there will be another stat which is the special gauge , when the monster attack , or is attacked the gauge rises , 
it will have 4 level , for each level the special attack will be more powerful , and when clicked the special gauge will return back to 0*/
