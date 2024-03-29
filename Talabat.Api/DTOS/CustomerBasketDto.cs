﻿using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entityes;

namespace Talabat.Api.DTOS
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; }
    }
}
 