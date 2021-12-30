using Product.Domain.Interfaces;
using System;

namespace Product.Domain.Aggregates.ProductAgg
{
    public class Product : IAggregateRoot
    {
        public Product(Guid id, string name, string iMEI, string model, DateTime manufacturingDate, int memory, int storage, 
            int batteryLifeInMinutes, string manufacturer, Brand brand)
        {
            Id = id;
            Name = name;
            IMEI = iMEI;
            Model = model;
            ManufacturingDate = manufacturingDate;
            Memory = memory;
            Storage = storage;
            BatteryLifeInMinutes = batteryLifeInMinutes;
            Manufacturer = manufacturer;
            Brand = brand;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string IMEI { get; set; }
        public string Model { get; set; }
        public  DateTime ManufacturingDate { get; set; }
        public int Memory { get; set; }
        public int Storage { get; set; }
        public int BatteryLifeInMinutes { get; set; }
        public string Manufacturer { get; set; }
        public Brand Brand  { get; set; }

        public bool CheckImeiIsValid()
        {
            return IMEI.Length >= 30;
        }

        public bool WarrantyIsValid()
        {
            return true;
        }
    }
}
