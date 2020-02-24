using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Lobby : MonoBehaviour
{

	public GameObject painelLogin;
	public GameObject paineLobby;

	public TextMeshProUGUI lobbyAguardar;
	public TextMeshProUGUI lobbyTimeStart;

	public string lobbyTImeStartText = "Start Game in {}...";

	private void Start() {
		lobbyTimeStart.gameObject.SetActive(false);
		PainelLoginActive();
	}

	public void LoadGame() {
		SceneManager.LoadScene(1);
	}

	public void PainelLobbyActive() {
		paineLobby.SetActive(true);
		painelLogin.SetActive(false);
	}

	public void PainelLoginActive() {
		paineLobby.SetActive(false);
		painelLogin.SetActive(true);
	}
}
