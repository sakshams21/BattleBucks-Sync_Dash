using System;
using DG.Tweening;
using TMPro;
using UnityEngine;


public class UI_Manager : MonoBehaviour
{
    [Header("Loading")]
    [SerializeField] private Canvas Loading_Canvas;
    [SerializeField] private CanvasGroup Loading_CanvasGroup;

    [Header("Score & Lives")]
    [SerializeField] private TextMeshProUGUI Score_Text;
    [SerializeField] private TextMeshProUGUI Lives_Text;

    [Space(7)]
    [Header("Game Over")]
    [SerializeField] private CanvasGroup GameOver_CanvasGroup;

    [SerializeField] private TextMeshProUGUI GameOverScore_Text;


    private void Start()
    {
        GameManager.Instance.OnScoreUpdate += ScoreUpdate;
        GameManager.Instance.OnLivesUpdate += LivesUpdate;
        GameManager.Instance.OnGameOver += GameoverScreen;
    }



    private void OnDestroy()
    {
        GameManager.Instance.OnScoreUpdate -= ScoreUpdate;
        GameManager.Instance.OnLivesUpdate -= LivesUpdate;
        GameManager.Instance.OnGameOver -= GameoverScreen;
    }

    private void GameoverScreen(int endScore)
    {
        GameOver_CanvasGroup.alpha = 0;
        GameOver_CanvasGroup.gameObject.SetActive(true);
        GameOverScore_Text.text = endScore.ToString();
        GameOver_CanvasGroup.DOFade(1, 1f);
    }

    private void LivesUpdate(int remainingLives)
    {
        Lives_Text.text = $"Lives: {remainingLives}";
    }
    private void ScoreUpdate(int score)
    {
        Score_Text.text = $"Score: {score}";
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
        ScoreUpdate(0);
        LivesUpdate(GameManager.Instance.Ref_LevelData.TotalLives);
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

    // public void PauseGame(bool isPause)
    // {
    //     GameManager.Instance.PauseGame(isPause);
    //}



}