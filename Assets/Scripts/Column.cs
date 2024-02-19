using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Column : MonoBehaviour
{
    public Image[] column1;
    public Image[] column2;
    public Image[] column3;
    public Image[] column4;
    public Image[] column5;

    public Image[] columnCell1;
    public Image[] columnCell2;
    public Image[] columnCell3;
    public Image[] columnCell4;
    public Image[] columnCell5;

    private List<Image[]> listOfColumns = new List<Image[]>();
    private List<Image[]> listOfCells = new List<Image[]>();
    public static List<Image[]> GetColumnSymbolArray
    {
        get;
        private set;
    }

    public List<Image[]> GetColumnCellArray
    {
        get;
        private set;
    }


    void Awake()
    {
        listOfColumns.Add(column1);
        listOfColumns.Add(column2);
        listOfColumns.Add(column3);
        listOfColumns.Add(column4);
        listOfColumns.Add(column5);

        listOfCells.Add(columnCell1);
        listOfCells.Add(columnCell2);
        listOfCells.Add(columnCell3);
        listOfCells.Add(columnCell4);
        listOfCells.Add(columnCell5);

        GetColumnSymbolArray = listOfColumns;
        GetColumnCellArray = listOfCells;
    }
}
