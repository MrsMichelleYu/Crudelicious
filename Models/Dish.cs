using System.ComponentModel.DataAnnotations;
using System;

namespace Crudelicious.Models
{
    public class Dish
    {
        // auto-implemented properties need to match the columns in your table
        // the [Key] attribute is used to mark the Model property being used for your table's Primary Key
        [Key]
        public int DishId { get; set; }
        // MySQL VARCHAR and TEXT types can be represeted by a string
        [Required]
        public string Name { get; set; }
        [Required]
        public string Chef { get; set; }
        [Required]
        [Range(1, 5)]
        public int? Tastiness { get; set; }
        [Required]
        [Range(1, 10000, ErrorMessage = "Calories must between 0 and 10,000")]
        public int? Calories { get; set; }
        [Required]
        public string Description { get; set; }
        // The MySQL DATETIME type can be represented by a DateTime
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}