using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuotesDB.Models
{
    public class TagQuote
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey("TagId")]
        public int TagId { get; set; }
        public Tag Tag { get; set; }


        [ForeignKey("QuoteId")]
        public int QuoteId { get; set; }
        public Quote Quote { get; set; }
    }
}
