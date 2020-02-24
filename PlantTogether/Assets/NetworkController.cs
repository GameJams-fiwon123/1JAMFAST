using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkController : MonoBehaviourPunCallbacks
{

	public Lobby lobbyScript;

	public override void OnConnected() {
		Debug.Log("OnConnected");
	}

	public override void OnConnectedToMaster() {
		Debug.Log("OnConnectedToMaster");

		lobbyScript.PainelLobbyActive();
	}

	public override void OnDisconnected(DisconnectCause cause) {
		Debug.Log("OnDisconnected: " + cause.ToString());

		lobbyScript.PainelLoginActive();
	}

	public void BotaoCancelar() {
		PhotonNetwork.Disconnect();
	}

	public void BotaoLogin() {
		PhotonNetwork.ConnectUsingSettings();
	}
}
