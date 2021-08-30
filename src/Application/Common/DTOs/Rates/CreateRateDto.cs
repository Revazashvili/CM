using System;

namespace Application.Common.DTOs.Rates
{
    public class CreateRateDto
    {
        public string From { get; set; }
        public string To { get; set; }
        /// <summary>
        /// Gets or sets currency sell value
        /// </summary>
        public double Sell { get; set; }
        /// <summary>
        /// Gets or sets currency buy value
        /// </summary>
        public double Buy { get; set; }
        /// <summary>
        /// Gets or sets currency active date value
        /// </summary>
        public DateTime Date { get; set; }
    }
}