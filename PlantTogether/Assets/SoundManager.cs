using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
	FMOD.Studio.Bus masterBus;

	public float masterVolume = 0.5f;

	public static SoundManager instance;

	private void Awake() {
		if (FindObjectsOfType<SoundManager>().Length > 1) {
			Destroy(gameObject);
		} else {
			DontDestroyOnLoad(this);
			instance = this;
		}
	}

	// Start is called before the first frame update
	void Start() {
		string masterBusString = "Bus:/";

		masterBus = FMODUnity.RuntimeManager.GetBus(masterBusString);
	}

	// Update is called once per frame
	void Update() {
		masterBus.setVolume(masterVolume);
	}

}
