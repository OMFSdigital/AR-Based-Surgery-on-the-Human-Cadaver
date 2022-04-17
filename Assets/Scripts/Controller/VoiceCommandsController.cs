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
using UnityEngine.Windows.Speech;
using System.Linq;
using UnityEngine.UI;
public class VoiceCommandsController : MonoBehaviour
{
    KeywordRecognizer keywordRecognizer;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    private GameObject gui;
    private GameObject patientGui;

    void Start()
    {
        AddVoiceCommands();
        

        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();

        gui = GameManager.instance.ui;
        patientGui = GameManager.instance.patientUi;

        //casesFrozen = false;

    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        keywords[speech.text].Invoke();


        Debug.Log("recognized the following: " + speech.text);
        //text.text += " " + speech.text;
    }

    private void AddVoiceCommands()
    {
        keywords.Add("Settings", () =>
        {
            if (gui.activeSelf)
                gui.SetActive(false);

            gui.SetActive(true);

            GameManager.instance.GetComponent<VoiceFeedbackController>().PlaySettings();
        });

        keywords.Add("Patient", () =>
        {
            if (!patientGui.activeSelf)
            {
                if(GameManager.instance.GetComponent<CaseController>().currentCase == null)
                {
                    GameManager.instance.GetComponent<VoiceFeedbackController>().PlayNoPatient();
                } 
                else
                {
                    patientGui.SetActive(true);
                    GameManager.instance.GetComponent<VoiceFeedbackController>().PlayPatientSettings();
                }
            } 
        });

        keywords.Add("calibrate", () =>
        {
            Debug.Log("Calibrating...");
            gameObject.GetComponent<TrackableController>().CalibrateAndDisplayTool();

            GameManager.instance.GetComponent<VoiceFeedbackController>().PlayCalibrating();
        });

        //Drawing Commands
        keywords.Add("start", () =>
        {
            GameObject currentTool = GameManager.instance.GetComponent<TrackableController>().trackedTool;
            Debug.Log("draw start");
            if(currentTool)
            {
                GameManager.instance.drawLineRenderer.GetComponent<LineRendererController>().StartDrawing();

                GameManager.instance.GetComponent<VoiceFeedbackController>().PlayDrawStart();
            } 
            else
            {
                GameManager.instance.GetComponent<VoiceFeedbackController>().PlayNoTrackedTool();
            }
        });
        keywords.Add("stop", () =>
        {
            GameObject currentTool = GameManager.instance.GetComponent<TrackableController>().trackedTool;
            Debug.Log("draw stop");
            if (currentTool)
            {
                GameManager.instance.drawLineRenderer.GetComponent<LineRendererController>().StopDrawing();

                GameManager.instance.GetComponent<VoiceFeedbackController>().PlayDrawStop();
            }
            else
            {
                GameManager.instance.GetComponent<VoiceFeedbackController>().PlayNoTrackedTool();
            }
        });
        keywords.Add("close", () =>
        {
            GameManager.instance.drawLineRenderer.GetComponent<LineRendererController>().Erase();

            if(GameManager.instance.drawLineRenderer.GetComponent<LineRendererController>().zoomLineRenderer.enabled)
            {
                GameManager.instance.drawLineRenderer.GetComponent<LineRendererController>().zoomLineRenderer.gameObject.SetActive(false);
            }

            GameManager.instance.GetComponent<VoiceFeedbackController>().PlayDrawClose();
        });
        keywords.Add("show", () =>
        {
            GameManager.instance.drawLineRenderer.GetComponent<LineRendererController>().PresentDraw();

            GameManager.instance.GetComponent<VoiceFeedbackController>().PlayDrawShow();
        });

        keywords.Add("step next", () =>
        {
            GameManager.instance.GetComponent<CaseController>().NextStep();
        });

        keywords.Add("step back", () =>
        {
            GameManager.instance.GetComponent<CaseController>().PreviousStep();
        });
    }

    //private void FreezeCases()
    //{
    //    cases.GetComponent<Vuforia.ImageTargetBehaviour>().enabled = casesFrozen;
    //    cases.GetComponent<DefaultTrackableEventHandler>().enabled = casesFrozen;
    //    cases.GetComponent<TrackableObject>().enabled = casesFrozen;
    //    cases.GetComponent<Vuforia.TurnOffBehaviour>().enabled = casesFrozen;

    //    if(!casesFrozen)
    //    {
    //        foreach (var mesh in cases.GetComponentsInChildren<MeshRenderer>())
    //        {

    //            if (mesh.gameObject.name.Contains("Case "))
    //            {
    //                if(mesh.gameObject.GetComponent<MeshRenderer>() != null)
    //                {
    //                    mesh.gameObject.GetComponent<MeshRenderer>().enabled = true;
    //                    if(mesh.gameObject.GetComponent<MeshCollider>() != null)
    //                    {
    //                        mesh.gameObject.GetComponent<MeshCollider>().enabled = true;
    //                    }

    //                }    
    //            }
    //        }
    //    }
    //    else
    //    {
    //        if(toggleModelAdjustButton.IsToggled)
    //        {
    //            GameManager.instance.GetComponent<Microsoft.MixedReality.Toolkit.Input.UIManager>().ToggleModelAdjustMenu();
    //        }
    //        toggleModelAdjustButton.IsToggled = false;
    //    }


    //    casesFrozen = !casesFrozen;

    //    casesRadios.SetActive(!casesFrozen);
    //    casesMenuWhenFrozen.SetActive(casesFrozen);


    //}

    //public void ResetDraw()
    //{
    //    drawLine.StopDrawing();
    //    drawLine.Erase();
    //}


}