using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Currency : Auditable
    {
        public Currency() { }

        public Currency(string? code, string name, string latinName,CurrencyStatus status = CurrencyStatus.Active) =>
            (Code, Name, LatinName,Status) = (code, name, latinName,status);

        public string? Code { get; set; }
        public string Name { get; set; }
        public string LatinName { get; set; }
        public CurrencyStatus Status { get; set;}
    }
}