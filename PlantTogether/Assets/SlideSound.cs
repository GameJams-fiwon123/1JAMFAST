using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideSound : MonoBehaviour
{
	Slider slider;

	private void Start() {
		slider = GetComponent<Slider>();
		slider.value = SoundManager.instance.masterVolume;
	}

	public void ChangeVolume() {
		SoundManager.instance.masterVolume = slider.value;
	}
}
