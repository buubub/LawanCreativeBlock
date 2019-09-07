using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class IntroController : MonoBehaviour {

    public List<GameObject> intro = new List<GameObject>();
    public Image layer;
    private int currentIntro = 0;
    private new AudioSource audio;

    void Start() {
        audio = GetComponent<AudioSource>();
        layer.DOFade(0f, 0.5f);
    }

    public void Next() {
        currentIntro++;
        for (int i = 0; i < intro.Count; i++) {
            intro[i].GetComponent<RectTransform>().DOAnchorPosX((i * 1080) - 1080 * currentIntro, 0.25f);
        }
        if(currentIntro == 1) {
            audio.Play();
        }
    }

    public void End() {
        audio.DOFade(0f, 2f);
        layer.DOFade(1f, 2f).OnComplete(toMainMenu);
        PlayerPrefs.SetInt("Played", 1);
    }

    private void toMainMenu() {
        LoadingController.scene = "MainMenuScene";
        SceneManager.LoadScene("LoadingScene");
    }
}
