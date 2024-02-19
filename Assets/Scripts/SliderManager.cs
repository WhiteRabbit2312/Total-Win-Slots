using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    [SerializeField] private GameObject chooseSymbolPanel;
    [SerializeField] private Slider[] scoreSymbolSlider;
    

    [SerializeField] private ElementSprite element;
    [SerializeField] private Image[] otherChosenSymbolImage;
    private int chosenSymbol;
    private List<int> symbolInSlider = new List<int>();

    private void Awake()
    {
        GenerateRow.onGeneratedSlots += FillTheSliders;
        chooseSymbolPanel.SetActive(true);
    }

    private void FillTheSliders()
    {
        for(int i = 0; i < scoreSymbolSlider.Length; ++i)
        {
            CountScore(symbolInSlider[i], scoreSymbolSlider[i]);
        }
    }

    /*
               if(matrixOfElements[i, j] == chosenSymbol)
               {
                   scoreChosenSymbolSlider.value += 1;
               }

               if (matrixOfElements[i, j] == symbolInSlider.Peek())
               {

                   symbolInSlider.Dequeue();

               }*/

    private void CountScore(int symbol, Slider slider)
    {
        GenerateRow generateRow = GetComponent<GenerateRow>();

        int[,] matrixOfElements = generateRow.MatrixOfElements;

        for (int j = 0; j < GenerateRow.CELL_NUMBER; ++j)
        {
            for (int i = 0; i < GenerateRow.CELL_NUMBER; ++i)
            {
                if(matrixOfElements[j, i] == symbol)
                {
                    slider.value += 1f;
                }
            }
        }

        Debug.Log("Slider value " + scoreSymbolSlider);
    }

    public void ChosenSymbolButton(int chosenSymbol)
    {
        this.chosenSymbol = chosenSymbol;
        chooseSymbolPanel.SetActive(false);
        GenerateOtherSlider(chosenSymbol);
    }

    private void GenerateOtherSlider(int chosenSymbol)
    {
        int firstChosenElement = 0;
        otherChosenSymbolImage[firstChosenElement].sprite = element.element[chosenSymbol];
        symbolInSlider.Add(chosenSymbol);

        for (int i = 1; i < otherChosenSymbolImage.Length; ++i)
        {
            int randomSprite = GetRandomNumberExcluding(chosenSymbol);
            symbolInSlider.Add(randomSprite);
            otherChosenSymbolImage[i].sprite = element.element[randomSprite];
        } 
    }

    private int GetRandomNumberExcluding(int excludedValue)
    {
        int randomValue;
        do
        {
            randomValue = Random.Range(0, GenerateRow.NUMBER_OF_ELEMENTS);
        } while (randomValue == excludedValue);

        return randomValue;
    }
}
