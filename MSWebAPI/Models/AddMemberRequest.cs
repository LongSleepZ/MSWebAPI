using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSWebAPI.Models
{
    public class AddMemberRequest
    {
        /// <summary>
        /// 會員編號
        /// </summary>
        public string uid { get; set; }
        /// <summary>
        /// 會員姓名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 會員電話
        /// </summary>
        public string phone { get; set; }
    }
}