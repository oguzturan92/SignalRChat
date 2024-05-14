using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Concrete;
using Data.Concrete.EfCore;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebUI.Models;

namespace WebUI.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private UserManager<ProjeUser> _userManager;
        private RoleManager<ProjeRole> _roleManager;
        public ChatController(UserManager<ProjeUser> userManager, RoleManager<ProjeRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        StudentManager _studentService = new StudentManager(new StudentDal());
        ChatManager _chatService = new ChatManager(new ChatDal());
        ChatLineManager _chatLineService = new ChatLineManager(new ChatLineDal());
        MentorManager _mentorService = new MentorManager(new MentorDal());

        public async Task<IActionResult> Index(int? id, int newMessageReceiverUserId)
        {   
            if (id != null)
            {
                var chat = _chatService.GetById((int)id);
                chat.ChatRead = false;
                _chatService.Update(chat);
            }

            var baseUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (newMessageReceiverUserId > 0)
            {
                var chat = _chatService.GetAll().Where(i => i.ChatSenderUserId == baseUser.Id && i.ChatReceiverUserId == newMessageReceiverUserId).FirstOrDefault();
                if (chat != null)
                {
                    return RedirectToAction("Index", "Chat", new { id = chat.ChatId });
                }
            }

            var chats = _chatService.GetChatsAndChatLineLast(baseUser.Id);

            var baseUserRole = (await _userManager.GetRolesAsync(baseUser)).FirstOrDefault();
            ViewBag.roleName = baseUserRole;
            
            List<StudentChatListModel> chatList = new List<StudentChatListModel>();
            foreach (var item in chats)
            {
                var chatListUser = new ProjeUser();
                var chatListImage = "";
                if (baseUserRole == "Mentor")
                {
                    chatListUser = await _userManager.FindByIdAsync(item.ChatSenderUserId.ToString());
                    chatListImage = _studentService.GetAll().Where(i => i.Id == item.ChatSenderUserId).FirstOrDefault().StudentImage;
                } else
                {
                    chatListUser = await _userManager.FindByIdAsync(item.ChatReceiverUserId.ToString());
                    chatListImage = _mentorService.GetAll().Where(i => i.Id == item.ChatReceiverUserId).FirstOrDefault().Image;
                }
                
                var chatModel = new StudentChatListModel()
                {
                    ChatId = item.ChatId,
                    ChatImage = chatListImage,
                    ChatRead = item.ChatRead,
                    ChatFullname = chatListUser.Name + " " + chatListUser.Surname,
                    ChatDate = item.ChatLines.FirstOrDefault().ChatLineDate,
                    ChatLineLast = item.ChatLines.FirstOrDefault().ChatLineMessage
                };
                chatList.Add(chatModel);
            }
            
            var chatLines = new List<ChatLine>();
            var receiverUserId = 0;
            if (id != null)
            {
                chatLines = _chatLineService.GetAll().Where(i => i.ChatId == id).OrderBy(i => i.ChatLineDate).ToList();
                var focusChat = _chatService.GetById((int)id);
                receiverUserId = focusChat.ChatSenderUserId == baseUser.Id ? focusChat.ChatReceiverUserId:focusChat.ChatSenderUserId;
            }
            
            var model = new StudentChatIndexModel()
            {
                FocusChatId = id ?? 0,
                BaseUserId = baseUser.Id,
                StudentChatList = chatList.OrderByDescending(i => i.ChatDate).ToList(),
                ChatLines = chatLines,
                NewMessageReceiverUserId = newMessageReceiverUserId,
                ReceiverUserId = receiverUserId,
                UserFullname = baseUser.Name + " " + baseUser.Surname
            };
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> NewMessage(int receiverUserId, string message)
        {
            var baseUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var chat = new Chat()
            {
                ChatSenderUserId = baseUser.Id,
                ChatReceiverUserId = receiverUserId,
                ChatRead = true,
                ChatStartingDate = DateTime.Now,
                ChatLines = new List<ChatLine>()
                {
                    new ChatLine(){
                        ChatLineMessage = message,
                        ChatLineSenderUserId = baseUser.Id,
                        ChatLineReceiverUserId = receiverUserId,
                        ChatLineDate = DateTime.Now
                    }
                }
            };
            _chatService.Create(chat);
            return RedirectToAction("Index", "Chat");
        }

        public IActionResult ChatDelete(int chatId)
        {
            var chat = _chatService.GetById(chatId);
            _chatService.Delete(chat);
            return RedirectToAction("Index", "Chat");
        }
        
        public IActionResult Document(int chatIdDocument, IFormFile file)
        {
            var documentName = file.FileName;
            var dosyaYolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\document", documentName);

            var sayi = 1;
            while (System.IO.File.Exists(dosyaYolu))
            {
                documentName = sayi + "-" + file.FileName;
                dosyaYolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\document", documentName);
                sayi++;
            }

            var stream = new FileStream(dosyaYolu, FileMode.Create);
            file.CopyTo(stream);
            stream.Flush();
            stream.Close();
            
            var chat = _chatService.GetById(chatIdDocument);

            var entity = new ChatLine(){
                ChatLineMessage = documentName,
                Document = true,
                ChatLineSenderUserId = chat.ChatSenderUserId,
                ChatLineReceiverUserId = chat.ChatReceiverUserId,
                ChatLineDate = DateTime.Now,
                ChatId = chatIdDocument
            };
            _chatLineService.Create(entity);

            return RedirectToAction("Index", "Chat", new { id = chatIdDocument });
        }
        
    }
}