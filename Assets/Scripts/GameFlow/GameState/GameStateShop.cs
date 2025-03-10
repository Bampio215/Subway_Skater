
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStateShop : GameState
{
    public GameObject shopUI;
    public TextMeshProUGUI totalFish;
    public TextMeshProUGUI curentHatName;
    public HatLogic hatLogic;
    private bool isInit = false;

    //Shop Item
    public GameObject hatprefab;
    public Transform hatContainer;

    private Hat[] hats;
    public override void Construct()
    {
        GameManager.Instance.ChangeCamera(GameCamera.Shop);
        hats = Resources.LoadAll<Hat>("Hat");
        shopUI.SetActive(true);

        totalFish.text = "x" + SaveManager.Instance.save.Fish.ToString("000");
        curentHatName.text = hats[SaveManager.Instance.save.currentHatindex].ItemName;
        if (!isInit)
        {
            PopulateShop();
            isInit = true;
        }

    }
    public override void Destruct()
    {
        shopUI.SetActive(false);
    }
    private void PopulateShop()
    {
        Debug.Log("Oki");
        for (int i = 0; i < hats.Length; i++)
        {
            int index = i;
            GameObject go = Instantiate(hatprefab, hatContainer);
            //Button
            go.GetComponent<Button>().onClick.AddListener(() => OnHatClick(index));
            //Thumbnail
            go.transform.GetChild(0).GetComponent<Image>().sprite = hats[index].Thumbnail;
            //ItemName
            go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = hats[index].ItemName;
            //Price
            if (SaveManager.Instance.save.UnlockedhatFlag[i] == 0)
            {
                go.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = hats[index].itemPrice.ToString();
            }
            else
            {
                go.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";

            }

        }
    }
    private void OnHatClick(int i)
    {
        if (SaveManager.Instance.save.UnlockedhatFlag[i] == 1)
        {
            SaveManager.Instance.save.currentHatindex = i;
            curentHatName.text = hats[i].ItemName;
            hatLogic.SelectHat(i);
            SaveManager.Instance.Save();
        }
        //If we don't have it, can we buy it?
        else if (hats[i].itemPrice <= SaveManager.Instance.save.Fish)
        {
            Debug.Log("Okiee");
            SaveManager.Instance.save.Fish -= hats[i].itemPrice;
            SaveManager.Instance.save.UnlockedhatFlag[i] = 1;
            SaveManager.Instance.save.currentHatindex = i;
            curentHatName.text = hats[i].ItemName;
            hatLogic.SelectHat(i);
            totalFish.text = "x" + SaveManager.Instance.save.Fish.ToString("000");
            SaveManager.Instance.Save();
            hatContainer.GetChild(i).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";

        }
        //Don't have it, can't buy it
        else
        {
            Debug.Log("Not enough fish");
        }

    }

    public void ToMenu()
    {
        brain.ChangeState(GetComponent<GameStateInit>());
    }
}
