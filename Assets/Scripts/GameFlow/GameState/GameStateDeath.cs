
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStateDeath : GameState
{
    public GameObject DeathUI;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI fishText;
    [SerializeField] private TextMeshProUGUI hightScoreText;
    [SerializeField] private TextMeshProUGUI totalFish;
    [SerializeField] private Image completionCircle;
    public float timeToDecision = 3.0f;
    private float deathTime;
    public override void Construct()
    {
        base.Construct();
        GameManager.Instance.motor.PausePlayer();

        deathTime = Time.time;
        DeathUI.SetActive(true);
        completionCircle.gameObject.SetActive(true);
        //Prior to saving, set the highscore if needed
        if (SaveManager.Instance.save.Highscore < (int)GameStats.Instance.score)
        {
            SaveManager.Instance.save.Highscore = (int)GameStats.Instance.score;
            scoreText.color = Color.green;
        }
        else
        {
            scoreText.color = Color.white;
        }
        SaveManager.Instance.save.Fish += GameStats.Instance.fishCollectedThisSession;
        SaveManager.Instance.Save();
        scoreText.text = GameStats.Instance.ScoreToText();
        fishText.text = GameStats.Instance.FishToText();
        hightScoreText.text = "High Score: " + SaveManager.Instance.save.Highscore.ToString();
        totalFish.text = "Total Fish: " + SaveManager.Instance.save.Fish.ToString();
    }
    public override void Destruct()
    {
        DeathUI.SetActive(false);
    }
    public override void UpdateState()
    {
        float ratio = (Time.time - deathTime) / timeToDecision;
        completionCircle.color = Color.Lerp(Color.green, Color.red, ratio);
        completionCircle.fillAmount = 1 - ratio;

        if (ratio > 1)
        {
            completionCircle.gameObject.SetActive(false);
        }
    }
    public void ResumeGame()
    {
        brain.ChangeState(GetComponent<GameStateGame>());
        GameManager.Instance.motor.RespawnPlayer();
    }
    public void ToMenu()
    {

        brain.ChangeState(GetComponent<GameStateInit>());
        GameManager.Instance.motor.ResetPlayer();
        GameManager.Instance.worldGeneration.ResetWorld();
        GameManager.Instance.sceneChunkGeneration.ResetWorld();


    }
}
