using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReorderDatabase.Data.Entities
{
    internal class Note
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Text { get; set; }


        public ulong Numerator { get; set; }
        public ulong Denominator { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal Order { get; }
    }
}
