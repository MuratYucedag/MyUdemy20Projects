using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project3_EntityFrameworkStatistics
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Db3Project20Entities db = new Db3Project20Entities();
        private void Form1_Load(object sender, EventArgs e)
        {
            //Toplam kategori sayısı
            int categoryCount = db.TblCategory.Count();
            lblCategoryCount.Text = categoryCount.ToString();

            //Toplam ürün sayısı
            int productCount = db.TblProduct.Count();
            lblProductCount.Text = productCount.ToString();

            //Toplam müşteri sayısı
            int customerCount = db.TblCustomer.Count();
            lblCustomerCount.Text = customerCount.ToString();

            //Toplam sipariş sayısı
            int orderCount = db.TblOrder.Count();
            lblOrderCount.Text = orderCount.ToString();

            //Toplam stok sayısı
            var totalProductStockCount = db.TblProduct.Sum(x => x.ProductStock);
            lblProductTotalStock.Text = totalProductStockCount.ToString();

            //Ortalama ürün fiyatı
            var averageProductPrice = db.TblProduct.Average(x => x.ProductPrice);
            lblProductAveragePrice.Text = averageProductPrice.ToString() + " ₺";

            //Toplam meyve stoğu sayısı
            var totalProductCountByCategoryIsFurit = db.TblProduct.Where(x => x.CategoryId == 1).Sum(y => y.ProductStock);
            lblProductCountByCategoryIsFruit.Text = totalProductCountByCategoryIsFurit.ToString();

            //Gazoz isimli ürünün toplam işlem hacmi
            var totalPriceByProductNameIsGazozGetStock = db.TblProduct.Where(x => x.ProductName == "Gazoz").Select(y => y.ProductStock).FirstOrDefault();
            var totalPriceByProductNameIsGazozGetUnitPrice = db.TblProduct.Where(x => x.ProductName == "Gazoz").Select(y => y.ProductPrice).FirstOrDefault();
            var totalPriceByProductNameIsGazoz = totalPriceByProductNameIsGazozGetStock * totalPriceByProductNameIsGazozGetUnitPrice;
            lblTotalPriceByProductNameIsGazoz.Text = totalPriceByProductNameIsGazoz.ToString() + " ₺";
        }
    }
}
