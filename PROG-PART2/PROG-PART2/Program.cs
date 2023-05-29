using System.Linq.Expressions;

internal class Program
{
    private static void Main(string[] args)
    {
        Dictionary<String, Recipe> dRecipe= new Dictionary<String, Recipe>();
        Appmenu aMenu = new Appmenu(dRecipe);
        aMenu.App();


    }
}

class RecipeLog
{
    private Dictionary<String, Recipe> dRecipe;
    public RecipeLog(Dictionary<string, Recipe> dRecipe)
    {
        this.dRecipe = dRecipe;
    }
    
    public void LogRecipe()
    {
        Console.WriteLine("Enter number of recipe: ");
        int rNum;

        if (int.TryParse(Console.ReadLine(), out rNum))
        {
            for(int i = 0; i < rNum; i++)
            {
                Console.WriteLine("Enter recipe name: ");
                String recipeName = Console.ReadLine();
                Recipe r = new Recipe();
                r.RecipeData();
                dRecipe.Add(recipeName, r);
            }

            String key;

            do
            {
                Console.WriteLine("Display ingredients and steps? (Y/N)");
                key = Console.ReadLine();

                switch (key)
                {
                    case "Y":
                        foreach (var entry in dRecipe)
                        {
                            Console.WriteLine($"Recipe Name: {entry.Key}");
                            entry.Value.DisplayRecipe();
                        }
                        break;

                    case "N":
                        Appmenu menu = new Appmenu(dRecipe);
                        menu.App();
                        break;

                    default:
                        Console.WriteLine("Input valid value");
                        break;
                }

            } while (key != "N");
        }else
        {
            Console.WriteLine("Enter a number");
        }

    }
    public void displayRecipeList()
    {
        foreach(var rEntry in dRecipe)
        {
            Console.WriteLine($"Recipe Name: {rEntry.Key}");
        }
    }
    public void recipeSearch()
    {
        Console.Write("Enter name of the recipe: ");
        String name = Console.ReadLine();
        if (dRecipe.ContainsKey(name))
        {
            Console.WriteLine($"Recipe name: {name}");
            dRecipe[name].DisplayRecipe();
        }
        else
        {
            Console.WriteLine("Recipe not available");
        }
    }
    
}
class Ingredient
{
    private Dictionary<String, Recipe> dRecipe;
    public Ingredient(Dictionary<string, Recipe> dRecipe)
    {
        this.dRecipe = dRecipe;
        Recipe r = new Recipe();

        while (true)
        {
            Console.WriteLine("*************************************************");
            Console.WriteLine("           Welcome to Mason Recipe!              ");
            Console.WriteLine("*************************************************");
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("1. To enter recipe details");
            Console.WriteLine("2. To display recipe");
            Console.WriteLine("3. To scale recipe");
            Console.WriteLine("4. To reset quantities");
            Console.WriteLine("5. To clear recipe");
            Console.WriteLine("0. To go back to main menu");
            Console.WriteLine("*************************************************");

            Console.Write("\nEnter key >> ");
            String key = Console.ReadLine();

            switch (key)
            {
                case "1":
                    r.RecipeData();
                    break;

                case "2":
                    r.DisplayRecipe();
                    break;

                case "3":
                    Console.Write("Enter scaling factor (0.5, 2, or 3): ");
                    double factor = double.Parse(Console.ReadLine());
                    r.Scale(factor);
                    break;

                case "4":
                    r.ResetQuantities();
                    break;

                case "5":
                    r.Clear();
                    break;

                case "0":
                    Appmenu a = new Appmenu(dRecipe);
                    a.App();
                    break;

                default:
                    Console.WriteLine("Invalid key");
                    break;
            }

        }

    }

}

class Recipe
{
    private string[] name;
    private String[] ingredients;
    private double[] Quantity;
    private String[] Unit;
    private String[] Steps;
    private double[] calories;
    private string[] foodGroup;

    public Recipe()
    {
        name = new string[0];
        ingredients = new String[0];
        Quantity = new double[0];
        Unit = new String[0];
        Steps = new String[0];
        calories = new double[0];
        foodGroup = new String[0];
    }

    //Method to enter recipe data
    public void RecipeData()
    {
  
      

        Console.Write("Enter number of ingredients >> ");
        int key = Convert.ToInt32(Console.ReadLine());

        ingredients = new string[key];
        Quantity = new double[key];
        Unit = new String[key];
        name = new string[key];
        calories= new double[key];
        foodGroup = new String[key];

        for (int i = 0; i < key; i++)
        { 
      
            Console.Write($"Enter ingredient {i + 1} name: ");
            ingredients[i] = Console.ReadLine();

            do
            {
                Console.Write("Quantity: ");

            } while (!double.TryParse(Console.ReadLine(), out Quantity[i]));

            Console.Write($"Enter ingredient {i + 1} unit: ");
            Unit[i] = Console.ReadLine();

            do
            {
                Console.Write("Number of calories: ");
            } while (!double.TryParse(Console.ReadLine(), out calories[i]));

            Console.Write("Enter food group: ");
            foodGroup[i] = Console.ReadLine();

        }

        //Delegate calories
        double caloriesExceeded = CalculateTotalCalories(calories);
        Console.WriteLine("Calories total: " + caloriesExceeded);

        if(caloriesExceeded > 300)
        {
            Console.WriteLine("Calories exceeded :(");
        }

        int stepKey;

        do
        {
            Console.Write("Enter number of steps >> ");
        } while (!int.TryParse(Console.ReadLine(), out stepKey));

        Steps = new string[stepKey];

        for(int i = 0; i < stepKey; i++)
        {
            Console.Write($"Steps#{i + 1} ");
            Steps[i] = Console.ReadLine();
        }
    }

    public double CalculateTotalCalories(double[] calories)
    {
        double result = 0;
        for(int i = 0; i < calories.Length;i++)
        {
            result += calories[i];  
        }return result;
    }
    
    public void DisplayRecipe()
    {
        Console.WriteLine("*************************************************************");
        Console.WriteLine("***                    MASON RECIPE                       ***");
        Console.WriteLine("*************************************************************");
        Console.WriteLine("INGREDIENTS: ");
        for(int i = 0; i < ingredients.Length; i++)
        {
            Console.WriteLine($"{i+1}. {Quantity[i]} {Unit[i]} of {ingredients[i]}");
        }
        Console.WriteLine("\n*************************************************************");
        Console.WriteLine("\nINSTRUCTIONS:");
        for (int i = 0; i < Steps.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {Steps[i]}");
        }
        Console.WriteLine("*************************************************************");
        Console.WriteLine("***                 MASON DINER KITCHEN                   ***");
        Console.WriteLine("*************************************************************");
    }

    //Method that scale or add the ingredient quantities and unit measurements
    public void Scale(double factor)
    {
        for(int i = 0; i < Quantity.Length; i++)
        {
            Quantity[i] *= factor;
        }
    }

    //Method which reset the entire recipe quantities
    public void ResetQuantities()
    {
        for (int i = 0; i < Quantity.Length; i++)
        {
            Quantity[i] /= 2;    
        }
    }

    //Method that clears everything within the recipe
    public void Clear()
    {
        ingredients = new String[0];
        Quantity = new double[0];
        Unit = new String[0];
        Steps = new String[0];
    }

}

class Appmenu
{
    private Dictionary<String, Recipe> dRecipe;
    private RecipeLog rLog;
    public Appmenu(Dictionary<string, Recipe> dRecipe)
    {
        this.dRecipe = dRecipe;
        rLog = new RecipeLog(dRecipe);
    }

    public void App()
    {
        while (true)
        {
            Console.WriteLine("*******************************************************");
            Console.WriteLine("***                RECIPE APPLICATION               ***");
            Console.WriteLine("*******************************************************");
            Console.WriteLine("1. CREATE RECIPE");
            Console.WriteLine("2. SEARCH RECIPE");
            Console.WriteLine("3. DISPLAY ALL RECIPES");
            Console.WriteLine("4. EXIT");

            Console.WriteLine("Select an option");
            String entryKey = Console.ReadLine();   
            switch(entryKey)
            {
                case "1":
                    rLog.LogRecipe();
                    break;
                case "2":
                    rLog.recipeSearch();
                    break;
                case "3":
                    rLog.displayRecipeList();
                    break;
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid input ");
                    break;
            }

        }
    }
}