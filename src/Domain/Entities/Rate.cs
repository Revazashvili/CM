using System;
using Domain.Common;

namespace Domain.Entities
{
    public class Rate : Entity
    {
        public Rate() { }
        
        public Rate(Currency from, Currency to, double sell, double buy, DateTime date) =>
            (From, To, Sell, Buy, Date) = (from, to, sell, buy, date);

        public Currency From { get; set; }
        public Currency To { get; set; }
        public double Sell { get; set; }
        public double Buy { get; set; }
        public DateTime Date { get; set; }
    }
}