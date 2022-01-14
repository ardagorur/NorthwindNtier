﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NorthwindNtierBL.DTOs;
using NorthwindNtierDAL.Repository;
using NorthwindNtierDAL.Context;

namespace NorthwindNtierUI
{
    public partial class Form1 : Form
    {
        ProductRepository proRepo = new ProductRepository();
        CategoryRepository catRepo = new CategoryRepository();
        int choosen1 = 0;
        Product updateProduct = new Product();
        public Form1()
        {
            InitializeComponent();
            FillTable(1);
            FillCb();
        }

        private void FillTable(int id)
        {
            Refresh_();
            foreach (var item in proRepo.GetProductsByCatID(id))
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Tag = item.ProductID;
                lvi.Text = item.ProductName;
                lvi.SubItems.Add(item.UnitPrice.ToString());
                lvi.SubItems.Add(item.UnitsInStock.Value.ToString());
                lvi.SubItems.Add(item.CategoryName);
                listView1.Items.Add(lvi);
            }
        }
        public void Refresh_()
        {
            listView1.Items.Clear();
        }
        public void FillCb()
        {
            cmbGuncellemeKategoriler.DataSource = catRepo.GetCategories();
            cmbGuncellemeKategoriler.DisplayMember = "CategoryName";
            cmbGuncellemeKategoriler.ValueMember = "CategoryID";
            cmbKategoriler.DataSource = catRepo.GetCategories();
            cmbKategoriler.DisplayMember = "CategoryName";
            cmbKategoriler.ValueMember = "CategoryID";
            cmbUrunEklemeKategorler.DataSource = catRepo.GetCategories();
            cmbUrunEklemeKategorler.DisplayMember = "CategoryName";
            cmbUrunEklemeKategorler.ValueMember = "CategoryID";
        }

        private void cmbKategoriler_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int choosenId = 1;
                choosenId = Convert.ToInt32(cmbKategoriler.SelectedValue);
                FillTable(choosenId);
            }
            catch 
            {
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            Product p = new Product();
            p.ProductName = txtEklemeUrunAd.Text;
            p.UnitPrice = Convert.ToInt32(txtEklemeFiyat.Text);
            p.UnitsInStock = (short?)nudEklemeStok.Value;
            p.CategoryID = (int)cmbUrunEklemeKategorler.SelectedValue;
            proRepo.Add(p);
            proRepo.Update();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            updateProduct.ProductName = txtGuncellemeUrunAd.Text;
            updateProduct.UnitPrice = Convert.ToInt32(txtGuncellemeFiyat.Text);
            updateProduct.UnitsInStock = (short)nudGuncellemeStok.Value;
            updateProduct.CategoryID = Convert.ToInt32(cmbGuncellemeKategoriler.SelectedValue);
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            string id = "xxx";
            //secilenId = Convert.ToInt32(listView1.Items[0].Text);
            if (e.Button == MouseButtons.Right)
            {
                if (listView1.FocusedItem != null && listView1.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    ContextMenu m = new ContextMenu();
                    MenuItem cashMenuItem = new MenuItem("Güncelle");
                    cashMenuItem.Tag = listView1.FocusedItem.Tag;
                    m.MenuItems.Add(cashMenuItem);
                    cashMenuItem.Click += delegate (object sender2, EventArgs e2)
                    {
                        ActionClick(sender, e, id);
                    };

                    MenuItem delMenuItem = new MenuItem("Sil");
                    delMenuItem.Click += delegate (object sender2, EventArgs e2) {
                        DelectAction(sender, e, id);
                    };// 
                    m.MenuItems.Add(delMenuItem);

                    m.Show(listView1, new Point(e.X, e.Y));

                }
            }
        }

        private void DelectAction(object sender, MouseEventArgs e, string id)
        {
            foreach (ListViewItem eachItem in listView1.SelectedItems)
            {
                // you can use this idea to get the ListView header's name is 'Id' before delete
                Console.WriteLine(GetTextByHeaderAndIndex(listView1, "Id", eachItem.Index));
                listView1.Items.Remove(eachItem);
            }
        }

        private void ActionClick(object sender, MouseEventArgs e, string id)
        {
            choosen1 = Convert.ToInt32(listView1.SelectedItems[0].Tag);
            updateProduct = proRepo.Find(choosen1);
            txtGuncellemeUrunAd.Text = updateProduct.ProductName;
            txtGuncellemeFiyat.Text = updateProduct.UnitPrice.ToString();
            nudGuncellemeStok.Value = (decimal)updateProduct.UnitsInStock;
            cmbGuncellemeKategoriler.SelectedValue = updateProduct.CategoryID;
        }
        public static string GetTextByHeaderAndIndex(ListView listViewControl, string headerName, int index)
        {


            int headerIndex = -1;
            foreach (ColumnHeader header in listViewControl.Columns)
            {
                if (header.Name == headerName)
                {
                    headerIndex = header.Index;
                    break;
                }
            }
            if (headerIndex > -1)
            {
                return listViewControl.Items[index].SubItems[headerIndex].Text;
            }
            return null;
        }
    }
}