using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class backgroundScript : MonoBehaviour
{
    //Attach this script to a Dropdown GameObject
    Dropdown m_Dropdown;
    //This is the string that stores the current selection m_Text of the Dropdown
    string m_thecolor;
    int m_DropdownValue;

    void Start()
    {
        //Fetch the DropDown component from the GameObject
        m_Dropdown = GetComponent<Dropdown>();
        m_Dropdown.value = m_DropdownValue;
        //Output the first Dropdown index value
        
    }

    void Update()
    {


        
        m_DropdownValue = m_Dropdown.value;
        m_thecolor = m_Dropdown.options[m_DropdownValue].text;
        PlayerPrefs.DeleteKey("backgroundColor");
        PlayerPrefs.SetString("backgroundColor", m_thecolor);
        Debug.Log("PlayerPrefs was set to: " + m_thecolor);

        
    }
}
