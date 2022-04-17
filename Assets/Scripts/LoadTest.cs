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

using Dummiesman;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadTest : MonoBehaviour
{

    void Start()
    {
        //GameObject prefab = Resources.Load<GameObject>("untitled");

        //GameObject go = Instantiate(prefab);
        //go.name = prefab.name;
    }
}



////file path
//string filePath = ".../PatientData/lol.obj";
//string mtl = ".../PatientData/lol.mtl";

//if (!File.Exists(filePath))
//{
//    Debug.LogError("Please set FilePath in ObjFromFile.cs to a valid path.");
//    return;
//}

////load
//GameObject loadedObj = new OBJLoader().Load(filePath);