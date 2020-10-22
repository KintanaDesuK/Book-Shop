using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Shop.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public bool Available { get; set; }
        public string Image { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Categories { get; set; }

        [Display(Name = "Publisher")]
        public int PublisherId { get; set; }
        [ForeignKey("PublisherId")]
        public virtual Publisher Publishers { get; set; }

        [Display(Name = "Author")]
        public int AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public virtual Author Authors { get; set; }
    }
}