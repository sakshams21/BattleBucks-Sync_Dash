using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup Loading_CanvasGroup;
    public void StartGame()
    {
        Loading_CanvasGroup.alpha = 0;
        Loading_CanvasGroup.gameObject.SetActive(true);
        Loading_CanvasGroup.DOFade(1, 1f).OnComplete(() =>
        {
            SceneManager.LoadSceneAsync(1);
        });

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
