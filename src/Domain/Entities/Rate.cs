﻿using System;
using Domain.Common;

namespace Domain.Entities
{
    public class Rate : Entity
    {
        public Rate() { }
        
        public Rate(Currency from, Currency to, double sell, double buy, DateTime date) =>
            (From, To, Sell, Buy, Date) = (from, to, sell, buy, date);

        public Currency From { get; }
        public Currency To { get; }
        public double Sell { get; }
        public double Buy { get; }
        public DateTime Date { get; }
    }
}