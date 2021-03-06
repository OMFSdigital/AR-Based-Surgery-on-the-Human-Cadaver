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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Case
{
    public string modelLocation = "";
    public CaseInfo caseInfo = new CaseInfo();
    public string[] steps;

    [Serializable]
    public class CaseInfo
    {
        public string inserted;
        public string updated;
        public string author;
        public string name;
        public string patient;
        public string birthdate;
        public string gender;
        public string caseNumber;
        public string finding;
        public string info;
    }
}
