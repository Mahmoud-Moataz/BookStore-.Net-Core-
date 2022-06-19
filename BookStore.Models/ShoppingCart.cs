﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.ViewModels
{
  public class ShoppingCart
  {
    public Product Product { get; set; }

    [Range(1, 1000, ErrorMessage = "Please Enter a valid number between 1and 1000")]
    public int Count { get; set; }
  }
}