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

            //Stok sayısı 100'den az olan ürün sayısı
            var productCountByStockCountSmallerThen100 = db.TblProduct.Where(x => x.ProductStock < 100).Count();
            lblProductStockSmallerThen100.Text = productCountByStockCountSmallerThen100.ToString();

            //Kategorisi sebze ve durumu aktif(true) olan ürün stok toplamı
            int id = db.TblCategory.Where(x => x.CategoryName == "Sebze").Select(y => y.CategoryId).FirstOrDefault();
            var productStockCountByCategoryNameIsSebzeAndStatusIsTrue = db.TblProduct.Where(x => x.CategoryId == (db.TblCategory.Where(w => w.CategoryName == "Sebze").Select(y => y.CategoryId).FirstOrDefault()) && x.ProductStatus == true).Sum(y => y.ProductStock);
            lblProductCountByCategorySebzeAndStatusTrue.Text = productStockCountByCategoryNameIsSebzeAndStatusIsTrue.ToString();

            //Türkiye'den yapılan siparişler SQL Query        
            var orderCountFromTurkiye = db.Database.SqlQuery<int>("Select count(*) From TblOrder Where CustomerId In (Select CustomerId From TblCustomer Where CustomerCountry='Türkiye')").FirstOrDefault();
            lblOrderCountFromTurkiyeBySQL.Text = orderCountFromTurkiye.ToString();

            //Türkiye'den yapılan siparişler EF metodu
            var turkishCustomerIds = db.TblCustomer.Where(x => x.CustomerCountry == "Türkiye")
                                                 .Select(y => y.CustomerId)
                                                 .ToList();
            var orderCountFromTurkiyeWithEf = db.TblOrder.Count(z => turkishCustomerIds.Contains(z.CustomerId.Value));

            lblOrderCountFromTurkiyeByEf.Text = orderCountFromTurkiyeWithEf.ToString();

            //Siparişler içinde kategorisi meyve olan ürünlerin toplam satış fiyatı SQL sorgusu

            var orderTotalPriceByCategoryIsMeyve = db.Database.SqlQuery<decimal>("Select Sum(o.TotalPrice) From TblOrder o Join TblProduct p On o.ProductId=p.ProductId Join TblCategory c On p.CategoryId=c.CategoryId Where c.CategoryName='Meyve'").FirstOrDefault();
            lblOrderTotalPriceByCategoryIsMeyve.Text = orderTotalPriceByCategoryIsMeyve.ToString() + "₺";

            //Siparişler içinde kategorisi meyve olan ürünlerin toplam satış fiyatı Entity Framework Metodu

            var orderTotalPriceByCategoryIsMeyveWithEf = (from o in db.TblOrder
                                                          join p in db.TblProduct on o.ProductId equals p.ProductId
                                                          join c in db.TblCategory on p.CategoryId equals c.CategoryId
                                                          where c.CategoryName == "Meyve"
                                                          select o.TotalPrice).Sum();
            lblOrderTotalPriceByCategoryIsMeyveByEf.Text = orderTotalPriceByCategoryIsMeyveWithEf.ToString() + "₺";

            //Son eklenen ürünün adı
            var lastProductName = db.TblProduct.OrderByDescending(x => x.ProductId).Select(y => y.ProductName).FirstOrDefault();
            lblLastProductName.Text = lastProductName.ToString();

            //Son eklenen ürünün kategori adı
            var lastProductCategoryId = db.TblProduct.OrderByDescending(x => x.ProductId).Select(y => y.CategoryId).FirstOrDefault();
            var lastProductCategoryName = db.TblCategory.Where(x => x.CategoryId == lastProductCategoryId).Select(y => y.CategoryName).FirstOrDefault();
            lblLastProductCategoryName.Text = lastProductCategoryName.ToString();

            //Aktif ürün sayısı
            var activeProductCount = db.TblProduct.Where(x => x.ProductStatus == true).Count();
            lblActiveProductCount.Text = activeProductCount.ToString();

            //Toplam Kola Stok Satışlarından Kazanılan Para
            var colaStock = db.TblProduct.Where(x => x.ProductName == "Kola").Select(y => y.ProductStock).FirstOrDefault();
            var colaPrice = db.TblProduct.Where(x=>x.ProductName=="Kola").Select(y => y.ProductPrice).FirstOrDefault();
            var totalColaStockPrice = colaStock * colaPrice;
            lblTotalPriceWithStockByCola.Text = totalColaStockPrice + "₺";

            //Sistemde son sipariş veren müşteri adı
            var lastCustomerId = db.TblOrder.OrderByDescending(x => x.OrderId).Select(y => y.CustomerId).FirstOrDefault();
            var lastCustomerName = db.TblCustomer.Where(x => x.CustomerId == lastCustomerId).Select(y => y.CustomerName).FirstOrDefault();
            lblLastCustomerName.Text = lastCustomerName.ToString();

            //Ülke çeşitliliği sayısı
            var countryDifferentCount = db.TblCustomer.Select(x=>x.CustomerCountry).Distinct().Count();
            lblCountryDifferentCount.Text=countryDifferentCount.ToString();
        }
    }
}
//lblCountryDifferentCount