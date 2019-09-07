using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class CharacterController : MonoBehaviour {
    
    public static string character;
    public AudioClip sfxConfirm, sfxCancel;
    public Button detail;
    public Image layer;
    public List<GameObject> characterPage = new List<GameObject>();
    public GameObject[] characters;
    private int currentPage = 0;

    void Start() {
        layer.DOFade(0f, 0.25f);
        GameObject viewedCharacter = null;
        switch (character) {
            case "Sanguine":
                viewedCharacter = characters[0];
                break;
            case "Phlegmatic":
                viewedCharacter = characters[1];
                break;
            case "Melancholic":
                viewedCharacter = characters[2];
                break;
            case "Choleric":
                viewedCharacter = characters[3];
                break;
        }
        characterPage[0].GetComponent<RawImage>().texture = viewedCharacter.GetComponent<Character>().overview;
        characterPage[1].GetComponent<RawImage>().texture = viewedCharacter.GetComponent<Character>().detail[0];
        characterPage[2].GetComponent<RawImage>().texture = viewedCharacter.GetComponent<Character>().detail[1];
        detail.GetComponent<RawImage>().texture = viewedCharacter.GetComponent<Character>().detailButton;
    }

    void Update() {
        if (currentPage == characterPage.Count - 1) {
            detail.gameObject.SetActive(false);
        }
        else {
            detail.gameObject.SetActive(true);
        }
    }

    public void Next() {
        GameObject.Find("Controller").GetComponent<AudioSource>().PlayOneShot(sfxConfirm);
        currentPage++;
        for (int i = 0; i < characterPage.Count; i++) {
            characterPage[i].GetComponent<RectTransform>().DOAnchorPosX((i * 1080) - 1080 * currentPage, 0.25f);
        }
    }

    public void Prev() {
        GameObject.Find("Controller").GetComponent<AudioSource>().PlayOneShot(sfxCancel);
        if (currentPage == 0) {
            Back();
        }
        else {
            currentPage--;
            for (int i = 0; i < characterPage.Count; i++) {
                characterPage[i].GetComponent<RectTransform>().DOAnchorPosX((i * 1080) - 1080 * currentPage, 0.25f);
            }
        }
    }

    public void Back() {
        GameObject.Find("Controller").GetComponent<AudioSource>().PlayOneShot(sfxCancel);
        //if(SceneManager.GetActiveScene().name == "BeforeQuizScene") {
        //    GameObject beforeQuiz = GameObject.Find("Controller");
        //beforeQuiz.GetComponent<AudioSource>().Stop();
        //beforeQuiz.GetComponent<AudioSource>().clip = beforeQuiz.GetComponent<BeforeQuizController>().bgm;
        //beforeQuiz.GetComponent<AudioSource>().Play();
        //}
        GameObject.Find("Layer").GetComponent<Image>().DOFade(0f, 0.25f).SetDelay(0.25f);
        layer.DOFade(1f, 0.25f).OnComplete(() => SceneManager.UnloadSceneAsync("CharacterScene"));
    }
}
