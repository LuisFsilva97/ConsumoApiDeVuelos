using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newshore.Viajes.Model.Model
{
    public class SearchHistory
    {
        [Key]
        public int Id { get; set; }
        public string Origin { get; set; }

        public string Destination { get; set; }

        public DateTime SearchDate { get; set; }
    }
}
