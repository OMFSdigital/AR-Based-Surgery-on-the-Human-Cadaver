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

public class CasesMenu : MonoBehaviour
{
    public GameObject prefabCase;

    [SerializeField]
    private GameObject caseInfo;
    [SerializeField]
    private GameObject caseContainer;

    public SegmentsMenu segmentsMenu;
    public SegmentsMenu segmentPatientMenu;

    private GameObject infoName, infoBirthday, infoGender, infoCaseNumber, infoFinding;

    public Vector3 startPosition;
    public float marginDown; //0.17
    public float marginLeft; //0.45

    private void Awake()
    {
        InitializeCaseInformation();

        caseContainer.SetActive(true);
        caseInfo.SetActive(false);
    }


    private void OnEnable()
    {
        LoadCasesInfo();

        //Load correct view (Case overview if no case currently selected. Otherwise show current case
        DisplayCorrectView();
    }

    private void OnDisable()
    {
        RemoveAllCaseMenuButtons();
    }

    private void LoadCasesInfo()
    {
        CaseController cc = GameManager.instance.GetComponent<CaseController>();
        cc.ParseCases();

        for (int i = 0; i < cc.cases.Count; i++)
        {
            GameObject caseMenuItem = Instantiate(prefabCase);
            Case currentCase = cc.cases[i];

            PositionMenuItem(caseMenuItem, i);
            SetMenuText(caseMenuItem, currentCase);

            //Wire up OnClick Logic with correct case
            caseMenuItem.GetComponent<Interactable>().OnClick.AddListener(delegate { LoadCase(currentCase); });
        }
    }

    private void PositionMenuItem(GameObject item, int index)
    {
        item.transform.SetParent(caseContainer.transform);

        item.transform.localPosition = startPosition;
        item.transform.localRotation = Quaternion.identity;

        //Position vertically
        item.transform.position = item.transform.position - transform.up * marginDown * Mathf.FloorToInt(index / 2);

        //Position horizontally
        if(index % 2 == 1)
        {
            item.transform.position = item.transform.position + transform.right * marginLeft;
        }
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
        caseContainer.SetActive(!isCaseLoaded);
        caseInfo.SetActive(isCaseLoaded);

    }

    private void LoadCase(Case currentCase)
    {
        CaseController cc = GameManager.instance.GetComponent<CaseController>();

        cc.LoadCase(currentCase);

        UpdateCaseInformationDisplay();
        DisplayCorrectView();

        segmentsMenu.CreateSegmentSettings();
        segmentPatientMenu.CreateSegmentSettings();
    }

    private void UpdateCaseInformationDisplay()
    {
        CaseController cc = GameManager.instance.GetComponent<CaseController>();

        if(cc.currentCase == null)
        {
            //Do something
            return;
        }

        UpdatePatientDataDisplay();
        //foreach (string step in cc.currentCase.steps)
        //{
        //    //do something
        //}
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

    public void UnloadCase()
    {
        CaseController cc = GameManager.instance.GetComponent<CaseController>();

        RemoveAllCaseMenuButtons();
        UpdateCaseInformationDisplay();
        cc.UnloadCase();
        cc.ParseCases();
        LoadCasesInfo();
        DisplayCorrectView();

        segmentsMenu.EmptySegmentSettings();
        segmentPatientMenu.EmptySegmentSettings();
    }

    private void RemoveAllCaseMenuButtons()
    {
        foreach (Transform t in caseContainer.transform)
        {
            Destroy(t.gameObject);
        }
    }

    private void InitializeCaseInformation()
    {
        caseInfo = transform.Find("CaseInfo").gameObject;
        caseContainer = transform.Find("CaseContainer").gameObject;

        caseInfo.SetActive(false);
        caseContainer.SetActive(true);

        infoName = caseInfo.transform.Find("Content").Find("Info").Find("Name").gameObject;
        infoBirthday = caseInfo.transform.Find("Content").Find("Info").Find("Birthday").gameObject;
        infoGender = caseInfo.transform.Find("Content").Find("Info").Find("Gender").gameObject;
        infoCaseNumber = caseInfo.transform.Find("Content").Find("Info").Find("CaseNr").gameObject;
        infoFinding = caseInfo.transform.Find("Content").Find("Info").Find("Finding").gameObject;
    }
}
