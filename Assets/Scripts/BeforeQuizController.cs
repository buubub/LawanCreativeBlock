using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class BeforeQuizController : MonoBehaviour {
    
    public Image layer;
    public RectTransform beforeQuiz, characterSelect;
    public GameObject ambilQuiz, ambilQuizLagi, resetPopUp;
    public GameObject[] characters;
    public AudioClip bgm;
    private AudioSource audio;

    void Start() {
        audio = GetComponent<AudioSource>();
        audio.volume = PlayerPrefs.GetFloat("Volume");
        layer.DOFade(0f, 0.25f);
        if (PlayerPrefs.HasKey("Character")) {
            ambilQuiz.SetActive(false);
            ambilQuizLagi.SetActive(true);
        }
    }

    public void StartQuiz() {
        LoadingController.scene = "QuizScene";
        audio.DOFade(0f, 0.25f).SetDelay(0.25f);
        layer.DOFade(1f, 0.5f).OnComplete(() => SceneManager.LoadScene("LoadingScene"));
    }

    public void ResetPopUp(bool open) {
        if(open) {
            resetPopUp.SetActive(true);
        }
        else {
            resetPopUp.SetActive(false);
        }
    }

    public void ResetQuiz() {
        PlayerPrefs.DeleteKey("Character");
        StartQuiz();
    }

    public void CharacterSelect() {
        beforeQuiz.DOAnchorPosX(-1080, 0.25f);
        characterSelect.DOAnchorPosX(0, 0.25f).SetDelay(0.5f);
    }

    public void CancelCharacterSelect() {
        beforeQuiz.DOAnchorPosX(0, 0.25f).SetDelay(0.5f);
        characterSelect.DOAnchorPosX(1080, 0.25f);
    }

    public void Back() {
        LoadingController.scene = "MainMenuScene";
        audio.DOFade(0f, 0.25f).SetDelay(0.25f);
        layer.DOFade(1f, 0.5f).OnComplete(() => SceneManager.LoadScene("LoadingScene"));
    }

    public void CharacterDetail(string character) {
        CharacterController.character = character;
        //audio.Stop();
        //switch(character) {
        //    case "Sanguine":
        //        audio.clip = characters[0].GetComponent<Character>().bgm;
        //        break;
        //    case "Phlegmatic":
        //        audio.clip = characters[1].GetComponent<Character>().bgm;
        //        break;
        //    case "Melancholic":
        //        audio.clip = characters[2].GetComponent<Character>().bgm;
        //        break;
        //    case "Choleric":
        //        audio.clip = characters[3].GetComponent<Character>().bgm;
        //        break;
        //}
        //audio.Play();
        layer.DOFade(1f, 0.25f).OnComplete(() => SceneManager.LoadScene("CharacterScene", LoadSceneMode.Additive));
    }
}
