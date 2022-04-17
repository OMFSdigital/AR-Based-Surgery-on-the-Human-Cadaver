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

using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StepInfoMenu : MonoBehaviour
{
    public TextMeshPro currentStepNumber;
    public TextMeshPro currentStepInfoText;

    public Interactable nextStepButton;
    public Interactable previousStepButton;

    public Material enabledMat;
    public Material disabledMat;

    public GameObject previousBackPlate;
    public GameObject nextBackPlate;

    private void OnEnable()
    {
        UpdateStepInfoTexts();
    }

    public void UpdateStepInfoTexts()
    {
        CaseController cc = GameManager.instance.GetComponent<CaseController>();
        currentStepNumber.text = cc.currentStep.ToString();
        currentStepInfoText.text = cc.currentCase.steps[cc.currentStep];

        if (!cc.HasNextStep())
        {
            nextStepButton.IsEnabled = false;
            nextBackPlate.GetComponent<Renderer>().material = disabledMat;
        } else
        {
            nextStepButton.IsEnabled = true;
            nextBackPlate.GetComponent<Renderer>().material = enabledMat;
        }

        if (!cc.HasPreviousStep())
        {
            previousStepButton.IsEnabled = false;
            previousBackPlate.GetComponent<Renderer>().material = disabledMat;
        } else
        {
            previousStepButton.IsEnabled = true;
            previousBackPlate.GetComponent<Renderer>().material = enabledMat;
        }
    }

    public void NextStep()
    {
        CaseController cc = GameManager.instance.GetComponent<CaseController>();
        cc.NextStep();
        UpdateStepInfoTexts();
    }

    public void PreviousStep()
    {
        CaseController cc = GameManager.instance.GetComponent<CaseController>();
        cc.PreviousStep();
        UpdateStepInfoTexts();
    }
}
