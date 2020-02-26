using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{

	public static GameManager instance;

	public TextMeshProUGUI textResult;
	public GameObject panelResult;

	public GameObject panelVolume;

	public TextMeshProUGUI textTimer;
	public TextMeshProUGUI textFlowers;

	public FMODUnity.StudioEventEmitter musicEvent;

	public int totalFlowers = 50;
	public int currentFlowers = 0;

	private float startTime;

	public float Countdown = 180;

	public bool isFinish = true;

	public GameObject vasePrefab;
	public Ground[] groundsVase;

	public GameObject myPlayer;
	public Transform[] spawnPlayer;

	private int vaseIndex = 1;

	private int totalPlayers;

	private FMOD.Studio.EventInstance eventFmod;

	private void Awake() {
		if (FindObjectsOfType<GameManager>().Length > 1) {
			Destroy(gameObject);
		} else {
			instance = this;
		}
	}

	// Start is called before the first frame update
	void Start() {
		totalPlayers = PhotonNetwork.CurrentRoom.PlayerCount;

		int i = UnityEngine.Random.Range(0, spawnPlayer.Length);
		totalFlowers = 5 * totalPlayers;

		textFlowers.text = currentFlowers + "/" + totalFlowers.ToString();

		PhotonNetwork.Instantiate(myPlayer.name, spawnPlayer[i].position, spawnPlayer[i].rotation, 0);
	}

	// Update is called once per frame
	void Update() {
		if (!isFinish) {

			float timer = (float)PhotonNetwork.Time - startTime;
			float countdown = Countdown - timer;

			string min = ((int)countdown / 60).ToString();
			string sec = (countdown % 60).ToString("00");

			if (sec == "60") {
				return;
			}

			if (textTimer.Equals("0:00")) {
				// TODO END
				GameOver();
			}

			textTimer.text = min + ":" + sec;

			if (timer >= 120) {
				musicEvent.SetParameter("tempo", 1);
			}

			CheckPlayers();
		}
	}

	private void CheckPlayers() {
		if (PhotonNetwork.PlayerList.Length < totalPlayers) {
			GameOver();
		}
	}

	private void GameOver() {
		musicEvent.Stop();

		if (totalFlowers == 0) {
			eventFmod = FMODUnity.RuntimeManager.CreateInstance("event:/Músicas/Vitória");
			eventFmod.start();
			Debug.Log("You Win!");
			textResult.text = "You Win!";
		} else {
			eventFmod = FMODUnity.RuntimeManager.CreateInstance("event:/Músicas/Derrota");
			Debug.Log("You Lose!");
			eventFmod.start();
			textResult.text = "You Lose!";
		}
		isFinish = true;
		panelVolume.SetActive(false);
		panelResult.SetActive(true);
	}

	public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged) {

		object startTimeProps;

		if (propertiesThatChanged.TryGetValue("endgame", out startTimeProps)) {
			isFinish = false;
			startTime = (float)startTimeProps;
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

	public void ExitRoom() {
		PhotonNetwork.LeaveRoom();
	}

	public void ExitSoundPanel() {
		FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Click");
		panelVolume.SetActive(false);
	}


	public override void OnDisconnected(DisconnectCause cause) {
		SceneManager.LoadScene(0);
	}

	public override void OnLeftRoom() {
		PhotonNetwork.Disconnect();
	}
}
