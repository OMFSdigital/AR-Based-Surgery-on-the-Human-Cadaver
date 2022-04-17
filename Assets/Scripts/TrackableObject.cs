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

//Custom Trackable Class that handles enabling/disabling tracked objects in a useful way
public class TrackableObject : DefaultObserverEventHandler
{

    public bool beingTracked = false;
    //void OnTargetStatusChanged(ObserverBehaviour observerbehavour, TargetStatus status)
    //{
    //    if (status.Status == Status.TRACKED && status.StatusInfo == StatusInfo.NORMAL)
    //    {
    //        // ...
    //    }
    //}

    /*public override void OnTargetStatusChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        m_PreviousStatus = previousStatus;
        m_NewStatus = newStatus;

        //Debug.Log("Trackable " + mTrackableBehaviour.TrackableName +
        //          " - Status: " + mTrackableBehaviour.CurrentStatus +
        //          " - Status Info: " + mTrackableBehaviour.CurrentStatusInfo +
        //          " - Previous Status: " + previousStatus +
        //          " - New Status: " + newStatus);


        if (newStatus == Status.DETECTED ||
            newStatus == Status.TRACKED)
        {
            OnTrackingFound();
            beingTracked = true;
            Debug.Log(mTrackableBehaviour.TrackableName + " is being tracked!");
            
        }
        else if (previousStatus == Status.TRACKED &&
                 newStatus == Status.NO_POSE)
        {
            OnTrackingLost();
            beingTracked = false;
        }
        else
        {
            OnTrackingLost();
            beingTracked = false;
        }
    }*/
}
