using System;
using Domain.Common;

namespace Domain.Entities
{
    public class Convert : AuditableEntity
    {
        public Convert() { }

        public Convert(Currency from, Currency to, User converter, string recommenderPin, double amount, DateTime time)
        {
            From = from;
            To = to;
            Converter = converter;
            RecommenderPin = recommenderPin;
            Amount = amount;
            Time = time;
        }

        public Currency From { get; }
        public Currency To { get; }
        public User Converter { get; }
        public string RecommenderPin { get; }
        public double Amount { get; }
        public DateTime Time { get; }
    }
}