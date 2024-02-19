using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GenerateRow : MonoBehaviour
{
    public static readonly int CELL_NUMBER = 5;
    public static readonly int NUMBER_OF_ELEMENTS = 10;
    public static Action onGeneratedSlots;


    [SerializeField] private Button buttonSpin;
    [SerializeField] private ElementSprite elements;

    private Column column;

    private bool _spin = false;
    private int[] _elementInColumn = new int[CELL_NUMBER];
    public int[,] MatrixOfElements = new int[CELL_NUMBER, CELL_NUMBER];
    public Dictionary<int, int> Combination = new Dictionary<int, int>();
    private int _columnNumber = 0;
    private int timer = 0;

    private void Awake()
    {
        for(int i = 0; i < NUMBER_OF_ELEMENTS; ++i)
        {
            Combination.Add(i, 0);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        column = GetComponent<Column>();
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
                if (_columnNumber == CELL_NUMBER)
                {
                    CheckCombination();
                    onGeneratedSlots?.Invoke();
                    _spin = false;
                    buttonSpin.enabled = true;
                }
            }
        }
    }

    private void SpinTheColumn(int columnNumber)
    {
        Image[] columnArray;
        for (int j = columnNumber; j < CELL_NUMBER; ++j)
        {
            for (int i = 0; i < CELL_NUMBER; ++i)
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
        for (int i = 0; i < CELL_NUMBER; ++i)
        {
            Image[] columnArray = Column.GetColumnSymbolArray[columnNumber];
            columnArray[i].sprite = elements.element[elementIndex[i]];
            MatrixOfElements[i, columnNumber] = elementIndex[i];
        }
    }

    private int[] Generator()
    {
        for (int i = 0; i < CELL_NUMBER; ++i)
            _elementInColumn[i] = UnityEngine.Random.Range(0, NUMBER_OF_ELEMENTS);

        return _elementInColumn;
    }

    public void SpinButton()
    {
        _columnNumber = 0;
        _spin = true;
        buttonSpin.enabled = false;
        ClearCells();
    }

    private void ClearCells()
    {
        for (int j = 0; j < CELL_NUMBER; ++j)
        {
            for (int i = 0; i < CELL_NUMBER; ++i)
            { 
                Image[] columnArray = column.GetColumnCellArray[j];

                columnArray[i].color = new Color(1f, 1f, 1f);
            }
        }
    }

    private void CheckCombination()
    {
        int _currentColumn = 1;
        for (int j = 0; j < CELL_NUMBER; ++j)
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
        for (int i = 0; i < CELL_NUMBER; ++i)
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

        for (int j = 1; j < CELL_NUMBER; ++j)
        {
            for (int i = 0; i < CELL_NUMBER; ++i)
            {
                if (MatrixOfElements[i, j] == currentElement)
                {
                    Image[] columnArray = column.GetColumnCellArray[j];

                    columnArray[i].color = new Color(0.67f, 0.54f, 0.54f);

                    //Combination[currentElement]++;
                    //Debug.Log("Combination " + Combination[currentElement]);
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
        Image[] columnArray0 = column.GetColumnCellArray[0];
        for (int i = 0; i < CELL_NUMBER; ++i)
        {
            for (int j = 0; j < CELL_NUMBER; ++j)
            {
                if (MatrixOfElements[i, 0] == array[j, 1])
                {
                    Combination[array[j, 1]]++;
                    columnArray0[i].color = new Color(0.67f, 0.54f, 0.54f);
                }
            }
        }
    }
}
