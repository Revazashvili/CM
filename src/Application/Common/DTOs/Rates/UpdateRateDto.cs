namespace Application.Common.DTOs.Rates
{
    public class UpdateRateDto
    {
        public int Id { get; set; }
        public double Sell { get; set; }
        public double Buy { get; set; }
    }
}