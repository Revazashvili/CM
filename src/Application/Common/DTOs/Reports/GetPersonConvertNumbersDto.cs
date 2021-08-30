using System;

namespace Application.Common.DTOs.Reports
{
    public class GetPersonConvertNumbersDto
    {
        /// <summary>
        /// Gets or sets start date for report
        /// </summary>
        public DateTime? From { get; set; }
        /// <summary>
        /// Gets or sets end date for report
        /// </summary>
        public DateTime? To { get; set; }
    }
}