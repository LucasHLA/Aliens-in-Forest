using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private string sceneNameNext;
    private AudioSource audioSRC;
    [SerializeField] private AudioClip click;

    void Start()
    {
        audioSRC = GetComponent<AudioSource>();
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void MoveScene()
    {
        //audioSRC.PlayOneShot(click);
        //SceneManager.LoadScene(sceneNameNext);
        StartCoroutine(moveSceneTime());
    }

    IEnumerator moveSceneTime()
    {
        audioSRC.PlayOneShot(click);
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(sceneNameNext);
    }
}
