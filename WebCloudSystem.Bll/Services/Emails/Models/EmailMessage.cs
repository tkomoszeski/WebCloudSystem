using System.Collections.Generic;

namespace WebCloudSystem.Bll.Services.Emails.Models {

    public class EmailMessage {

        public List<EmailAddress> ToAddresses { get; set; }
        public List<EmailAddress> FromAddresses { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

    }
}