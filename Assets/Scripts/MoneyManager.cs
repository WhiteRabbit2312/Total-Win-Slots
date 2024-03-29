using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bidText;
    [SerializeField] private TextMeshProUGUI wholeSumText;
    [SerializeField] private GameObject giveMoneyPanel;

    private int wholeSum;
    private int bid = 1000;
    // Start is called before the first frame update

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("WholeSum"))
        {
            PlayerPrefs.SetInt("WholeSum", 2000);
        }

        wholeSum = PlayerPrefs.GetInt("WholeSum");

        giveMoneyPanel.SetActive(false);

        if (wholeSum == 0)
        {
            wholeSum += 1000;
            PlayerPrefs.SetInt("WholeSum", wholeSum);
            giveMoneyPanel.SetActive(true);
        }

        wholeSum = PlayerPrefs.GetInt("WholeSum");
        wholeSumText.text = wholeSum.ToString();
        bidText.text = bid.ToString();
    }

    void Start()
    {
        GenerateRow.onGeneratedSlots += MoneyForCombination;
        GenerateRow.onGeneratedSlots += LevelResult;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MoneyForCombination()
    {
        GenerateRow generateRow = GetComponent<GenerateRow>();

        for(int i = 0; i < GenerateRow.NUMBER_OF_ELEMENTS; ++i)
        {
            int k = (bid / 5) * (generateRow.Combination[i] + 1);
            wholeSum += k;
            Debug.Log("bid " + generateRow.Combination[i]);
            wholeSumText.text = wholeSum.ToString();
        }

    }

    private void LevelResult()
    {
        SliderManager sliderManager = GetComponent<SliderManager>();

        if (sliderManager.SliderResult == "Win")
        {
            wholeSum += 10000;
            wholeSumText.text = wholeSum.ToString();
            SaveMoney();
        }

        else if(sliderManager.SliderResult == "Lose")
        {
            wholeSum -= 10000;
            wholeSumText.text = wholeSum.ToString();
            SaveMoney();
        }
    }

    public void Less()
    {
        if(bid > 1000)
        {
            bid -= 1000;
            bidText.text = bid.ToString();
        }
    }

    public void More()
    {
        if(bid < wholeSum - 1000)
        {
            bid += 1000;
            bidText.text = bid.ToString();
        }
    }

    public void CloseGiveMoneyPanel()
    {
        giveMoneyPanel.SetActive(false);
    }

    public void SpinAndTakeMoney()
    {
        wholeSum -= bid;
        SaveMoney();
    }

    private void SaveMoney()
    {
        PlayerPrefs.SetInt("WholeSum", wholeSum);
        wholeSum = PlayerPrefs.GetInt("WholeSum");
    }
}
