
using TMPro;
using UnityEngine;

public class GameStateInit : GameState
{
    public GameObject menuUI;
    [SerializeField] private TextMeshProUGUI hisScoreText;
    [SerializeField] private TextMeshProUGUI fishScoreText;
    public override void Construct()
    {
        GameManager.Instance.ChangeCamera(GameCamera.Init);

        hisScoreText.text = "High Score: " + SaveManager.Instance.save.Highscore.ToString();
        fishScoreText.text = "Fish: " + SaveManager.Instance.save.Fish.ToString();

        menuUI.SetActive(true);
    }
    public override void Destruct()
    {
        menuUI.SetActive(false);
    }
    public void OnPlayClick()
    {
        brain.ChangeState(GetComponent<GameStateGame>());
        GameStats.Instance.ResetSession();
    }
    public void OnShopClick()
    {
        brain.ChangeState(GetComponent<GameStateShop>());

    }

}
