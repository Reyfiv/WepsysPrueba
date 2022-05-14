using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Token
    {
        [NotMapped]
        public string TokenG { get; set; }
        [NotMapped]
        public DateTime Expiration { get; set; }
    }
}
