/* AR-Based Surgery on the Human Cadaver
   Copyright (C) 2022 Mark Cesov and Behrus Puladi

   This program is free software; you can redistribute it and/or
   modify it under the terms of the GNU General Public License
   as published by the Free Software Foundation; either version 2
   of the License, or (at your option) any later version.
   
   This program is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   GNU General Public License for more details.
   
   You should have received a copy of the GNU General Public License
   along with this program; if not, write to the Free Software
   Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject casesMenu;
    public GameObject helpMenu;

    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    private void Awake()
    {
        GoToMainMenu();
    }

    private void OnDisable()
    {
        GoToMainMenu();
    }

    public void GoToMainMenu()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        casesMenu.SetActive(false);
        helpMenu.SetActive(false);
    }

    public void GoToSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
        casesMenu.SetActive(false);
        helpMenu.SetActive(false);
    }

    public void GoToCases()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        casesMenu.SetActive(true);
        helpMenu.SetActive(false);
    }

    public void GoToHelp()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        casesMenu.SetActive(false);
        helpMenu.SetActive(true);
    }
}
