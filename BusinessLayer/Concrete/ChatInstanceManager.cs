using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;

namespace BusinessLayer.Concrete;

public class ChatInstanceManager : IChatInstanceService
{
    private readonly IChatInstanceDal _chatInstance;

    public ChatInstanceManager(IChatInstanceDal chatInstance)
    {
        _chatInstance = chatInstance;
    }


    public int CreateChannelId()
    {
        var index = 1;
        var chats = _chatInstance.List();
        for (int i = 0; i < chats.Count; i++)
        {
            if (chats[i].Channel == index)
            {
                index++;
                i = 0;
            }
            if (index == 100)
            {
                throw new Exception("Couldn't find channel id");
            }
        }
        return index;
            

    }
    public int? GetChannelId(string username1, string username2)
    {
        var userList = new List<string>() { username1, username2 };

        var chat = _chatInstance.List(c => userList.Contains(c.Sender) && userList.Contains(c.Receiver)).FirstOrDefault();

        if (chat is null)
            return null;
        

        return chat.Channel;
    }

    public void Add(ChatInstance t)
    {
        _chatInstance.Insert(t);
    }

    public void Delete(ChatInstance t)
    {
        _chatInstance.Delete(t);

    }

    public ChatInstance? GetById(int id)
    {
        return _chatInstance.Get(c => c.ChatInstanceId== id);
    }

    public List<ChatInstance> GetList()
    {
        return _chatInstance.List();

    }

    public List<ChatInstance> GetListByUsernames(string username1, string username2)
    {
        var userList = new List<string>() { username1, username2 };

        return _chatInstance.List(c => userList.Contains(c.Sender) && userList.Contains(c.Receiver)).OrderBy(c => c.DateTime).ToList();
    }
    public List<ChatInstance> GetListByChannelId(int channelId)
    {
        return _chatInstance.List(c => c.Channel == channelId).OrderByDescending(c => c.DateTime).OrderBy(c => c.DateTime).ToList();

    }

    public void Update(ChatInstance t)
    {
        _chatInstance.Update(t);

    }
}
