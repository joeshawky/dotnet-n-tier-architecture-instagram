using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelViews;
using ModelViews.Concrete;

namespace UiLayerMvc.Controllers.Api
{
	[Route("api/[controller]")]
	[ApiController]
	public class ChatsController : ControllerBase
	{
		private readonly ChatInstanceManager _chatManager;
		private readonly UserManager _userManager;

		public ChatsController(IChatInstanceDal chatInstanceDal, IUserDal userDal, IFollowInstanceDal followInstanceDal)
		{
			_chatManager = new ChatInstanceManager(chatInstanceDal);
			_userManager = new UserManager(userDal, followInstanceDal);
		}

		[HttpGet(nameof(GetUserInfo))]
		public IActionResult GetUserInfo(int userId)
		{
			var user = _userManager.GetById(userId);
			if (user is null)
				return BadRequest("User not found");

			return Ok(new ChatIconModelView()
			{
				ProfilePicturePath = user.ProfileImage.ImagePath,
				UserId = userId,
				Username = user.Username
			});
		}

		[HttpPost(nameof(SendMessage))]
		public IActionResult SendMessage([FromBody] SendmessageModelView vm)
		{
			var sender = _userManager.GetById(vm.SenderId);
			var receiver = _userManager.GetById(vm.ReceiverId);

			if (sender is null || receiver is null)
				return BadRequest("User was not found");



			var senderUsername = sender.Username;
			var receiverUsername = receiver.Username;


			var channelId = _chatManager.GetChannelId(senderUsername, receiverUsername) ?? _chatManager.CreateChannelId();

			var chatInstance = new ChatInstance()
			{
				Sender = senderUsername,
				Receiver = receiverUsername,
				Message = vm.Message,
				Channel = channelId
			};

			_chatManager.Add(chatInstance);

			return Ok("Successful");
		}

		[HttpGet(nameof(GetAllMessagesByUsernames))]
		public IActionResult GetAllMessagesByUsernames(int firstUserId, int secondUserId)
		{
			var firstUser = _userManager.GetById(firstUserId);
			var secondUser = _userManager.GetById(secondUserId);

			if (firstUser is null || secondUser is null)
				return BadRequest("User was not found");

			var username1 = firstUser.Username;
			var username2 = secondUser.Username;

			var channelId = _chatManager.GetChannelId(username1, username2);
			if (channelId is null)
				return Ok(new List<string>());


			var chatListDto = new List<ChatDto>();
			var chatList = _chatManager.GetListByChannelId((int)channelId);

			foreach (var chat in chatList)
			{
				chatListDto.Add(new ChatDto()
				{
					MessageId = chat.ChatInstanceId,
					Message = chat.Message,
					Sender = chat.Sender,
					MessageSentDate = chat.DateTime.ToShortTimeString(),
					Receiver = chat.Receiver
				});
			}

			return Ok(chatListDto);
		}
	}
}
