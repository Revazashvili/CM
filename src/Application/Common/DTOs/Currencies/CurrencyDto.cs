using Domain.Enums;

namespace Application.Common.DTOs.Currencies
{
    public class CurrencyDto
    {
        public string? Code { get; set; }
        public string Name { get; set; }
        public string LatinName { get; set; }
        public CurrencyStatus Status { get; set; }
    }
}