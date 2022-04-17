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
using Microsoft.MixedReality.Toolkit.Utilities;

public class ZoomLineRenderer : MonoBehaviour
{
    private LineRenderer mainLineRenderer;

    private void OnEnable()
    {
        mainLineRenderer = GameManager.instance.drawLineRenderer.GetComponent<LineRenderer>();
        BakeLineDebuger(mainLineRenderer.gameObject);
        GetComponent<Microsoft.MixedReality.Toolkit.UI.BoundingBox>().enabled = true;
        GetComponent<Microsoft.MixedReality.Toolkit.UI.ManipulationHandler>().enabled = true;
    }

    private void OnDisable()
    {
        gameObject.transform.position = Vector3.zero;
        gameObject.transform.rotation = Quaternion.identity;
        gameObject.transform.localScale = Vector3.one;
        GetComponent<Microsoft.MixedReality.Toolkit.UI.BoundingBox>().enabled = false;
        GetComponent<Microsoft.MixedReality.Toolkit.UI.ManipulationHandler>().enabled = false;
        Destroy(gameObject.GetComponent<MeshRenderer>());
        Destroy(gameObject.GetComponent<MeshFilter>());
        //Destroy(gameObject.GetComponent<Microsoft.MixedReality.Toolkit.UI.TransformScaleHandler>());
        Destroy(gameObject.GetComponent<Microsoft.MixedReality.Toolkit.Input.NearInteractionGrabbable>());
    }

    public void BakeLineDebuger(GameObject lineObj)
    {
        Debug.Log("NAME: " + lineObj.name);
        var lineRenderer = lineObj.GetComponent<LineRenderer>();
        var meshFilter = gameObject.AddComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        lineRenderer.BakeMesh(mesh);
        meshFilter.sharedMesh = mesh;

        gameObject.AddComponent<MeshRenderer>();
        gameObject.GetComponent<MeshFilter>().sharedMesh = mesh;
        gameObject.GetComponent<Renderer>().material = mainLineRenderer.gameObject.GetComponent<Renderer>().material;

        gameObject.transform.position = Vector3.zero;
        gameObject.transform.rotation = Quaternion.identity;
        gameObject.transform.localScale = Vector3.one;
    }
}
