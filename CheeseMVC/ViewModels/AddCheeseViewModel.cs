using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CheeseMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CheeseMVC.ViewModels
{
    public class AddCheeseViewModel
    {
        [Required]
        [Display(Name = "Cheese Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must provide a description")]
        public string Description { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryID { get; set; }

        public List<SelectListItem> Categories { get; set; }

        public AddCheeseViewModel(IEnumerable<CheeseCategory> categories)
        {
            this.Categories = new List<SelectListItem>();

            foreach (CheeseCategory category in categories)
            {
                // Generate a new SelectListItem
                // Add SelectListItem to this.Categories
                this.Categories.Add(new SelectListItem()
                {
                    Value = category.ID.ToString(),
                    Text = category.Name
                });
            }
        }

        public AddCheeseViewModel() { }

        public Cheese CreateCheese()
        {
            return new Cheese
            {
                Name = this.Name,
                Description = this.Description,
                CategoryID = this.CategoryID,
                Rating = this.Rating
            };
        }

    }
}
