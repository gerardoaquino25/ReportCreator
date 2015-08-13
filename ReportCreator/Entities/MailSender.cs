using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportCreator.Entities
{
    public class MailSender
    {
        public int id { get; set; }
        public string email { get; set; }
        public int puerto { get; set; }
        public string smtp { get; set; }
        public string password { get; set; }
    }
}
