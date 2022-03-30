﻿using System.ComponentModel.DataAnnotations;

namespace TiendaOnline.Web.Models
{
    public class Country
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string Name { get; set; }
    }
}
