using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class StartController : MonoBehaviour {
    public Image layer;
    private new AudioSource audio;

    void Awake() {
        if(!PlayerPrefs.HasKey("Volume")) {
            PlayerPrefs.SetFloat("Volume", 0.5f);
        }
    }

    void Start() {
        layer.DOFade(0f, 1f);
        audio = GetComponent<AudioSource>();
        audio.volume = PlayerPrefs.GetFloat("Volume");
        audio.PlayDelayed(0.5f);
    }

    public void Mulai() {
        audio.DOFade(0f, 1f);
        layer.DOFade(1f, 1f).OnComplete(StartGame);
    }

    public void StartGame() {
        if (PlayerPrefs.HasKey("Played")) {
            LoadingController.scene = "MainMenuScene";
            SceneManager.LoadScene("LoadingScene");
        }
        else {
            LoadingController.scene = "IntroScene";
            SceneManager.LoadScene("LoadingScene");
        }        
    }
}
