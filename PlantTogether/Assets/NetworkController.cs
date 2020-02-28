using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkController : MonoBehaviourPunCallbacks
{

	public byte playerRoomMax = 2;

	public Lobby lobbyScript;

	private void Start() {
		PhotonNetwork.AutomaticallySyncScene = true;
	}

	public override void OnEnable() {

		CountdownTimer.OnCountdownTimerHasExpired += OnCountDownTimeIsExpired;
	}

	public override void OnDisable() {
		CountdownTimer.OnCountdownTimerHasExpired -= OnCountDownTimeIsExpired;
	}

	void OnCountDownTimeIsExpired() {

		// Chamar a função a ser executada
		StartGame();
	}

	public override void OnConnected() {
		Debug.Log("OnConnected");
	}

	public override void OnConnectedToMaster() {
		Debug.Log("OnConnectedToMaster");
		lobbyScript.PainelLobbyActive();

		PhotonNetwork.JoinLobby();
	}

	public override void OnDisconnected(DisconnectCause cause) {
		Debug.Log("OnDisconnected: " + cause.ToString());

		if (lobbyScript) {
			lobbyScript.PainelLoginActive();
		}
	}

	public override void OnJoinedLobby() {
		Debug.Log("OnJoinedLobby");

		PhotonNetwork.JoinRandomRoom();
	}

	public override void OnJoinRandomFailed(short returnCode, string message) {

		Debug.Log("OnJoinRandomFailed");

		string roomName = "Room" + Random.Range(1000, 10000);

		RoomOptions roomOption = new RoomOptions() {

			IsOpen = true,
			IsVisible = true,
			MaxPlayers = playerRoomMax

		};

		PhotonNetwork.CreateRoom(roomName, roomOption, TypedLobby.Default);
	}

	public override void OnJoinedRoom() {
		Debug.Log("OnJoinedRoom");
	}

	public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer) {

		Debug.Log("OnPlayerEnteredRoom");

		if (PhotonNetwork.CurrentRoom.PlayerCount == playerRoomMax) {

			foreach (var item in PhotonNetwork.PlayerList) {
				if (item.IsMasterClient) {

					Hashtable props = new Hashtable() {
						{ CountdownTimer.CountdownStartTime, (float) PhotonNetwork.Time }
					};

					PhotonNetwork.CurrentRoom.SetCustomProperties(props);

					PhotonNetwork.CurrentRoom.IsOpen = false;
					PhotonNetwork.CurrentRoom.IsVisible = false;

					return;
				}
			}

		}

	}

	public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged) {

		if (propertiesThatChanged.ContainsKey(CountdownTimer.CountdownStartTime)) {
			lobbyScript.lobbyAguardar.gameObject.SetActive(false);
			lobbyScript.buttonCancel.SetActive(false);
			lobbyScript.lobbyTimeStart.gameObject.SetActive(true);
		}

	}

	void StartGame() {
		PhotonNetwork.LoadLevel(1);
	}

	public void BotaoCancelar() {

		FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Click");

		PhotonNetwork.Disconnect();
		lobbyScript.playerStatusText.gameObject.SetActive(false);
		lobbyScript.lobbyAguardar.gameObject.SetActive(true);
		lobbyScript.buttonCancel.SetActive(true);
	}

	public void BotaoLogin() {

		//SceneManager.LoadScene(0);

		FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Click");

		PhotonNetwork.NickName = lobbyScript.playerInputField.text;

		lobbyScript.playerStatusText.gameObject.SetActive(true);

		PhotonNetwork.ConnectUsingSettings();

	}
}
