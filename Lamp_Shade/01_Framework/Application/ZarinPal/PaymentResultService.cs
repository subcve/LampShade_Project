namespace _01_Framework.Application.ZarinPal
{
	public class PaymentResultService
	{
		public bool IsSuccesful { get; set; }
		public string? Message { get; set; }
		public string IssueTrackingNo { get; set; }
		public PaymentResultService Succeed(string message,string issueTrackingNo)
		{
			IsSuccesful = true;
			Message = message;
			IssueTrackingNo = issueTrackingNo;
			return this;
		}
		public PaymentResultService Failed(string message)
		{
			IsSuccesful = false;
			Message = message;
			return this;
		}
	}
}
