using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateRow : MonoBehaviour
{
    public static readonly int NUMBER_OF_ELEMENTS = 5;

    [SerializeField] private Button buttonSpin;
    [SerializeField] private ElementSprite elements;
    
    private bool _spin = false;
    private int[] _elementInColumn = new int[NUMBER_OF_ELEMENTS];
    private int[,] MatrixOfElements = new int[NUMBER_OF_ELEMENTS, NUMBER_OF_ELEMENTS];
    private int _columnNumber = 0;
    private int timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (_spin)
        {
            timer += 1;
            SpinTheColumn(_columnNumber);
            if (timer == 60)
            {
                SetElement(_columnNumber);
                _columnNumber++;
                timer = 0;
                if (_columnNumber == NUMBER_OF_ELEMENTS)
                {
                    CheckCombination();
                    _spin = false;
                    buttonSpin.enabled = true;
                }
            }
        }
    }

    private void SpinTheColumn(int columnNumber)
    {
        Image[] columnArray;
        for (int j = columnNumber; j < NUMBER_OF_ELEMENTS; ++j)
        {
            for (int i = 0; i < NUMBER_OF_ELEMENTS; ++i)
            {
                int temp = UnityEngine.Random.Range(0, NUMBER_OF_ELEMENTS);
                Image[] images = Column.GetColumnSymbolArray[j];
                columnArray = images;
                columnArray[i].sprite = elements.element[temp];
            }
        }
    }

    private void SetElement(int columnNumber)
    {
        int[] elementIndex;

        elementIndex = Generator();
        for (int i = 0; i < NUMBER_OF_ELEMENTS; ++i)
        {
            Image[] columnArray = Column.GetColumnSymbolArray[columnNumber];
            columnArray[i].sprite = elements.element[elementIndex[i]];
            MatrixOfElements[i, columnNumber] = elementIndex[i];
        }
    }

    private int[] Generator()
    {
        for (int i = 0; i < NUMBER_OF_ELEMENTS; ++i)
            _elementInColumn[i] = UnityEngine.Random.Range(0, NUMBER_OF_ELEMENTS);

        return _elementInColumn;
    }

    public void SpinButton()
    {
        _columnNumber = 0;
        _spin = true;
        buttonSpin.enabled = false;
    }

    private void CheckCombination()
    {
        int _currentColumn = 1;
        for (int j = 0; j < NUMBER_OF_ELEMENTS; ++j)
        {
            CellColor(MatrixOfElements[j, 0]);
            while (_currentColumn < 5)
            {
                if (FindElementInColumn(_currentColumn, MatrixOfElements[j, 0]))
                {
                    _currentColumn++;
                }

                else
                {
                    break;
                }
            }
            _currentColumn = 1;
        }
    }

    private bool FindElementInColumn(int currentColumn, int currentElement)
    {
        for (int i = 0; i < NUMBER_OF_ELEMENTS; ++i)
        {
            if (MatrixOfElements[i, currentColumn] == currentElement)
            {
                return true;
            }
        }
        return false;
    }
    private void CellColor(int currentElement)
    {
        int count = 0;

        int combinationCount = 0;
        for (int j = 1; j < NUMBER_OF_ELEMENTS; ++j)
        {
            for (int i = 0; i < NUMBER_OF_ELEMENTS; ++i)
            {
                if (MatrixOfElements[i, j] == currentElement)
                {
                    Image[] columnArray = Column.GetColumnCellArray[j];

                    if (columnArray[i].color == new Color(0.8f, 0.8f, 1f))
                    {
                        continue;
                    }

                    else
                    {
                        columnArray[i].color = new Color(0.67f, 0.54f, 0.54f);
                    }

                    combinationCount++;
                }

                else
                    count++;
            }

            if (count == 5)
            {

                break;
            }
            count = 0;

        }

        PaintFirstColumn(MatrixOfElements);
    }

    private void PaintFirstColumn(int[,] array)
    {
        Image[] columnArray0 = Column.GetColumnCellArray[0];
        for (int i = 0; i < NUMBER_OF_ELEMENTS; ++i)
        {
            for (int j = 0; j < NUMBER_OF_ELEMENTS; ++j)
            {
                if (MatrixOfElements[i, 0] == array[j, 1])
                {
                    if (columnArray0[i].color == new Color(0.8f, 0.8f, 1f))
                    {
                        continue;
                    }
                    else
                    {
                        columnArray0[i].color = new Color(0.67f, 0.54f, 0.54f);
                    }
                }
            }
        }
    }
}
