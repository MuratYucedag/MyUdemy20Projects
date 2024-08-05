using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project2_EntityFrameworkDbFirstProduct
{
    public partial class FrmProduct : Form
    {
        public FrmProduct()
        {
            InitializeComponent();
        }

        Db2Project20Entities db = new Db2Project20Entities();
        void ProductList()
        {
            dataGridView1.DataSource = db.TblProduct.ToList();
        }
        private void btnList_Click(object sender, EventArgs e)
        {
            ProductList();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            TblProduct tblProduct = new TblProduct();
            tblProduct.ProductPrice = decimal.Parse(txtProductPrice.Text);
            tblProduct.ProductStock = int.Parse(txtProductStock.Text);
            tblProduct.ProdutcName = txtProductName.Text;
            tblProduct.CategoryId = int.Parse(cmbProductCategory.SelectedValue.ToString());
            db.TblProduct.Add(tblProduct);
            db.SaveChanges();
            ProductList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var value = db.TblProduct.Find(int.Parse(txtProductId.Text));
            db.TblProduct.Remove(value);
            db.SaveChanges();
            ProductList();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var value = db.TblProduct.Find(int.Parse(txtProductId.Text));
            value.ProductPrice = decimal.Parse(txtProductPrice.Text);
            value.ProductStock = int.Parse(txtProductStock.Text);
            value.ProdutcName = txtProductName.Text;
            value.CategoryId = int.Parse(cmbProductCategory.SelectedValue.ToString());
            db.SaveChanges();
            ProductList();
        }
        private void FrmProduct_Load(object sender, EventArgs e)
        {
            var values = db.TblCategory.ToList();
            cmbProductCategory.DisplayMember = "CategoryName";
            cmbProductCategory.ValueMember = "CategoryId";
            cmbProductCategory.DataSource = values;
        }

        private void btnProductListWithCategory_Click(object sender, EventArgs e)
        {
            var values = db.TblProduct
                .Join(db.TblCategory,
                product => product.CategoryId,
                category => category.CategoryId,
                (product, category) => new
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProdutcName,
                    ProductPrice = product.ProductPrice,
                    ProductStock = product.ProductStock,
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName,
                })
                .ToList();
            dataGridView1.DataSource = values;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var values = db.TblProduct.Where(x => x.ProdutcName == txtProductName.Text).ToList();
            dataGridView1.DataSource = values;
        }
    }
}
