using System;
using DG.Tweening;
using TMPro;
using UnityEngine;


public class UI_Manager : MonoBehaviour
{
    [SerializeField] private Canvas Loading_Canvas;
    [SerializeField] private CanvasGroup Loading_CanvasGroup;
    [SerializeField] private TextMeshProUGUI Score_Text;

    private void Start()
    {
        GameManager.Instance.OnScoreUpdate += ScoreUpdate;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnScoreUpdate -= ScoreUpdate;
    }

    private void ScoreUpdate(int score)
    {
        Score_Text.text = score.ToString();
    }

    public void StartLoading()
    {
        Loading_Canvas.enabled = true;
        Loading_CanvasGroup.alpha = 1;
        Loading_Canvas.gameObject.SetActive(true);
    }

    public void UnLoading()
    {
        Loading_Canvas.enabled = true;
        Loading_CanvasGroup.alpha = 1;
        Loading_Canvas.gameObject.SetActive(true);
        Loading_CanvasGroup.DOFade(0, 1f).OnComplete(() =>
        {
            Loading_Canvas.enabled = false;
            Loading_Canvas.gameObject.SetActive(false);
        });
    }

    public void ShowLoading(Action someAction)
    {
        Loading_Canvas.enabled = true;
        Loading_CanvasGroup.alpha = 1;
        Loading_Canvas.gameObject.SetActive(true);
        Loading_CanvasGroup.DOFade(0, 1f).OnComplete(() =>
        {
            Loading_Canvas.enabled = false;
            Loading_Canvas.gameObject.SetActive(false);
            someAction?.Invoke();
        });
    }

    public void PauseGame(bool isPause)
    {
        GameManager.Instance.PauseGame(isPause);
    }



}