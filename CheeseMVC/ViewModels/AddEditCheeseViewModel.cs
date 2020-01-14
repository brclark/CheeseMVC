using System;
using System.Collections.Generic;
using CheeseMVC.Models;

namespace CheeseMVC.ViewModels
{
    public class AddEditCheeseViewModel : AddCheeseViewModel
    {
        public int CheeseId { get; set; }

        public AddEditCheeseViewModel()
        {
        }

        public AddEditCheeseViewModel(Cheese ch, IEnumerable<CheeseCategory> categories) :
            base(categories)
        {
            // Use Cheese object to initialize the ViewModel properties
            CheeseId = ch.ID;
            Name = ch.Name;
            Description = ch.Description;
            CategoryID = ch.CategoryID;
            Rating = ch.Rating;
        }
    }
}
