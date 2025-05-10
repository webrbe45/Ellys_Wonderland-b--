using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SubMenu : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if(MenuSet.activeSelf)
                MenuSet.SetActive(false);
            else
                MenuSet.SetActive(true);
        }
           
    }

    public GameObject MenuSet;
}
