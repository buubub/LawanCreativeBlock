using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.IO;

public class SaranController : MonoBehaviour {

    private AudioSource audio;
    public Image layer;
    public GameObject saran;
    private int currentSaran;
    private List<Texture> characterSaran = new List<Texture>();

    void Start() {
        audio = GetComponent<AudioSource>();
        audio.volume = PlayerPrefs.GetFloat("Volume");
        layer.DOFade(0f, 0.25f);
        characterSaran = GameObject.Find(PlayerPrefs.GetString("Character")).GetComponent<Character>().saran;
        currentSaran = Random.Range(0, characterSaran.Count);
        saran.GetComponent<RawImage>().texture = characterSaran[currentSaran];
        saran.GetComponent<RectTransform>().DOScale(1f, 0.5f);
    }

    public void SaranLagi() {
        characterSaran = GameObject.Find(PlayerPrefs.GetString("Character")).GetComponent<Character>().saran;
        while(true) {
            int random = Random.Range(0, characterSaran.Count);
            if (random != currentSaran) {
                currentSaran = random;
                break;
            }
        }
        saran.GetComponent<RectTransform>().DOScale(0f, 0.5f).OnComplete(()=> saran.GetComponent<RawImage>().texture = characterSaran[currentSaran]);
        saran.GetComponent<RectTransform>().DOScale(1f, 0.5f).SetDelay(0.5f);
    }

    public void Share() {
        Texture2D characterShare = GameObject.Find(PlayerPrefs.GetString("Character")).GetComponent<Character>().share;
        string filePath = Path.Combine(Application.temporaryCachePath, PlayerPrefs.GetString("Character").ToString() + ".png");
        File.WriteAllBytes(filePath, characterShare.EncodeToPNG());

        new NativeShare().AddFile(filePath).SetSubject("Lawan Creative Block!").SetText("Ternyata ini tipe kreatifku! Apa tipe kreatifmu? Yuk cari tahu di http://opensource.petra.ac.id/~m26415067/file/folder/public/APK/LawanCreativeBlock-fix.apk").Share();
    }

    public void Back() {
        LoadingController.scene = "MainMenuScene";
        audio.DOFade(0f, 0.25f);
        layer.DOFade(1f, 0.25f).OnComplete(() => SceneManager.LoadScene("LoadingScene"));
    }
}
