using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Model
{
    partial class Ingredient
    {
        public string Color
        {
            get
            {
                if (AvailableCount < 60)
                    return "green";
                else if (AvailableCount > 60 && AvailableCount < 200)
                    return "yellow";
                else
                    return "red";
            }
        }
    }
}
