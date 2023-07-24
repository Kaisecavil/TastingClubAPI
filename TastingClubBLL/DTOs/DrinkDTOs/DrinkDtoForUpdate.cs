namespace TastingClubBLL.DTOs.DrinkDTOs
{
    public class DrinkDtoForUpdate
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double AlcoholPercentage { get; set; }
        public string Color { get; set; }
        public string Taste { get; set; }
        public string Aroma { get; set; }
        public string GastronomicCombination { get; set; }

        public int DrinkTypeId { get; set; }
        public int ProducingCountryId { get; set; }
    }
}