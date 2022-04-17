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

public class ColorFeedback : MonoBehaviour
{
    //private Color initialColor;
    //private Color highlightColor;

    private Material initialMaterial;

    void Start()
    {
        //initialColor = GetComponent<Renderer>().material.color;
        //highlightColor = new Color(255, 0, 0, 0.3f);

        initialMaterial = GetComponent<Renderer>().material;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!GameManager.instance.GetComponent<FeatureController>().colorFeedbackEnabled) return;

        GameObject trackedTool = GameManager.instance.GetComponent<TrackableController>().trackedTool;
        if (trackedTool == null) return;

        if (trackedTool.GetComponent<TrackableTool>().tip.Equals(other.gameObject))
        {
            //GetComponent<Renderer>().material.color = highlightColor;
            GetComponent<Renderer>().material = GameManager.instance.GetComponent<ColorFeedbackController>().mat;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!GameManager.instance.GetComponent<FeatureController>().colorFeedbackEnabled) return;

        GameObject trackedTool = GameManager.instance.GetComponent<TrackableController>().trackedTool;
        if (trackedTool == null) return;

        if (trackedTool.GetComponent<TrackableTool>().tip.Equals(other.gameObject))
        {
            //GetComponent<Renderer>().material.color = initialColor;
            GetComponent<Renderer>().material = initialMaterial;
        }
    }
}
