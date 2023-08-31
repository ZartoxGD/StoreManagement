using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoteManagementLab
{
    internal class Product
    {
        private int id;
        private string name;
        private int price;
        private int promoPercent;
        private int taxPercent;
        private int stock;
        private string serviceName;

        public Product(int id, string serviceName, int promoPercent, int price, int taxPercent, string name, int stock)
        {
            this.serviceName = serviceName;
            this.id = id;
            this.promoPercent = promoPercent;
            this.price = price;
            this.taxPercent = taxPercent;
            this.name = name;
            this.stock = stock;
        }

        public override string ToString()
        {
            return $"[{Id}:{ServiceName}] {Name} (€: {Price} | Promo %: {PromoPercent} | Tax %: {TaxPercent} | Stock: {Stock})";
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int Stock { get => stock; set => stock = value; }
        public int Price { get => price; set => price = value; }
        public int PromoPercent { get => promoPercent; set => promoPercent = value; }
        public int TaxPercent { get => taxPercent; set => taxPercent = value; }
        public string ServiceName { get => serviceName; set => serviceName = value; }
    }
}
