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
        private string serviceName;
        private int promoPercent;
        private int price;
        private int taxPercent;
        private string name;

        public Product(int id, string serviceName, int promoPercent, int price, int taxPercent, string name)
        {
            this.id = id;
            this.serviceName = serviceName;
            this.promoPercent = promoPercent;
            this.price = price;
            this.taxPercent = taxPercent;
            this.name = name;
        }

        public int Id { get => id; set => id = value; }
        public string ServiceName { get => serviceName; set => serviceName = value; }
        public int PromoPercent { get => promoPercent; set => promoPercent = value; }
        public int Price { get => price; set => price = value; }
        public int TaxPercent { get => taxPercent; set => taxPercent = value; }
        public string Name { get => name; set => name = value; }
    }
}
