using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GridExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitCustomComponents();
        }

        private void InitCustomComponents()
        {
            InitColors();
            InitSizes();
            InitGridView();
        }

        private void InitGridView()
        {
            DataGridViewTextBoxColumn quantityColumn = new DataGridViewTextBoxColumn();
            quantityColumn.DataPropertyName = "Quantity";
            quantityColumn.Name = "Quantity";
            quantityColumn.HeaderText = "Quantity";

            DataGridViewComboBoxColumn colorColumn = new DataGridViewComboBoxColumn();
            colorColumn.DataPropertyName = "ItemColor";
            colorColumn.Name = "ItemColor";
            colorColumn.HeaderText = "ItemColor";
            colorColumn.DataSource = colors;
            colorColumn.DisplayMember = "Name";
            colorColumn.ValueMember = "This";

            DataGridViewComboBoxColumn sizesColumn = new DataGridViewComboBoxColumn();
            sizesColumn.DataPropertyName = "ItemSize";
            sizesColumn.Name = "ItemSize";
            sizesColumn.HeaderText = "ItemSize";
            sizesColumn.DataSource = sizes;
            sizesColumn.DisplayMember = "Name";
            sizesColumn.ValueMember = "This";

            DataGridViewTextBoxColumn noteColumn = new DataGridViewTextBoxColumn();
            noteColumn.DataPropertyName = "Note";
            noteColumn.Name = "Note";
            noteColumn.HeaderText = "Note";

            DataGridViewTextBoxColumn quantityColumn2 = new DataGridViewTextBoxColumn();
            quantityColumn2.DataPropertyName = "Quantity2";
            quantityColumn2.Name = "Quantity2";
            quantityColumn2.HeaderText = "Quantity2";

            DataGridViewTextBoxColumn quantityColumn3 = new DataGridViewTextBoxColumn();
            quantityColumn3.DataPropertyName = "Quantity3";
            quantityColumn3.Name = "Quantity3";
            quantityColumn3.HeaderText = "Quantity3";

            DataGridViewTextBoxColumn quantityColumn4 = new DataGridViewTextBoxColumn();
            quantityColumn4.DataPropertyName = "Quantity4";
            quantityColumn4.Name = "Quantity4";
            quantityColumn4.HeaderText = "Quantity4";

            //myGridView.Columns.Add(quantityColumn);
            //myGridView.Columns.Add(colorColumn);
            //myGridView.Columns.Add(sizesColumn);
            //myGridView.Columns.Add(noteColumn);

            myGridView.Columns.Add(quantityColumn);
            myGridView.Columns.Add(quantityColumn2);
            myGridView.Columns.Add(quantityColumn3);
            myGridView.Columns.Add(quantityColumn4);
        }

        private void InitColors()
        {
            colors = new List<ItemColor>();
            colors.Add(new ItemColor(1, "Red", Color.Red));
            colors.Add(new ItemColor(2, "Green", Color.Green));
            colors.Add(new ItemColor(3, "Yellow", Color.Yellow));
        }

        private void InitSizes()
        {
            sizes = new List<ItemSize>();
            sizes.Add(new ItemSize(1, "Small", 1));
            sizes.Add(new ItemSize(2, "Medium", 5));
            sizes.Add(new ItemSize(3, "Large", 10));
        }

        private void btnSaveOrder_Click(object sender, EventArgs e)
        {
            //orderedItems = new List<OrderItem>();

            //foreach (DataGridViewRow row in myGridView.Rows)
            //{
            //    if (!row.IsNewRow)
            //    {
            //        OrderItem itemOrdered = new OrderItem();
            //        itemOrdered.Quantity = Convert.ToInt32(row.Cells["Quantity"].Value);
            //        itemOrdered.ItemColor = row.Cells["ItemColor"].Value as ItemColor;
            //        itemOrdered.ItemSize = row.Cells["ItemSize"].Value as ItemSize;

            //        orderedItems.Add(itemOrdered);
            //    }
            //}

            
            inputedItems = new SortedList<int, double[]>();
            double[] rowData;

            for (int i = 0; i < rowNum; i++)
            {
                rowData = new double[columnNum];

                for (int j = 0; j < columnNum; j++)
                {
                    rowData[j] = Convert.ToDouble(myGridView.Rows[i].Cells[j].Value);
                }
                
                inputedItems.Add(i, rowData);
            }
        }

        private void btnClearGridView_Click(object sender, EventArgs e)
        {
            myGridView.DataSource = null;
            myGridView.Rows.Clear();
            myGridView.Columns.Clear();
        }

        private void btnLoadSavedData_Click(object sender, EventArgs e)
        {
            if (myGridView.Columns.Count <= 0)
            {
                InitGridView();
            }

            // myGridView.DataSource = orderedItems;

            // 사용자가 입력한 데이터를 추출한다.
            var data = new double[rowNum, columnNum];

            for (int i = 0; i < rowNum; i++)
            {
                for (int j = 0; j < columnNum; j++)
                {
                    data[i, j] = Convert.ToDouble(inputedItems[i].GetValue(j));
                }
            }

            for (int rowIndex = 0; rowIndex < rowNum; rowIndex++)
            {
                var row = new DataGridViewRow();

                for (int columnIndex = 0; columnIndex < columnNum; columnIndex++)
                {
                    row.Cells.Add(new DataGridViewTextBoxCell()
                    {
                        Value = data[rowIndex, columnIndex]
                    });
                }
                myGridView.Rows.Add(row);
            }
        }

        private IList<ItemSize> sizes;
        private IList<ItemColor> colors;
        private SortedList<int, double[]> inputedItems;
        int rowNum = 1;
        int columnNum = 1;
    }
}

public class ItemColor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Color ColorValue { get; set; }
    public ItemColor This { get; private set; }

    public ItemColor() { }
    public ItemColor(int id, string name, Color value)
    {
        Id = id;
        Name = name;
        ColorValue = value;
        This = this;
    }
}

public class ItemSize
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int SizeValue { get; set; }
    public ItemSize This { get; private set; }

    public ItemSize() { }
    public ItemSize(int id, string name, int value)
    {
        Id = id;
        Name = name;
        SizeValue = value;
        This = this;
    }
}

public class OrderItem
{
    public int Quantity { get; set; }
    public ItemColor ItemColor { get; set; }
    public ItemSize ItemSize { get; set; }

    public OrderItem() { }
    public OrderItem(int quantity, ItemColor color, ItemSize size)
    {
        Quantity = quantity;
        ItemColor = color;
        ItemSize = size;
    }
}

