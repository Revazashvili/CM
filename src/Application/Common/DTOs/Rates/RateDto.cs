using System;

namespace Application.Common.DTOs.Rates
{
    public class RateDto
    {
        public string From { get; set; }
        public string To { get; set; }
        public double Sell { get; set; }
        public double Buy { get; set; }
        public DateTime Date { get; set; }
    }
}