using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Lobby : MonoBehaviour
{

	public GameObject painelLogin;
	public GameObject paineLobby;

	public TextMeshProUGUI lobbyAguardar;
	public Text lobbyTimeStart;

	public TMP_InputField playerInputField;
	public string playerName;

	public TextMeshProUGUI playerStatusText;

	public GameObject buttonCancel;

	private void Awake() {
		playerName = "Player" + Random.Range(1000, 10000);
		playerInputField.text = playerName;
	}

	private void Start() {
		lobbyTimeStart.gameObject.SetActive(false);
		PainelLoginActive();

		playerStatusText.gameObject.SetActive(false);
	}

	public void PainelLobbyActive() {
		if (paineLobby)
			paineLobby.SetActive(true);

		if (painelLogin)
			painelLogin.SetActive(false);
	}

	public void PainelLoginActive() {
		paineLobby.SetActive(false);
		painelLogin.SetActive(true);
	}
}
