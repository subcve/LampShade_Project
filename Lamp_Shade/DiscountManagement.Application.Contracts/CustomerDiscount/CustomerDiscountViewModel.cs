﻿namespace DiscountManagement.Application.Contracts.CustomerDiscount
{
    public class CustomerDiscountViewModel
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public string Product { get; set; }
        public int DiscountRate { get; set; }
        public string StartDate { get; set; }
        public DateTime StartDateGR { get; set; }
        public string EndDate { get; set; }
        public DateTime EndDateGR { get; set; }
        public string Reason { get; set; }
    }

}
