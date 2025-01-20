using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Api.Tests
{
    public class Errors
    {
        public List<string>? Cvv { get; set; }
        public List<string>? Currency { get; set; }
        public List<string>? CardNumber { get; set; }
        public List<string>? ExpiryYear { get; set; }
        public List<string>? ExpiryMonth { get; set; }
    }

    public class ValidationModel
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public Errors Errors { get; set; }
        public string TraceId { get; set; }
    }
}
