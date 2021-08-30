using System;

namespace Application.Common.DTOs.Rates
{
    public class RateDto
    {
        /// <summary>
        /// Gets or sets rate identifier
        /// </summary>
        public int Id { get; set; }
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