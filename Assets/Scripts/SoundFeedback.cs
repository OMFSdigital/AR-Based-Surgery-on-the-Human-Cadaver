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

public class SoundFeedback : MonoBehaviour
{
    private AudioSource audioSource;

    private float DISTANCE_THRESHOLD = 0.05f;
    private float FREQUENCY_MULTIPLIER = 50f;

    //public GameObject debugTip;

    private float lastTimePlayed;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = GameManager.instance.GetComponent<DistanceSoundController>().clip;

        gameObject.AddComponent<MeshCollider>().convex = true;

        lastTimePlayed = 0;
    }

    void Update()
    {
        if(!GameManager.instance.GetComponent<FeatureController>().soundFeedbackEnabled)
        {
            audioSource.Stop();
            return;
        }

        if (!GetComponent<MeshRenderer>().enabled)
        {
            audioSource.Stop();
            return;
        }

        if (!GameManager.instance.GetComponent<DistanceSoundController>().isActive)
        {
            audioSource.Stop();
            return;
        }

        if (GameManager.instance.GetComponent<TrackableController>().trackedTool == null)
        {
            audioSource.Stop();
            return;
        }

        Vector3 position = Physics.ClosestPoint(
            GameManager.instance.GetComponent<TrackableController>().trackedTool.GetComponent<TrackableTool>().tip.transform.position,
            GetComponent<Collider>(),
            transform.position,
            transform.rotation);
        float distance = Vector3.Distance(position, GameManager.instance.GetComponent<TrackableController>().trackedTool.GetComponent<TrackableTool>().tip.transform.position);


        //DEBUG
        //Vector3 position = Physics.ClosestPoint(
        //   debugTip.transform.position,
        //   GetComponent<Collider>(),
        //   transform.position,
        //   transform.rotation);
        //float distance = Vector3.Distance(position, debugTip.transform.position);
        //DEBUG END

        PlaySong(distance);

    }

    private void PlaySong(float distance)
    {
        if (distance > DISTANCE_THRESHOLD)
            return;

        float frequency = GameManager.instance.GetComponent<DistanceSoundController>().soundFrequency;

        if (Time.time > lastTimePlayed + FREQUENCY_MULTIPLIER * Mathf.Pow(distance, frequency))
        {

            lastTimePlayed = Time.time + FREQUENCY_MULTIPLIER * Mathf.Pow(distance, frequency);

            if (audioSource.isPlaying)
                return;
            audioSource.Play();
        }
    }
}