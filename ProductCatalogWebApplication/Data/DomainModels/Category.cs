﻿namespace ProductCatalogWebApplication.Data.DomainModels
{
    public class Category
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = null!;
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
