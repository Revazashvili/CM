using Domain.Enums;

namespace Application.Common.DTOs.Currencies
{
    public class CurrencyDto
    {
        /// <summary>
        /// Gets or sets currency code
        /// </summary>
        public string? Code { get; set; }
        /// <summary>
        /// Gets or sets currency name 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets currency latin name
        /// </summary>
        public string LatinName { get; set; }
        /// <summary>
        /// Gets or sets currency status
        /// </summary>
        public CurrencyStatus Status { get; set; }
    }
}