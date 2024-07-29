using System.Diagnostics.CodeAnalysis;

namespace iBurguer.Ordering.Infrastructure.SQS
{
    [ExcludeFromCodeCoverage]
    public class SQSConfiguration
    {
        public string Region { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string CartClosedQueue { get; set; }
        public string PaymentApprovedQueue { get; set; }
        public string PaymentRefusedQueue { get; set; }
    }
}
