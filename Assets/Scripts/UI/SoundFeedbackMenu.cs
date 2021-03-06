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

using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoundFeedbackMenu : MonoBehaviour
{
    public Interactable toggleFeedbackEnabledButton;
    public PinchSlider valueSlider;
    public TextMeshPro valueText;

    private void OnEnable()
    {
        toggleFeedbackEnabledButton.IsToggled = GameManager.instance.GetComponent<FeatureController>().soundFeedbackEnabled;
        UpdateSoundFrequency();
    }

    public void UpdateSoundFrequency()
    {
        GameManager.instance.GetComponent<DistanceSoundController>().UpdateSoundFrequency(valueSlider.SliderValue);
        UpdateText();
    }

    private void UpdateText()
    {
        valueText.text = (GameManager.instance.GetComponent<DistanceSoundController>().soundFrequency).ToString("F2");
    }
}
