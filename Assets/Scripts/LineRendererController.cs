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

public class LineRendererController : MonoBehaviour
{
    public ZoomLineRenderer zoomLineRenderer;

    public Vector3[] initialPositions;

    public List<Vector3> linePoints = new List<Vector3>();
    private bool doDraw;
    private bool isZoomed;

    private LineRenderer lineRenderer;

    private Vector3 startDrawPosition;

    void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();

        Initialize();
    }

    void Update()
    {
        if (!doDraw) return;

        GameObject currentTool = GameManager.instance.GetComponent<TrackableController>().trackedTool;
        if (!currentTool) return;

        linePoints.Add(currentTool.GetComponent<TrackableTool>().tip.transform.position);
        lineRenderer.positionCount++;

        for (int i = 0; i < linePoints.Count; i++)
        {
            lineRenderer.SetPosition(i, linePoints[i]);
        }
    }

    public void ChangeMaterial(Material mat)
    {
        gameObject.GetComponent<Renderer>().material = mat;

        //Also change material of zoomed drawing if we are showing it right now
        if (zoomLineRenderer.GetComponent<Renderer>() != null)
        {
            zoomLineRenderer.GetComponent<Renderer>().material = mat;
        }

    }

    void Initialize()
    {
        doDraw = false;
        isZoomed = false;
        linePoints = new List<Vector3>();

        lineRenderer.gameObject.SetActive(true);
        lineRenderer.positionCount = 0;
    }

    public void StartDrawing()
    {
        doDraw = true;
        //startDrawPosition = GameManager.instance.GetComponent<TrackableController>().trackedTool.GetComponent<TrackableTool>().tip.transform.position;
    }

    public void StopDrawing()
    {
        doDraw = false;
    }

    public void Erase()
    {
        Initialize();
    }

    public bool CanZoom()
    {
        return lineRenderer.positionCount > 0;
    }

    public void PresentDraw()
    {
        if (!(lineRenderer.positionCount > 0))
        {
            //Disable zoom if it shown right now (because we cannot zoom means we are either zoomed or not drawn yet)
            if (zoomLineRenderer.gameObject.activeSelf)
            {
                zoomLineRenderer.gameObject.SetActive(false);
            }

            return;
        }

        zoomLineRenderer.gameObject.SetActive(!zoomLineRenderer.gameObject.activeSelf);
        Debug.Log("bounds: " + zoomLineRenderer.GetComponent<Renderer>().bounds.size);

        Erase();
    }
}
