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

public class SettingsMenu : MonoBehaviour
{
    public GameObject navigationMenu;
    public GameObject calibrationMenu;
    public GameObject drawMenu;
    public GameObject colorChangeMenu;
    public GameObject soundFeedbackMenu;
    public GameObject angleCorrectionMenu;

    public GameObject backToNagivationMenuButton;

    private void Start()
    {
        GoToNavigation();
    }

    private void OnDisable()
    {
        GoToNavigation();
    }

    public void GoToNavigation()
    {
        DisableAllMenus();
        navigationMenu.SetActive(true);

        backToNagivationMenuButton.SetActive(false);
    }

    public void GoToCalibration()
    {
        DisableAllMenus();
        calibrationMenu.SetActive(true);
    }
    public void GoToDraw()
    {
        DisableAllMenus();
        drawMenu.SetActive(true);
    }
    public void GoToColorChange()
    {
        DisableAllMenus();
        colorChangeMenu.SetActive(true);
    }
    public void GoToSoundFeedback()
    {
        DisableAllMenus();
        soundFeedbackMenu.SetActive(true);
    }
    public void GoToAngleCorrection()
    {
        DisableAllMenus();
        angleCorrectionMenu.SetActive(true);
    }

    private void DisableAllMenus()
    {
        navigationMenu.SetActive(false);
        calibrationMenu.SetActive(false);
        drawMenu.SetActive(false);
        colorChangeMenu.SetActive(false);
        soundFeedbackMenu.SetActive(false);
        angleCorrectionMenu.SetActive(false);

        backToNagivationMenuButton.SetActive(true);
    }
}
