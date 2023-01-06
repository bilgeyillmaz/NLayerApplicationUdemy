﻿namespace NLayer.Core;

public class Category:BaseEntity
{
    public string Name { get; set; }
    //navigation property
    public ICollection<Product> Products { get; set; }
}