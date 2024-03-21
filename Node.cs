using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFoodNutritionAI
{

    
    public class Node
    {

        // list of menu items that make a meal
        public List<MenuItem> Meal { get; private set; } = new List<MenuItem>();
        // sum of calories accross each all items in the meal
        public double TotalCalories { get; private set; }

        // IEnumerable allows us to iterate over a collection
        public Node(IEnumerable<MenuItem> meal)
        {
            Meal = new List<MenuItem>(meal);
            TotalCalories = Meal.Sum(item => item.Calories);
        }

        // makes new node by adding menuitem to current meal, giving a new potential 
        public Node AddItem(MenuItem item)
        {
            var newMeal = new List<MenuItem>(Meal) { item };  // list of menu items starting with items in current meal 
            //var newTotalCalories = TotalCalories + item.Calories;
            return new Node(newMeal);

        }
    }
}
