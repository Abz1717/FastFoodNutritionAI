using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFoodNutritionAI
{
    public class GreedySearch
    {
        //fields to hold the menu items, the heuristic, goal test and calorie limit
        private List<MenuItem> menuItems;
        private Func<MenuItem, double> heuristic;
        private Func<Node, bool> goalTest;
        private int calorieLimit;

        //constructor 
        public GreedySearch(List<MenuItem> menuItems, int calorieLimit, Func<MenuItem, double> heuristic, Func<Node, bool> goalTest)
        {
            this.menuItems = menuItems;
            this.calorieLimit = calorieLimit;
            this.heuristic = heuristic;
            this.goalTest = goalTest;

        }

        //method to find optimalmeal using heuristic and goal
        public List<MenuItem> findOptimalMeal()
        {
            var root = new Node(new List<MenuItem>()); // root node with empty meal
            var frontier = new List<Node> { root };  // setting frontier as root node
            var seenMeals = new HashSet<string>();

            // looping until there are no more nodes
            while (frontier.Any())  
            {

                // current node choosen depending on the highest heuristic value
                var currentNode= frontier.OrderByDescending(node => node.Meal.Sum( item => heuristic(item))).FirstOrDefault();
                // check
                Console.WriteLine($"Current node has {currentNode.Meal.Count} items");

                if (currentNode == null) 
                    break;
                // if node matches goal test, return optimal meal 
                if (goalTest(currentNode))
                {
                    Console.WriteLine("Goal reached with current node.");
                    return currentNode.Meal;

                }
                var potentialExpansions = new List<Node>();


                //expanding node by adding each time that fits under the calorie limit, and excluding same categorys 
                foreach (var item in menuItems.Where(item => 
                currentNode.TotalCalories + item.Calories <= calorieLimit
                && !currentNode.Meal.Contains(item)
                && !currentNode.Meal.Any(existingItem => existingItem.Type == item.Type)))
                {

                   var newNode = currentNode.AddItem(item); // making new node with added item
                    var mealHash = CreateMealHash(newNode.Meal);
                    //preventing duplicates
                    if (!seenMeals.Contains(mealHash))
                    {
                        potentialExpansions.Add(newNode);
                        seenMeals.Add(mealHash);
                        Console.WriteLine($"Added new node with {newNode.Meal.Count} items, total calories: {newNode.TotalCalories}");

                    }
                }

                var selectedExpansions = potentialExpansions.OrderByDescending(node => node.Meal.Sum(item => heuristic(item)))
                                                     .Take(10); // For example, select the top 10 based on heuristic

                foreach (var expansion in selectedExpansions)
                {
                    frontier.Add(expansion);
                    Console.WriteLine($"Added new node with {expansion.Meal.Count} items, total calories: {expansion.TotalCalories}");
                }


                frontier.Remove(currentNode); // removing current node from frontier because its been expanded
                Console.WriteLine("Current node removed from frontier.");

            }
            Console.WriteLine("No optimal meal found.");

            return new List<MenuItem>(); // return an empy list if goal meal wasnt found
        }


        private string CreateMealHash(List<MenuItem> meal)
        {
            // making a unique hash for a meal based on sorted item IDs
            return string.Join("-", meal.Select(item => item.ID).OrderBy(id => id));
        }
    }

}
