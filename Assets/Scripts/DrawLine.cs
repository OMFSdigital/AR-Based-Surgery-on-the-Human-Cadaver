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

public class DrawLine : MonoBehaviour {
    private LineRenderer lineRenderer;
    private LineRendererController lineController;

    public Vector3[] initialPositions;

    public float x = 0;
    public float y = 0;
    public float z = 0;

    private Vector3 startDrawPosition;


	// Use this for initialization
	void Start () {
        lineRenderer = GameManager.instance.drawLineRenderer.GetComponent<LineRenderer>();
        lineController = lineRenderer.gameObject.GetComponent<LineRendererController>();

	}

    public Vector3 GetStartDrawPosition()
    {
        return startDrawPosition;
    }


    public bool CanZoom()
    {
        return lineRenderer.positionCount > 0;
    }
}
