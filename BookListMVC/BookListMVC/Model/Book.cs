using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookListMVC.Model
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        [Required]
        [Display(Name = "Book Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Book Author")]
        public string Author { get; set; }
        public int Pages { get; set; }


    }
}
