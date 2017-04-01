using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private string m_name;
    private string m_itemID;

    public string Name { get { return m_name; } }
    public string ItemID { get { return m_itemID; } }

    public virtual void Init(string p_name, string p_itemID)
    {
        this.m_name = p_name;
        this.m_itemID = p_itemID;
    }

    public virtual void UseItem() { }
}
