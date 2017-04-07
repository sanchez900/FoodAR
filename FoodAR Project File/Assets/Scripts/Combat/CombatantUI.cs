using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatantUI : MonoBehaviour
{
    [SerializeField] private Combatant m_assignedCombatant;
    [SerializeField] private Text m_nameText;
    [SerializeField] private Text m_healthText;
    [SerializeField] private Button m_specialButton;
    [SerializeField] private Image m_specialGaugeBar;

    private ActorDataManager m_actorDataManager;

    public Combatant AssignedCombatant { get { return m_assignedCombatant; } }

    void Awake()
    {
        m_nameText.text = m_assignedCombatant.Name;
        m_actorDataManager = m_assignedCombatant.GetComponent<ActorDataManager>();
    }

    void OnEnable()
    {
        m_healthText.text = "Health: " + m_actorDataManager.CurrentHealth + "/" + m_actorDataManager.MaxHealth;
        m_specialButton.interactable = m_assignedCombatant.CurrentSpecialPoints >= m_assignedCombatant.MaxSpecialPoints;
        m_specialGaugeBar.fillAmount = m_assignedCombatant.SpecialPointsRatio;
    }

    void Update()
    {
        m_healthText.text = "Health: " + m_actorDataManager.CurrentHealth + "/" + m_actorDataManager.MaxHealth;
        m_specialButton.interactable = m_assignedCombatant.CurrentSpecialPoints >= m_assignedCombatant.MaxSpecialPoints;
        m_specialGaugeBar.fillAmount = m_assignedCombatant.SpecialPointsRatio;
    }

    public void AssignCombatantStance(int p_stance)
    {
        m_assignedCombatant.AssignCombatantStance((CombatantStance)p_stance);
    }
}
