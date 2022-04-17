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

public class VoiceFeedbackController : MonoBehaviour
{
    //https://ttsmp3.com/ us English / Kimberly

    public AudioClip clipCalibrating;
    private AudioSource calibrating;

    public AudioClip clipDrawClose;
    private AudioSource drawClose;

    public AudioClip clipDrawShow;
    private AudioSource drawShow;

    public AudioClip clipDrawStart;
    private AudioSource drawStart;

    public AudioClip clipDrawStop;
    private AudioSource drawStop;

    public AudioClip clipSettings;
    private AudioSource settings;

    public AudioClip clipStepNext;
    private AudioSource stepNext;

    public AudioClip clipStepBack;
    private AudioSource stepBack;

    public AudioClip clipStepNextLast;
    private AudioSource stepNextLast;

    public AudioClip clipStepBackLast;
    private AudioSource stepBackLast;

    public AudioClip clipNoCase;
    private AudioSource noCase;

    public AudioClip clipNoTrackedTool;
    private AudioSource noTrackedTool;

    public AudioClip clipStepsDisabled;
    private AudioSource stepsDisabled;

    public AudioClip clipPatientSettings;
    private AudioSource patientSettings;

    public AudioClip clipNoPatient;
    private AudioSource noPatient;





    private void Start()
    {
        calibrating = gameObject.AddComponent<AudioSource>();
        calibrating.clip = clipCalibrating;
        calibrating.playOnAwake = false;

        drawClose = gameObject.AddComponent<AudioSource>();
        drawClose.clip = clipDrawClose;
        drawClose.playOnAwake = false;

        drawShow = gameObject.AddComponent<AudioSource>();
        drawShow.clip = clipDrawShow;
        drawShow.playOnAwake = false;

        drawStart = gameObject.AddComponent<AudioSource>();
        drawStart.clip = clipDrawStart;
        drawStart.playOnAwake = false;

        drawStop = gameObject.AddComponent<AudioSource>();
        drawStop.clip = clipDrawStop;
        drawStop.playOnAwake = false;

        settings = gameObject.AddComponent<AudioSource>();
        settings.clip = clipSettings;
        settings.playOnAwake = false;

        stepNext = gameObject.AddComponent<AudioSource>();
        stepNext.clip = clipStepNext;
        stepNext.playOnAwake = false;

        stepBack = gameObject.AddComponent<AudioSource>();
        stepBack.clip = clipStepBack;
        stepBack.playOnAwake = false;

        stepNextLast = gameObject.AddComponent<AudioSource>();
        stepNextLast.clip = clipStepNextLast;
        stepNextLast.playOnAwake = false;

        stepBackLast = gameObject.AddComponent<AudioSource>();
        stepBackLast.clip = clipStepBackLast;
        stepBackLast.playOnAwake = false;

        noCase = gameObject.AddComponent<AudioSource>();
        noCase.clip = clipNoCase;
        noCase.playOnAwake = false;

        noTrackedTool = gameObject.AddComponent<AudioSource>();
        noTrackedTool.clip = clipNoTrackedTool;
        noTrackedTool.playOnAwake = false;

        stepsDisabled = gameObject.AddComponent<AudioSource>();
        stepsDisabled.clip = clipStepsDisabled;
        stepsDisabled.playOnAwake = false;

        patientSettings = gameObject.AddComponent<AudioSource>();
        patientSettings.clip = clipPatientSettings;
        patientSettings.playOnAwake = false;

        noPatient = gameObject.AddComponent<AudioSource>();
        noPatient.clip = clipNoPatient;
        noPatient.playOnAwake = false;
    }

    public void PlayCalibrating()
    {
        calibrating.Play();
    }

    public void PlayDrawClose()
    {
        drawClose.Play();
    }

    public void PlayDrawShow()
    {
        drawShow.Play();
    }

    public void PlayDrawStart()
    {
        drawStart.Play();
    }

    public void PlayDrawStop()
    {
        drawStop.Play();
    }

    public void PlaySettings()
    {
        settings.Play();
    }

    public void PlayStepNext()
    {
        stepNext.Play();
    }

    public void PlayStepBack()
    {
        stepBack.Play();
    }

    public void PlayStepNextLast()
    {
        stepNextLast.Play();
    }

    public void PlayStepBackLast()
    {
        stepBackLast.Play();
    }

    public void PlayNoCase()
    {
        noCase.Play();
    }

    public void PlayNoTrackedTool()
    {
        noTrackedTool.Play();
    }

    public void PlayStepsDisabled()
    {
        stepsDisabled.Play();
    }

    public void PlayPatientSettings()
    {
        patientSettings.Play();
    }

    public void PlayNoPatient()
    {
        noPatient.Play();
    }
    //audioSource = gameObject.AddComponent<AudioSource>();
    //    audioSource.playOnAwake = false;
    //    audioSource.clip = GameManager.instance.GetComponent<DistanceSoundController>().clip;
}
