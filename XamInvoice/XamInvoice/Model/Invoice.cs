using System;

using static System.DateTime;
using static System.String;


namespace XamInvoice.Model
{
    public   class Invoice
    {
        public double Total { get; set; } = 15.0;
        public DateTime TimeStamp { get; set; } = UtcNow;
        public string Photo { get; set; } = Empty;
    }
}
