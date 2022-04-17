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
using UnityEngine;

public class SegmentsMenu : MonoBehaviour
{
    public GameObject prefabSegmentOptions;
    public GameObject segmentsContainer;

    public List<GameObject> segments;

    public int currentScroll;

    public Material enabledMat;
    public Material disabledMat;

    public GameObject previousBackPlate;
    public GameObject nextBackPlate;

    public Interactable nextButton;
    public Interactable backButton;

    private Vector3 offsetSecond = new Vector3(0, -0.404f, 0);


    public void CreateSegmentSettings()
    {
        currentScroll = 0;

        CaseController cc = GameManager.instance.GetComponent<CaseController>();

        foreach(Transform t in cc.currentCaseModels)
        {
            GameObject go = Instantiate(prefabSegmentOptions);
            go.GetComponent<SegmentSettings>().InitializeSegment(t.gameObject);

            segments.Add(go);

            go.transform.SetParent(segmentsContainer.transform, false);

            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;
        }

        DisplayCurrentSegments();
    }

    public void EmptySegmentSettings()
    {
        foreach(Transform t in segmentsContainer.transform)
        {
            Destroy(t.gameObject);
        }

        segments.Clear();
    }

    public void ShowNextSegments()
    {
        currentScroll++;
        DisplayCurrentSegments();
    }

    public void ShowPreviousSegments()
    {
        currentScroll--;
        DisplayCurrentSegments();
    }

    private void DisplayCurrentSegments()
    {
        HideAllSegments();

        segments[currentScroll * 2].SetActive(true);
        segments[currentScroll * 2].transform.localPosition = Vector3.zero;

        if(currentScroll * 2 + 1 <= segments.Count - 1)
        {
            segments[currentScroll * 2 + 1].SetActive(true);
            segments[currentScroll * 2 + 1].transform.localPosition = offsetSecond;
        }

        bool enableBack = currentScroll == 0 ? false : true;
        EnableBackButton(enableBack);

        bool enableNext = (currentScroll + 1) * 2 < segments.Count ? true : false;
        EnableNextButton(enableNext);

        Debug.Log("1: " + ((currentScroll + 1 )* 2));
        Debug.Log("2: " + (segments.Count));
    }

    private void HideAllSegments()
    {
        foreach(GameObject go in segments)
        {
            go.SetActive(false);
        }
    }

    private void EnableBackButton(bool doEnable)
    {
        backButton.IsEnabled = doEnable;
        previousBackPlate.GetComponent<Renderer>().material = doEnable ? enabledMat : disabledMat;
    }

    private void EnableNextButton(bool doEnable)
    {

        nextButton.IsEnabled = doEnable;
        nextBackPlate.GetComponent<Renderer>().material = doEnable ? enabledMat : disabledMat;
    }
}