using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Concrete;
using Data.Concrete.EfCore;
using Entity.Concrete;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using WebUI.Data;
using WebUI.Models;
// using WebUI.Models;

namespace WebUI.Hubs
{
    public class ChatHub : Hub
    {
        private UserManager<ProjeUser> _userManager;
        public ChatHub(UserManager<ProjeUser> userManager)
        {
            _userManager = userManager;
        }
        ChatManager _chatService = new ChatManager(new ChatDal());
        ChatLineManager _chatLineService = new ChatLineManager(new ChatLineDal());

        public async Task GetNickName(string userName)
        {
            if (ClientSource.Clients.Where(i => i.NickName == userName).FirstOrDefault() != null)
            {
                ClientSource.Clients.Remove(ClientSource.Clients.Where(i => i.NickName == userName).FirstOrDefault());
            }
            Client client = new Client
            {
                ConnectionId = Context.ConnectionId,
                NickName = userName
            };
            ClientSource.Clients.Add(client);
        }

        public async Task SendMessage(string message, string senderUserName, int focusChatId, int receiverUserId)
        {
            var senderUser = await _userManager.FindByNameAsync(senderUserName);
            var receiverUser = await _userManager.FindByIdAsync(receiverUserId.ToString());
            var chat = _chatService.GetById(focusChatId);
            chat.ChatRead = true;
            _chatService.Update(chat);
            var chatLine = new ChatLine()
            {
                ChatLineMessage = message,
                ChatLineDate = DateTime.Now,
                ChatLineSenderUserId = senderUser.Id,
                ChatLineReceiverUserId = receiverUserId,
                ChatId = focusChatId
            };
            _chatLineService.Create(chatLine);

            var focusGroup = GroupSource.Groups.Where(i => i.GroupName == focusChatId.ToString()).FirstOrDefault();

            var dateTimeTime = DateTime.Now.ToShortTimeString();
            var dateTimeDate = DateTime.Now.ToShortDateString();
            var userName = Context.User.Identity.Name;
            await Clients.Group(focusGroup.GroupName).SendAsync("ReceiveMessage", message, dateTimeTime, dateTimeDate, userName);

            var newMessageChatId = focusChatId;
            var fullname = senderUser.Name + " " + senderUser.Surname;
            Client client = ClientSource.Clients.FirstOrDefault(c => c.NickName == receiverUser.UserName);
            await Clients.Client(client.ConnectionId.ToString()).SendAsync("Notification", newMessageChatId, message, fullname, dateTimeTime, dateTimeDate);
        }

        public async Task AddGroup(string groupName)
        {
            var groupsByClient = GroupSource.Groups.Where(i => i.Clients.Any(i => i.NickName == Context.User.Identity.Name)).ToList();
            var client = ClientSource.Clients.Where(i => i.NickName == Context.User.Identity.Name).FirstOrDefault();
            if (groupsByClient.Count() > 0)
            {
                foreach (var group in groupsByClient)
                {
                    var matchClient = group.Clients.Where(i => i.NickName == Context.User.Identity.Name).FirstOrDefault();
                    var sonuc = group.Clients.Remove(matchClient);
                }
            }
            
            bool grupSorgula = GroupSource.Groups.Any(i => i.GroupName == groupName);
            if (!grupSorgula)
            {
                Group group = new Group {GroupName = groupName};
                GroupSource.Groups.Add(group);

            }
            
            var grupSorgula1 = GroupSource.Groups.Where(i => i.GroupName == groupName).FirstOrDefault();
            var baseClient = ClientSource.Clients.Where(i => i.ConnectionId == Context.ConnectionId).FirstOrDefault();
            if (!grupSorgula1.Clients.Any(i => i.NickName == baseClient.NickName))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, grupSorgula1.GroupName);
                grupSorgula1.Clients.Add(baseClient);
            }
        }
    
        public async Task MessageRead(string chatId)
        {
            var chat = _chatService.GetById(Int32.Parse(chatId));
            chat.ChatRead = false;
            _chatService.Update(chat);
        }
    }
}