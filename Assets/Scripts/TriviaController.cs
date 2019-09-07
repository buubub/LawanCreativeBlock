using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class TriviaController : MonoBehaviour {
    
    public Image layer;
    public List<GameObject> trivia = new List<GameObject>();
    public Button next, prev, back;
    private AudioSource audio;
    private int currentTrivia = 0;

    void Start() {
        audio = GetComponent<AudioSource>();
        audio.volume = PlayerPrefs.GetFloat("Volume");
        layer.DOFade(0f, 0.25f);
    }
    
    void Update() {
        if(currentTrivia == 0) {
            prev.gameObject.SetActive(false);
        }
        else {
            prev.gameObject.SetActive(true);
        }
        if(currentTrivia == trivia.Count-1) {
            next.gameObject.SetActive(false);
        }
        else {
            next.gameObject.SetActive(true);
        }
    }

    public void Next() {
        currentTrivia++;
        for (int i = 0; i < trivia.Count; i++) {
            trivia[i].GetComponent<RectTransform>().DOAnchorPosX((i * 1080) - 1080 * currentTrivia, 0.25f);
        }
    }

    public void Prev() {
        currentTrivia--;
        for (int i = 0; i < trivia.Count; i++) {
            trivia[i].GetComponent<RectTransform>().DOAnchorPosX((i * 1080) - 1080 * currentTrivia, 0.25f);
        }
    }

    public void Back() {
        LoadingController.scene = "MainMenuScene";
        audio.DOFade(0f, 0.25f).SetDelay(0.25f);
        layer.DOFade(1f, 0.5f).OnComplete(() => SceneManager.LoadScene("LoadingScene"));
    }
}
