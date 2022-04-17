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
using Vuforia;

public class TrackableController : MonoBehaviour
{

    //Current active tracked tool
    public GameObject trackedTool;

    private void Start()
    {
        trackedTool = null;
    }

    private void Update()
    {
        bool toolTracked = false;

        foreach (TrackableBehaviour tb in TrackerManager.Instance.GetStateManager().GetActiveTrackableBehaviours())
        {
            
            if (tb.gameObject.tag == "Tool")
            {
                if (!tb.gameObject.GetComponent<TrackableObject>().beingTracked) continue;

                toolTracked = true;

                if(trackedTool == null && tb.GetComponent<TrackableTool>().calibrated)
                {
                    trackedTool = tb.gameObject;
                }
                break;
            } 
        }

        if(!toolTracked)
        {
            trackedTool = null;
        }
    }

    //Gets called by VoiceCommandsController
    public void CalibrateAndDisplayTool()
    {
        // Get the Vuforia StateManager
        StateManager sm = TrackerManager.Instance.GetStateManager();

        // Query the StateManager to retrieve the list of currently active trackables 
        IEnumerable<TrackableBehaviour> activeTrackables = sm.GetActiveTrackableBehaviours();

        // Only continue if calibrator being tracked
        if (!IsCalibratorTracked(activeTrackables)) return;

        foreach (TrackableBehaviour tb in activeTrackables)
        {
            Debug.Log("Trackable: " + tb.TrackableName);
            if(tb.gameObject.tag == "Tool")
            {
                if (tb.gameObject.GetComponent<TrackableTool>().calibrated)
                {
                    Debug.Log("We have already calibrated " + tb.gameObject.name + ", continueing");
                    continue;
                }

                Debug.Log("Calibrating on: " + tb.gameObject.name);
                Debug.Log("We being tracked: " + tb.GetComponent<TrackableObject>().beingTracked);
                tb.gameObject.GetComponent<TrackableTool>().Calibrate();
                break;
            }
        }
    }


    private bool IsCalibratorTracked(IEnumerable<TrackableBehaviour> activeTrackables)
    {
        bool result = false;

        foreach (TrackableBehaviour tb in activeTrackables)
        {
            if (tb.gameObject.tag == "Calibrator")
            {
                result = true;
                break;
            }
        }

        return result;
    }
}