using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class QuizController : MonoBehaviour {

    Dictionary<string, int> answers = new Dictionary<string, int>() {
        { "Sanguine", 0},
        { "Phlegmatic", 0},
        { "Choleric", 0},
        { "Melancholic", 0 }
    };
    public Image layer;
    public GameObject result, detail;
    public List<GameObject> quiz = new List<GameObject>();
    public GameObject[] characters;
    private List<string> characterPossibleResult = new List<string>();
    private int currentQuiz = 0;
    private AudioSource audio;
    public AudioClip resultMusic;

    void Start() {
        audio = GetComponent<AudioSource>();
        audio.volume = PlayerPrefs.GetFloat("Volume");
        layer.DOFade(0f, 0.25f);
    }

    public void Next() {
        currentQuiz++;
        for (int i = 0; i < quiz.Count; i++) {
            quiz[i].GetComponent<RectTransform>().DOAnchorPosX((i * 1080) - 1080 * currentQuiz, 0.25f);
        }
    }

    public void Result() {
        audio.Stop();
        foreach (KeyValuePair<string, int> answer in answers) {
            if(answer.Value == answers.Values.Max()) {
                characterPossibleResult.Add(answer.Key);
            }
        }
        int random = Random.Range(0, characterPossibleResult.Count);
        GameObject characterResult = null;
        switch (characterPossibleResult[random]) {
            case "Sanguine":
                characterResult = characters[0];
                break;
            case "Phlegmatic":
                characterResult = characters[1];
                break;
            case "Melancholic":
                characterResult = characters[2];
                break;
            case "Choleric":
                characterResult = characters[3];
                break;
        }
        //audio.clip = characterResult.GetComponent<Character>().bgm;
        audio.clip = resultMusic;
        audio.Play();
        PlayerPrefs.SetString("Character", characterPossibleResult[random]);        
        result.GetComponent<RawImage>().texture = characterResult.GetComponent<Character>().result;
        detail.GetComponent<RawImage>().texture = characterResult.GetComponent<Character>().detailButton;
        result.GetComponent<RectTransform>().DOAnchorPosX(10800 - 1080 * currentQuiz, 0.25f);
    }

    public void Pick() {
        GameObject button = EventSystem.current.currentSelectedGameObject;
        answers[button.name]++;
        Next();
        if(currentQuiz > quiz.Count - 1) {
            Result();
        }
    }

    public void Back() {
        LoadingController.scene = "BeforeQuizScene";
        audio.DOFade(0f, 0.25f).SetDelay(0.25f);
        layer.DOFade(1f, 0.5f).OnComplete(() => SceneManager.LoadScene("LoadingScene"));
    }

    public void Detail() {
        CharacterController.character = PlayerPrefs.GetString("Character");
        layer.DOFade(1f, 0.25f).OnComplete(() => SceneManager.LoadScene("CharacterScene", LoadSceneMode.Additive));
    }
}
