namespace Application.Common.DTOs.Rates
{
    public class UpdateRateDto
    {
        /// <summary>
        /// Gets or sets rate identifier
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets currency sell value
        /// </summary>
        public double Sell { get; set; }
        /// <summary>
        /// Gets or sets currency buy value
        /// </summary>
        public double Buy { get; set; }
    }
}