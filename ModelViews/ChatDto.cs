namespace ModelViews;

public class ChatDto
{
	public int MessageId { get; set; }
	public string Sender { get; set; } = string.Empty;
	public string Receiver { get; set; } = string.Empty;
	public string Message { get; set; } = string.Empty;
	public string MessageSentDate { get; set; } = string.Empty;

}
