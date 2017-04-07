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
    [SerializeField] private string m_name;
    [SerializeField] private Team m_team; // Teams not used for Demo.
    [SerializeField] private int m_maxSpecialLevel = 4;
    private int m_specialLevel;
    private float m_maxSpecialPoints;
    private float m_curSpecialPoints;

    private CombatantStance m_combatantStance = CombatantStance.None;

    public string Name { get { return m_name; } }
    public Team Team { get { return m_team; } }
    public int SpecialLevel { get { return m_specialLevel; } }
    public float MaxSpecialPoints { get { return m_maxSpecialPoints; } }
    public float CurrentSpecialPoints { get { return m_curSpecialPoints; } }
    public float SpecialPointsRatio { get { return m_curSpecialPoints / m_maxSpecialPoints; } }

    public CombatantStance CombatantStance
    {
        get { return m_combatantStance; }
    }

    void Start()
    {
        this.m_maxSpecialPoints = 20;
    }

    public void AddSpecialPoints(float p_value)
    {
        m_curSpecialPoints += p_value;

        /*if (m_specialLevel < m_maxSpecialLevel)
        {
            while (m_curSpecialPoints > MaxSpecialPoints)
            {
                m_curSpecialPoints -= MaxSpecialPoints;
                m_specialLevel++;
            }
        }*/

        Debug.Log(this.Name + " has gained " + p_value + " Special Points.");

        m_specialLevel = Mathf.Clamp(m_specialLevel, 0, m_maxSpecialLevel);
        m_curSpecialPoints = Mathf.Clamp(m_curSpecialPoints, 0, m_maxSpecialPoints);
    }

    public void AssignCombatantStance(CombatantStance p_stance)
    {
        m_combatantStance = p_stance;
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

        Debug.Log(this.Name + " dealt " + damage + " damage to " + p_target.Name + ".");
    }

    protected virtual void DefenseStance(Combatant p_target)
    {

        Debug.Log(this.Name + " has taken defensive stance.");
    }

    protected virtual void SpecialStance(Combatant p_target)
    {
        int damage = Constants.SpecialDamageValue[m_specialLevel];

        if (p_target.CombatantStance == CombatantStance.Defend)
        {
            damage = Mathf.RoundToInt(damage / 3.0f);
        }

        p_target.GetComponent<ActorDataManager>().ModifyHealth(-damage);

        m_specialLevel = 0;
        m_curSpecialPoints = 0;

        Debug.Log(this.Name + " has used Special and dealt " + damage + " damage to " + p_target.Name + ".");
    }
}
