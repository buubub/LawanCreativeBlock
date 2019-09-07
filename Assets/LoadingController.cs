using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LoadingController : MonoBehaviour {
    public RawImage loadingScreen;
    public Image layer;
    public Texture2D[] gif;
    public int fps = 60;
    public static string scene;

    void Awake() {
        DontDestroyOnLoad(loadingScreen);
        DontDestroyOnLoad(layer);
        DontDestroyOnLoad(gif[0]);
        DontDestroyOnLoad(gif[1]);
        DontDestroyOnLoad(gif[2]);
        DontDestroyOnLoad(gif[3]);
        DontDestroyOnLoad(gif[4]);
        DontDestroyOnLoad(gif[5]);
        DontDestroyOnLoad(gif[6]);
        DontDestroyOnLoad(gif[7]);
        DontDestroyOnLoad(gif[8]);
        DontDestroyOnLoad(gif[9]);
        DontDestroyOnLoad(gif[10]);
    }

    void Start() {
        layer.DOFade(0f, 0.25f).OnComplete(()=> StartCoroutine(EnterScene()));
    }

    
    void Update() {
        int index = (int)((Time.time * fps) % gif.Length);
        loadingScreen.texture = gif[index];
        if (Input.GetKeyDown(KeyCode.Space)) {
            StartCoroutine(EnterScene());
        }
    }

    IEnumerator EnterScene() {
        AsyncOperation load = SceneManager.LoadSceneAsync(scene);
        load.allowSceneActivation = false;

        while (!load.isDone) {
            if (load.progress >= 0.9f) {
                break;
            }

            yield return null;
        }
        layer.DOFade(1f, 0.25f).OnComplete(()=>load.allowSceneActivation = true).SetDelay(0.25f);
    }
}
