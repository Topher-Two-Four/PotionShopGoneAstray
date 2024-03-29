using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{

    [SerializeField] private GameObject introCutscene;
    [Range(0.1f, 10f)] public float introCutsceneLength = 3.0f;

    [SerializeField] private GameObject newDayCutscene;
    [Range(0.1f, 10f)] public float newDayCutsceneLength = 3.0f;

    [SerializeField] private GameObject winGameCutscene;
    [Range(0.1f, 10f)] public float winGameCutsceneLength = 5.0f;

    [SerializeField] private GameObject loseGameCutscene;
    [Range(0.1f, 10f)] public float loseGameCutsceneLength = 5.0f;

    [SerializeField] private GameObject birdCatchCutscene;
    [Range(0.1f, 10f)] public float birdCatchCutsceneLength = 1.0f;

    [SerializeField] private GameObject musicManCatchCutscene;
    [Range(0.1f, 10f)] public float musicManCatchCutsceneLength = 1.0f;

    [SerializeField] private GameObject jellyCatchCutscene;
    [Range(0.1f, 10f)] public float jellyCatchCutsceneLength = 1.0f;

    [SerializeField] private GameObject fallCutscene;
    [Range(0.1f, 10f)] public float fallCutsceneLength = 1.0f;

    [SerializeField] private GameObject loadingCutscene;
    [Range(0.1f, 10f)] public float loadingCutsceneLength = 1.0f;

    public static CutsceneManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); } else { Instance = this; }
    }

    public void PlayIntroCutscene()
    {
        introCutscene.SetActive(true);
        Invoke("StopIntroCutscene", introCutsceneLength);
    }

    public void StopIntroCutscene()
    {
        introCutscene.SetActive(false);
    }

    public void PlayNewDayCutscene()
    {
        newDayCutscene.SetActive(true);
        Invoke("StopNewDayCutscene", newDayCutsceneLength);
    }

    public void StopNewDayCutscene()
    {
        newDayCutscene.SetActive(false);
    }

    public void PlayWinGameCutscene()
    {
        winGameCutscene.SetActive(true);
        Invoke("StopWinGameCutscene", winGameCutsceneLength);
    }

    public void StopWinGameCutscene()
    {
        winGameCutscene.SetActive(false);
    }

    public void PlayLoseGameCutscene()
    {
        loseGameCutscene.SetActive(true);
        Invoke("StopLoseGameCutscene", loseGameCutsceneLength);
    }

    public void StopLoseGameCutscene()
    {
        loseGameCutscene.SetActive(false);
    }

    public void PlayBirdCatchCutscene()
    {
        birdCatchCutscene.SetActive(true);
        Invoke("StopBirdCatchCutscene", birdCatchCutsceneLength);
    }

    public void StopBirdCatchCutscene()
    {
        birdCatchCutscene.SetActive(false);
    }

    public void PlayMusicManCatchCutscene()
    {
        musicManCatchCutscene.SetActive(true);
        Invoke("StopMusicManCatchCutscene", musicManCatchCutsceneLength);
    }

    public void StopMusicManCatchCutscene()
    {
        musicManCatchCutscene.SetActive(false);
    }

    public void PlayJellyCatchCutscene()
    {
        jellyCatchCutscene.SetActive(true);
        Invoke("StopJellyCatchCutscene", jellyCatchCutsceneLength);
    }

    public void StopJellyCatchCutscene()
    {
        jellyCatchCutscene.SetActive(false);
    }

    public void PlayFallCutscene()
    {
        fallCutscene.SetActive(true);
        Invoke("StopFallCutscene", fallCutsceneLength);
    }

    public void StopFallCutscene()
    {
        fallCutscene.SetActive(false);
    }

    public void PlayLoadingCutscene()
    {
        loadingCutscene.SetActive(true);
        Invoke("StopLoadingCutscene", loadingCutsceneLength);
    }

    public void StopLoadingCutscene()
    {
        loadingCutscene.SetActive(false);
    }

}
