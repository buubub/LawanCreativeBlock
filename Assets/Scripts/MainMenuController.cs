using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenuController : MonoBehaviour {
    public GameObject help, exit, saranDisabled;
    public Image layer;
    public Sprite saranOn;
    public Button quiz, creativeBlock, tipeCreativeBlock, saran, mute, unmute;
    private new AudioSource audio;
    public AudioClip beforeQuiz, afterQuiz;

    void Start() {
        SaranCheck();
        MuteCheck();
        layer.DOFade(0f, 1f);
        audio = GetComponent<AudioSource>();
        audio.volume = PlayerPrefs.GetFloat("Volume");
        if (PlayerPrefs.HasKey("Character")) {
            audio.clip = afterQuiz;
        }
        else {
            audio.clip = beforeQuiz;
        }
        audio.PlayDelayed(0.75f);
    }
   
    private void SaranCheck() {
        if (PlayerPrefs.HasKey("Character")) {
            saran.interactable = true;
            saran.gameObject.GetComponent<Image>().sprite = saranOn;
        }
    }

    private void MuteCheck() {
        if (PlayerPrefs.GetFloat("Volume") == 0.5f) {
            unmute.gameObject.SetActive(false);
        }
        else {
            mute.gameObject.SetActive(false);
        }
    }

    public void openHelp(bool open) {
        help.SetActive(open);
    }

    public void openSaran(bool open) {
        saranDisabled.SetActive(open);
    }

    public void openExit(bool open) {
        exit.SetActive(open);
    }

    public void Exit() {
        Application.Quit();
    }

    public void Go(string Scene) {
        LoadingController.scene = Scene;
        audio.DOFade(0f, 0.25f).SetDelay(0.25f); ;
        layer.DOFade(1f, 0.5f).OnComplete(() => SceneManager.LoadScene("LoadingScene"));
    }

    public void GoSaran() {
        if (PlayerPrefs.HasKey("Character")) {
            LoadingController.scene = "SaranScene";
            audio.DOFade(0f, 0.25f).SetDelay(0.25f);
            layer.DOFade(1f, 0.5f).OnComplete(() => SceneManager.LoadScene("LoadingScene"));
        }
        else {
            openSaran(true);
        }
    }

    public void Mute(bool mute) {
        if(mute) {
            PlayerPrefs.SetFloat("Volume", 0.0f);
            this.mute.gameObject.SetActive(false);
            this.unmute.gameObject.SetActive(true);
        }
        else {
            PlayerPrefs.SetFloat("Volume", 0.5f);
            this.mute.gameObject.SetActive(true);
            this.unmute.gameObject.SetActive(false);
        }
        audio.volume = PlayerPrefs.GetFloat("Volume");
    }
}
