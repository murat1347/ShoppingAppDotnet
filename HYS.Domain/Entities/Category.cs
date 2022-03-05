﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYS.Domain.Entities
{
    [Table("Category")]
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        //public virtual ICollection<Product> Products { get; set; }
    }
}
