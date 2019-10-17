using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tacticsoft;

public class TestTableViewController : MonoBehaviour, ITableViewDataSource
{
    public TestCounterCell m_cellPrefab;
    public TableView m_tableView;
    public RectTransform TableViewTransform;

    public int m_numRows;
    private int m_numInstancesCreated = 0;

    //Register as the TableView's delegate (required) and data source (optional)
    //to receive the calls
    void Start()
    {
        m_tableView.dataSource = this;
        Debug.Log("Screen width is " + Screen.width);
        if(Screen.width > 2700)
        {
            Debug.Log("Setting A");
            TableViewTransform.offsetMin = new Vector2(1800.0f, -400.0f);
            TableViewTransform.offsetMax = new Vector2(1200.0f, -200.0f);
        } else if(Screen.width <= 2700 && Screen.width > 2480)
        {
            Debug.Log("Setting B");
            TableViewTransform.offsetMin = new Vector2(1600.0f, -400.0f);
            TableViewTransform.offsetMax = new Vector2(1100.0f, -200.0f);
        } else if(Screen.width <= 2480 && Screen.width > 2000)
        {
            Debug.Log("Setting C");
            TableViewTransform.offsetMin = new Vector2(1450.0f, -350.0f);
            TableViewTransform.offsetMax = new Vector2(1000.0f, -200.0f);
        } else if(Screen.width <= 2000 && Screen.width > 1680)
        {
            Debug.Log("Setting D");
            TableViewTransform.offsetMin = new Vector2(1225.0f, -350.0f);
            TableViewTransform.offsetMax = new Vector2(800.0f, -200.0f);
        } else
        {
            Debug.Log("Setting E");
            TableViewTransform.offsetMin = new Vector2(850.0f, -250.0f);
            TableViewTransform.offsetMax = new Vector2(550.0f, -200.0f);
        }
        
    }

    #region ITableViewDataSource

    //Will be called by the TableView to know how many rows are in this table
    public int GetNumberOfRowsForTableView(TableView tableView)
    {
        return m_numRows;
    }

    //Will be called by the TableView to know what is the height of each row
    public float GetHeightForRowInTableView(TableView tableView, int row)
    {
        return (m_cellPrefab.transform as RectTransform).rect.height;
    }

    //Will be called by the TableView when a cell needs to be created for display
    public TableViewCell GetCellForRowInTableView(TableView tableView, int row)
    {
        TestCounterCell cell = tableView.GetReusableCell(m_cellPrefab.reuseIdentifier) as TestCounterCell;
        if (cell == null)
        {
            cell = (TestCounterCell)GameObject.Instantiate(m_cellPrefab);
            cell.name = "VisibleCounterCellInstance_" + (++m_numInstancesCreated).ToString();
        }
        cell.SetNineum(NineumManager.manager.GetNineumForEquipLocationAtIndex(EquipLocations.Weapon, row));
        return cell;
    }

    #endregion

    #region Table View event handlers

    //Will be called by the TableView when a cell's visibility changed
    public void TableViewCellVisibilityChanged(int row, bool isVisible)
    {
        //Debug.Log(string.Format("Row {0} visibility changed to {1}", row, isVisible));
        if (isVisible)
        {
            TestCounterCell cell = (TestCounterCell)m_tableView.GetCellAtRow(row);
        }
    }

    #endregion

}
