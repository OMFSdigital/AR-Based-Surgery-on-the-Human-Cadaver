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

public class AngleController : MonoBehaviour
{
    private CaseController cc;
    private TrackableController tc;
    private void Start()
    {
        cc = GameManager.instance.GetComponent<CaseController>();
        tc = GameManager.instance.GetComponent<TrackableController>();
    }

    private void Update()
    {
        if (!NeedToShowAngleCorrection())
        {
            if(tc.trackedTool && tc.trackedTool.GetComponent<TrackableTool>().angleInfoBox.activeSelf)
            {
                tc.trackedTool.GetComponent<TrackableTool>().angleInfoBox.SetActive(false);
            }
            return;
        }

        if(!tc.trackedTool.GetComponent<TrackableTool>().angleInfoBox.activeSelf)
        {
            tc.trackedTool.GetComponent<TrackableTool>().angleInfoBox.SetActive(true);
        }

        Transform currentStep = cc.currentStepModels[cc.currentStep];
        Vector3 correctDirection = currentStep.Find(cc.TOKEN_GUIDE_END).GetComponent<Renderer>().bounds.center - currentStep.Find(cc.TOKEN_GUIDE_START).GetComponent<Renderer>().bounds.center;
        Vector3 toolDirection = tc.trackedTool.GetComponent<TrackableTool>().tip.transform.forward * -1;

        //Debug.DrawRay(currentStep.Find(cc.TOKEN_GUIDE_START).GetComponent<Renderer>().bounds.center, correctDirection * 50f, Color.red);
        //Debug.DrawRay(tc.trackedTool.GetComponent<TrackableTool>().tip.transform.position, tc.trackedTool.GetComponent<TrackableTool>().tip.transform.forward * -1 * 50, Color.blue);

        //Debug.Log("Angle: " + Vector3.Angle(correctDirection, toolDirection));

        tc.trackedTool.GetComponent<TrackableTool>().angleText.text = GetAngleText(correctDirection, toolDirection);

    }

    private bool NeedToShowAngleCorrection() 
    {
        if (!tc.trackedTool) return false;
        if (cc.currentCase == null) return false;
        if (!cc.showSteps) return false;
        if (cc.currentStepModels.Count < 1) return false;
        if (cc.currentStepModels[cc.currentStep].transform.childCount < 2) return false;

        return true;
    }

    private string GetAngleText(Vector3 correctDirection, Vector3 toolDirection)
    {
        float angle = Vector3.Angle(correctDirection, toolDirection);

        string result = angle.ToString("F2") + "°";

        return result;
    }
}
