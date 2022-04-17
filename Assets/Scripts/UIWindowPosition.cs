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

public class UIWindowPosition : MonoBehaviour
{
    public float headOffsetDistance = 1f;
    public float headOffsetHeight = 0f;

    private bool started = false;

    private void Start()
    {
        started = true;

        RepositionUI();
    }

    private void OnEnable()
    {
        if (started)
        {
            RepositionUI();
        }
    }

    private void RepositionUI()
    {
        transform.position = CameraCache.Main.transform.position +  CameraCache.Main.transform.forward * headOffsetDistance + CameraCache.Main.transform.up * headOffsetHeight;

        //Look at in the opposite direction
        transform.LookAt(2 * transform.position - CameraCache.Main.transform.position);
    }
}
