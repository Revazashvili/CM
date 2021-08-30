namespace Application.Common.DTOs.Rates
{
    public class CalculateAmountToGive
    {
        public string From { get; set; }
        public string To { get; set; }
        /// <summary>
        /// Gets or sets receive amount
        /// </summary>
        public double ReceivableAmount { get; set; }
    }
}