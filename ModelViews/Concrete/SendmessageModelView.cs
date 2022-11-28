namespace ModelViews.Concrete;

public class SendmessageModelView
{
	public int SenderId { get; set; }
	public int ReceiverId { get; set; }
	public string Message { get; set; } = string.Empty;

}
