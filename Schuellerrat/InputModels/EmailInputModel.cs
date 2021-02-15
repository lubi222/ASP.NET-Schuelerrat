using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schuellerrat.InputModels
{
    public class EmailInputModel
    {
        public string From { get; set; }
        public string FromName { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string HtmlContent { get; set; }
    }
}
