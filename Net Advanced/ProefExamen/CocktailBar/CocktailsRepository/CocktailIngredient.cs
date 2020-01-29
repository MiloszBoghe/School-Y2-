namespace CocktailBarData
{
    public class CocktailIngredient
    {
        // De naam en de eenheid van een ingredient
        // kan ook mee opgeslagen worden (uit Ingredient tabel)
        public int CocktailId { get; set; }
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }
        public decimal? Quantity { get; set; }
        public string Unit { get; set; }

    }
}
