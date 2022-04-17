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
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PatientCasesMenu : MonoBehaviour
{

    [SerializeField]
    private GameObject caseInfo;

    public SegmentsMenu segmentsMenu;

    private GameObject infoName, infoBirthday, infoGender, infoCaseNumber, infoFinding;


    private void Awake()
    {
        InitializeCaseInformation();
    }


    private void OnEnable()
    {
        UpdatePatientDataDisplay();
    }


    private void SetMenuText(GameObject item, Case currentCase)
    {
        TextMeshPro textMesh = item.transform.Find("IconAndText").Find("TextMeshPro").GetComponent<TextMeshPro>();
        textMesh.text = currentCase.caseInfo.patient + ", "
                        + currentCase.caseInfo.birthdate + ", "
                        + currentCase.caseInfo.gender + ", "
                        + "Finding: " + currentCase.caseInfo.finding;
    }


    private void DisplayCorrectView()
    {
        CaseController cc = GameManager.instance.GetComponent<CaseController>();

        bool isCaseLoaded = cc.currentCase != null;
        caseInfo.SetActive(isCaseLoaded);

    }


    private void UpdatePatientDataDisplay()
    {
        CaseController cc = GameManager.instance.GetComponent<CaseController>();

        infoName.GetComponent<TextMeshPro>().SetText(cc.currentCase.caseInfo.name);
        infoBirthday.GetComponent<TextMeshPro>().SetText(cc.currentCase.caseInfo.birthdate);
        infoGender.GetComponent<TextMeshPro>().SetText(cc.currentCase.caseInfo.gender);
        infoCaseNumber.GetComponent<TextMeshPro>().SetText(cc.currentCase.caseInfo.caseNumber);
        infoFinding.GetComponent<TextMeshPro>().SetText(cc.currentCase.caseInfo.finding);
    }


    private void InitializeCaseInformation()
    {
        caseInfo = transform.Find("CaseInfo").gameObject;


        infoName = caseInfo.transform.Find("Content").Find("Info").Find("Name").gameObject;
        infoBirthday = caseInfo.transform.Find("Content").Find("Info").Find("Birthday").gameObject;
        infoGender = caseInfo.transform.Find("Content").Find("Info").Find("Gender").gameObject;
        infoCaseNumber = caseInfo.transform.Find("Content").Find("Info").Find("CaseNr").gameObject;
        infoFinding = caseInfo.transform.Find("Content").Find("Info").Find("Finding").gameObject;
    }
}
