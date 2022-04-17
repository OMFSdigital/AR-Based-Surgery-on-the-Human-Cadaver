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
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using System.Text.RegularExpressions;

public class CaseController : MonoBehaviour
{
    //private string fileLocation = ".../PatientData";
    //private string fileLocation = "//PatientData";
    private string fileLocation = "PatientData";
    private string modelLocation = "Models";

    private const string TOKEN_TOP = "Top";
    private const string TOKEN_BOT = "Bot";
    private const string TOKEN_IMPORTANT = "_!_";
    private const string TOKEN_STEP = "Step";
    private const string TOKEN_GUIDE = "Guide";
    public string TOKEN_GUIDE_START = "Start";
    public string TOKEN_GUIDE_END = "End";

    private const float TEMP_PATIENT_SCALE_FACTOR = 0.1f;

    public List<Case> cases;
    public Case currentCase;
    public List<Transform> currentCaseModels;
    public List<Transform> currentStepModels;
    public int currentStep;
    public bool showSteps;

    public GameObject anchorTop;
    public GameObject anchorBot;

    public UnityEvent stepChangedEvent;

    void Awake()
    {
        currentCase = null;
        currentStep = 0;
        showSteps = false;
        ParseCases();

        //GameObject prefab = Resources.Load<GameObject>("untitled");

        //GameObject go = Instantiate(prefab);
        //go.name = prefab.name;
    }

    public void LoadCase(Case currentCase)
    {
        this.currentCase = currentCase;


        //if (!File.Exists(currentCase.modelLocation))
        //{
        //    Debug.LogError("Please set FilePath in json to a valid path.");
        //    return;
        //}

        //load
        //GameObject go = new OBJLoader().Load(currentCase.modelLocation);
        //go.name = prefab.name;

        string filePath = fileLocation + "/" + modelLocation + "/" + currentCase.modelLocation;

        GameObject go = CreateModel(filePath);
        MovePartsToAnchors(go);
        AddLogicToRelevantParts();
        DisableMeshRenderers();
    }

    private GameObject CreateModel(string filePath)
    {
        GameObject go = Instantiate(Resources.Load<GameObject>(filePath));
        go.transform.localScale = new Vector3(TEMP_PATIENT_SCALE_FACTOR, TEMP_PATIENT_SCALE_FACTOR, TEMP_PATIENT_SCALE_FACTOR);

        return go;
    }

    //Disabling mesh renderers at the very start so that
    //They are not visible before tracking happens.
    //When tracking happens, they get enabled again anyways
    private void DisableMeshRenderers()
    {
        foreach (Transform t in currentCaseModels)
        {
            foreach (var mesh in t.GetComponentsInChildren<MeshRenderer>())
            {
                if (mesh.gameObject.GetComponent<MeshRenderer>() != null)
                {
                    mesh.gameObject.GetComponent<MeshRenderer>().enabled = false;
                    if (mesh.gameObject.GetComponent<MeshCollider>() != null)
                    {
                        mesh.gameObject.GetComponent<MeshCollider>().enabled = false;
                    }

                }
            }
        }
    }

    private void MovePartsToAnchors(GameObject go)
    {
        currentCaseModels = new List<Transform>();
        currentStepModels = new List<Transform>();
        List<Transform> currentGuideModels = new List<Transform>();

        //Add Steps (without guide) and case parts corresponding lists
        foreach (Transform t in go.transform)
        {
            if (t.name.Contains(TOKEN_STEP))
            {
                if(!t.name.Contains(TOKEN_GUIDE))
                {
                    currentStepModels.Add(t);
                }     
            }
            else
            {
                currentCaseModels.Add(t);
            }
        }

        //Parent case parts to their anchors
        foreach (Transform t in currentCaseModels)
        {
            if (t.name.StartsWith(TOKEN_TOP))
            {
                t.SetParent(anchorTop.transform);
                t.localPosition = Vector3.zero;
                t.localRotation = Quaternion.identity;
                t.localScale = new Vector3(TEMP_PATIENT_SCALE_FACTOR, TEMP_PATIENT_SCALE_FACTOR, TEMP_PATIENT_SCALE_FACTOR);
            }
            else if (t.name.StartsWith(TOKEN_BOT))
            {
                t.SetParent(anchorBot.transform);
                t.localPosition = Vector3.zero;
                t.localRotation = Quaternion.identity;
                t.localScale = new Vector3(TEMP_PATIENT_SCALE_FACTOR, TEMP_PATIENT_SCALE_FACTOR, TEMP_PATIENT_SCALE_FACTOR);
            }
            else Debug.LogError("Neither Top or Bot anchor??? " + t.name);
        }

        //Parent steps to their anchors
        foreach (Transform t in currentStepModels)
        {
            if (t.name.Contains(TOKEN_TOP))
            {
                t.SetParent(anchorTop.transform);
                t.localPosition = Vector3.zero;
                t.localRotation = Quaternion.identity;
                t.localScale = new Vector3(TEMP_PATIENT_SCALE_FACTOR, TEMP_PATIENT_SCALE_FACTOR, TEMP_PATIENT_SCALE_FACTOR);
            }
            else if (t.name.Contains(TOKEN_BOT))
            {
                t.SetParent(anchorBot.transform);
                t.localPosition = Vector3.zero;
                t.localRotation = Quaternion.identity;
                t.localScale = new Vector3(TEMP_PATIENT_SCALE_FACTOR, TEMP_PATIENT_SCALE_FACTOR, TEMP_PATIENT_SCALE_FACTOR);
            }
            else Debug.LogError("Neither Top or Bot anchor??? " + t.name);

            t.gameObject.SetActive(false);
        }

        //Add guide to steps
        string pattern = TOKEN_STEP + "_" + "[0-9]{2}";
        foreach (Transform t in go.transform)
        {
            Match match = Regex.Match(t.name, pattern, RegexOptions.IgnoreCase);
            if (match.Success)
            {
                currentGuideModels.Add(t);
            }
        }
        foreach (Transform t in currentGuideModels)
        {
            string name = Regex.Match(t.name, pattern, RegexOptions.IgnoreCase).Value;
            foreach (Transform t2 in currentStepModels)
            {
                if (t2.name.Contains(name))
                {
                    t.SetParent(t2, false);
                    t.name = t.name.Contains(TOKEN_GUIDE_START) ? TOKEN_GUIDE_START : TOKEN_GUIDE_END;
                    t.gameObject.SetActive(false);
                }
            }
        }

        //Destroy(go);
    }

    private void AddLogicToRelevantParts()
    {
        foreach (Transform t in currentCaseModels)
        {
            if (t.name.Contains(TOKEN_IMPORTANT))
            {
                t.gameObject.AddComponent<ColorFeedback>();
                t.gameObject.AddComponent<SoundFeedback>();
                t.gameObject.tag = "Collidable";
            }
        }
    }

    public void UnloadCase()
    {
        currentCase = null;
        currentStep = 0;
        showSteps = false;

        foreach (Transform t in anchorTop.transform)
        {
            Destroy(t.gameObject);
        }

        foreach (Transform t in anchorBot.transform)
        {
            Destroy(t.gameObject);
        }

    }

    public void ParseCases()
    {
        cases = new List<Case>();

        //foreach (string file in System.IO.Directory.GetFiles(fileLocation))
        //{
        //    if (!file.EndsWith(".json")) continue;

        //    AddCase(file);
        //}

        foreach (TextAsset file in Resources.LoadAll<TextAsset>(fileLocation))
        {
            AddCase(file.text);
        }
    }

    private void AddCase(string text)
    {
        //string json = File.ReadAllText(path);
        cases.Add(JsonUtility.FromJson<Case>(text));
    }

    public void ToggleSteps()
    {
        showSteps = !showSteps;

        if (showSteps) DisplayCurrentStep();
        else
        {
            HideSteps();
        }
    }

    public bool NextStep()
    {
        if (currentCase == null)
        {
            GameManager.instance.GetComponent<VoiceFeedbackController>().PlayNoCase();
            return false;
        }

        if (!showSteps)
        {
            GameManager.instance.GetComponent<VoiceFeedbackController>().PlayStepsDisabled();
            return false;
        }

        if (currentStep >= currentCase.steps.Length - 1)
        {
            GameManager.instance.GetComponent<VoiceFeedbackController>().PlayStepNextLast();
            return false;
        }

        GameManager.instance.GetComponent<VoiceFeedbackController>().PlayStepNext();
        currentStep++;
        DisplayCurrentStep();
        stepChangedEvent.Invoke();

        return true;
    }

    public bool PreviousStep()
    {
        if (currentCase == null)
        {
            GameManager.instance.GetComponent<VoiceFeedbackController>().PlayNoCase();
            return false;
        }

        if (!showSteps)
        {
            GameManager.instance.GetComponent<VoiceFeedbackController>().PlayStepsDisabled();
            return false;
        }

        if (currentStep <= 0)
        {
            GameManager.instance.GetComponent<VoiceFeedbackController>().PlayStepBackLast();
            return false;
        }

        GameManager.instance.GetComponent<VoiceFeedbackController>().PlayStepBack();
        currentStep--;
        DisplayCurrentStep();
        stepChangedEvent.Invoke();

        return true;
    }

    public bool HasPreviousStep()
    {
        if (currentStep > 0) return true;
        return false;
    }

    public bool HasNextStep()
    {
        if (currentStep < currentCase.steps.Length - 1) return true;
        return false;
    }

    private void DisplayCurrentStep()
    {
        HideSteps();

        currentStepModels[currentStep].gameObject.SetActive(true);
    }

    private void HideSteps()
    {
        foreach (Transform t in currentStepModels)
        {
            t.gameObject.SetActive(false);
        }
    }
}
