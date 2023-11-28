using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    
    [SerializeField] private Menu[] menus;

    public void Awake()
    {
        Instance = this;
    }

    public void OpenMenu(string menu)
    {
        foreach (var m in menus)
        {
            if (m.menuName == menu)
            {
                OpenMenu(m);
            }
        }
    }

    public void OpenMenu(Menu menu)
    {
        foreach (var m in menus)
        {
            CloseMenu(m);
        }
        menu.Open();
    }
    
    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }
}
