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
using TMPro;
using UnityEngine;
using Vuforia;

public class TrackableTool : TrackableObject
{

    private const float X_OFFSET = 0.475f;
    private const float Y_OFFSET = 0.05f;
    private const float Z_OFFSET = 1.35f;

    private GameObject calibrator;

    private Transform model;
    private Transform panel;

    public bool calibrated;

    public GameObject tip;
    public GameObject angleInfoBox;
    public TextMeshPro angleText;

    new void Start()
    {
        base.Start();

        calibrator = GameObject.FindGameObjectWithTag("Calibrator").transform.gameObject;

        model = transform.Find("Tool").Find("Model");
        panel = transform.Find("Tool").Find("Panel");

        calibrated = false;

        panel.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.red);

    }

    private void Update()
    {
        if (calibrated) return;

        if (calibrator.GetComponent<TrackableObject>().beingTracked)
        {
            panel.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.green);

        }
        else
        {
            panel.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }

    }

    //Tool can only start being displayed after calibration happened
    //TrackableController handles this
    public void DisplayTool()
    {
        Calibrate();
    }

    public void Calibrate()
    {
        model.SetParent(calibrator.transform.GetChild(0).GetChild(0));

        model.transform.localEulerAngles = new Vector3(0, -90, 0);
        model.position = Vector3.zero;

        model.localPosition = new Vector3(X_OFFSET, Y_OFFSET, Z_OFFSET);

        model.SetParent(transform, true);
        model.transform.localEulerAngles = new Vector3(0, 0, 0);

        model.gameObject.SetActive(true);
        panel.gameObject.SetActive(false);
        calibrated = true;
 
    }

    public void ResetCalibration()
    {
        panel.gameObject.SetActive(true);
        model.gameObject.SetActive(false);
        calibrated = false;
    }

}
