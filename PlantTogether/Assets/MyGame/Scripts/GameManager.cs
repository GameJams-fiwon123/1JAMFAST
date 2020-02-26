using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

	public static GameManager instance;

	public TextMeshProUGUI textResult;
	public GameObject panelResult;

	public TextMeshProUGUI textTimer;
	public TextMeshProUGUI textFlowers;

	public FMODUnity.StudioEventEmitter musicEvent;

	public int totalFlowers = 50;
	public int currentFlowers = 0;

	public float sec = 0f;
	public float min = 3f;

	public bool isFinish = false;

	public GameObject vasePrefab;
	public Ground[] groundsVase;

	public GameObject myPlayer;
	public Transform[] spawnPlayer;

	private int vaseIndex = 1;

	private void Awake() {
		if (FindObjectsOfType<GameManager>().Length > 1) {
			Destroy(gameObject);
		} else {
			instance = this;
		}
	}

	// Start is called before the first frame update
	void Start() {
		textFlowers.text = currentFlowers + "/" + totalFlowers.ToString();

		int i = UnityEngine.Random.Range(0, spawnPlayer.Length);

		PhotonNetwork.Instantiate(myPlayer.name, spawnPlayer[i].position, spawnPlayer[i].rotation, 0);
	}

	// Update is called once per frame
	void Update() {
		if (!isFinish) {
			sec -= Time.deltaTime;

			if (sec <= 0f && min > 0f) {
				sec = 60f;
				min -= 1f;
			} else if (min == 0f && sec <= 0f) {
				// TODO END
				if (totalFlowers == 0) {
					FMODUnity.RuntimeManager.PlayOneShot("event:/Músicas/Vitória");
					Debug.Log("You Win!");
					textResult.text = "You Win!";
				} else {
					FMODUnity.RuntimeManager.PlayOneShot("event:/Músicas/Derrota");
					Debug.Log("You Lose!");
					textResult.text = "You Lose!";
				}
				musicEvent.Stop();
				isFinish = true;
				panelResult.SetActive(true);
			}

			if (sec < 10) {
				textTimer.text = min + ":0" + Math.Truncate(sec);
			} else {
				textTimer.text = min + ":" + Math.Truncate(sec);
			}

			if (min == 0) {
				musicEvent.SetParameter("tempo", 1);
			}
		}
	}

	public void SendFlower(){
		currentFlowers++;
		textFlowers.text = currentFlowers+ "/"+totalFlowers.ToString();

		foreach(Ground obj in groundsVase) {
			if (obj.obj == null) {
				GameObject refVase = Instantiate(vasePrefab, obj.transform.position, vasePrefab.transform.rotation, null);
				refVase.name = "Vase" + vaseIndex;
				vaseIndex++;
				obj.obj = refVase.GetComponent<InteractObject>();
				refVase.GetComponent<InteractObject>().ground = obj;
				ItemsManager.instance.interactObjects.Add(refVase.GetComponent<InteractObject>());
				break;
			}
		}
	}
}
