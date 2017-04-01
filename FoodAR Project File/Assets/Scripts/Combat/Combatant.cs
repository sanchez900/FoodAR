using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CombatantStance
{
    None = -1,
    Attack,
    Defend,
    Special
}

public class Combatant : MonoBehaviour
{
    [SerializeField] private int m_maxSpecialLevel = 4;
    private int m_specialLevel;
    private float m_maxSpecialPoints;
    private float m_curSpecialPoints;

    private CombatantStance m_combatantStance;

    public int SpecialLevel { get { return m_specialLevel; } }
    public float MaxSpecialPoints { get { return m_maxSpecialPoints; } }
    public float CurrentSpecialPoints { get { return m_curSpecialPoints; } }
    public float SpecialPointsRatio { get { return m_curSpecialPoints / m_maxSpecialPoints; } }

    public CombatantStance CombatantStance
    {
        get { return m_combatantStance; }
        set { m_combatantStance = value; }
    }

    void Start()
    {
        this.m_maxSpecialPoints = 20;
        this.m_curSpecialPoints = m_maxSpecialPoints;
    }

    public void AddSpecialPoints(float p_value)
    {
        m_curSpecialPoints += p_value;

        if (m_specialLevel < m_maxSpecialLevel)
        {
            while (m_curSpecialPoints > MaxSpecialPoints)
            {
                m_curSpecialPoints -= MaxSpecialPoints;
                m_specialLevel++;
            }
        }

        m_specialLevel = Mathf.Clamp(m_specialLevel, 0, m_maxSpecialLevel);
        m_curSpecialPoints = Mathf.Clamp(m_curSpecialPoints, 0, m_maxSpecialPoints);
    }

    public virtual void ProcessCombatantStances(Combatant p_target)
    {
        switch (this.m_combatantStance)
        {
            case CombatantStance.Attack: AttackStance(p_target); break;
            case CombatantStance.Defend: DefenseStance(p_target); break;
            case CombatantStance.Special: SpecialStance(p_target); break;
        }
    }

    protected virtual void AttackStance(Combatant p_target)
    {
        int damage = this.GetComponent<ActorDataManager>().NormalDamage;

        if (p_target.CombatantStance == CombatantStance.Defend)
        {
            damage = Mathf.RoundToInt(damage / 3.0f);
        }

        p_target.GetComponent<ActorDataManager>().ModifyHealth(-damage);
        AddSpecialPoints(damage);
    }

    protected virtual void DefenseStance(Combatant p_target)
    {

    }

    protected virtual void SpecialStance(Combatant p_target)
    {
        m_specialLevel = 0;
        m_curSpecialPoints = 0;
    }
}

/*turn based game ,that has 3 buttons , attack , defense , special.
there will be health gauge , it will just be the current health and max health that will 
be shown as just numbers and not health gauges.

there will be another stat which is the special gauge , when the monster attack , or is attacked the gauge rises , 
it will have 4 level , for each level the special attack will be more powerful , and when clicked the special gauge will return back to 0*/
