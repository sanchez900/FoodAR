using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum Team
{
    Team_1,
    Team_2
}

public class CombatSystem : MonoBehaviour
{
    [SerializeField] private List<Combatant> m_combatantList = new List<Combatant>();
    [SerializeField] private List<CombatantUI> m_combatantUIList = new List<CombatantUI>();

    private Combatant m_currentPlayingCombatant;
    private bool bProcessingRoundCheck = false;

    void Start()
    {
        m_currentPlayingCombatant = m_combatantList[Random.Range(0, m_combatantList.Count)];
        ProcessCurrentRound();
    }

    void Update()
    {
        if (!bProcessingRoundCheck)
        {
            if (m_currentPlayingCombatant != null)
            {
                if (m_currentPlayingCombatant.CombatantStance != CombatantStance.None)
                {
                    m_currentPlayingCombatant.ProcessCombatantStances(m_combatantList.Where(c => !c.GetComponent<ActorDataManager>().IsDead && c != m_currentPlayingCombatant).FirstOrDefault());
                    bProcessingRoundCheck = true;
                    CheckRoundCompletion();
                }
            }
        }
    }

    void CheckRoundCompletion()
    {
        if (m_combatantList.Count > 1)
        {
            Combatant m_nextCombatant = m_combatantList.Where(c => c.CombatantStance == CombatantStance.None && !c.GetComponent<ActorDataManager>().IsDead).FirstOrDefault();

            if (m_nextCombatant && m_nextCombatant != m_currentPlayingCombatant)
            {
                m_currentPlayingCombatant = m_nextCombatant;
                ProcessCurrentRound();
                bProcessingRoundCheck = false;
                return;
            }

            else if (!m_nextCombatant)
            {
                ProcessNextRound();
                m_nextCombatant = m_combatantList.Where(c => c.CombatantStance == CombatantStance.None && !c.GetComponent<ActorDataManager>().IsDead).FirstOrDefault();

                if (m_nextCombatant && m_nextCombatant != m_currentPlayingCombatant)
                {
                    m_currentPlayingCombatant = m_nextCombatant;
                    ProcessCurrentRound();
                    bProcessingRoundCheck = false;
                    return;
                }
            }
        }

        Debug.Log("Game Completed! " + m_currentPlayingCombatant.Name + " has won!");

        // Write here if you need something else when there is only 1 combatant left.

        // End ====================================================================
    }

    void ProcessCurrentRound()
    {
        for (int i = 0; i < m_combatantUIList.Count; i++)
        {
            m_combatantUIList[i].gameObject.SetActive(m_combatantUIList[i].AssignedCombatant == m_currentPlayingCombatant);
        }

        // Write here if you need something else to process for the current round.

        // End ====================================================================
    }

    void ProcessNextRound()
    {
        for (int i = 0; i < m_combatantList.Count; i++)
        {
            m_combatantList[i].AssignCombatantStance(CombatantStance.None);
        }

        // Write here if you need something else to process for the next round.

        // End ====================================================================

    }
}
