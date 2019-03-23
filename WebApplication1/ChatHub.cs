using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using WebApplication1.Models.Entities;

namespace WebApplication1
{
    public class ChatHub : Hub
    {
        static Dictionary<string, string> users = new Dictionary<string, string>();
        ChatContext database = ChatContext.Instance;

        public override Task OnDisconnected(bool stopCalled)
        {
            users.Remove(Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.addNewMessageToPage(name, message);
        }

        public void Hello(string tenNguoiDung)
        {
            users.Add(Context.ConnectionId, tenNguoiDung);
            Clients.Client(Context.ConnectionId).notify("Thanh cong");
        }

        public void GuiTinNhan(string nguoiGoi, string nguoiNhan, string text)
        {
            DateTime now = DateTime.Now;
            string connectionId = users.FirstOrDefault(p => p.Value == nguoiNhan).Key;
            if (connectionId != null)
            {
                Clients.Client(connectionId).sendMessage(nguoiGoi, nguoiNhan, text, now.ToString("dd/MM/yyyy hh:mm"));
            }

            database.TinNhans.Add(new TinNhan
            {
                NguoiGoi = nguoiGoi,
                NguoiNhan = nguoiNhan,
                NoiDung = text,
                ThoiGianGoi = now
            });
            database.SaveChanges();
        }
    }
}