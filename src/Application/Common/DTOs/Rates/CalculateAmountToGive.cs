﻿namespace Application.Common.DTOs.Rates
{
    public class CalculateAmountToGive
    {
        public string From { get; set; }
        public string To { get; set; }
        public double ReceivableAmount { get; set; }
    }
}