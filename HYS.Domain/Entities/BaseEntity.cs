using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYS.Domain.Entities
{
    public class BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Dapper.Contrib.Extensions.Key]
        public int Id { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string AddedBy { get; set; }
    }
}
