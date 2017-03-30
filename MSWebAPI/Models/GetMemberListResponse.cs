using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSWebAPI.Models
{
    /// <summary>
    /// 取得全部會員資訊
    /// </summary>
    public class GetMemberListResponse
    {
        /// <summary>
        /// 回傳結果狀態，true表示成功，false表示失敗
        /// </summary>
        public bool ok { get; set; }
        /// <summary>
        /// 失敗時，顯示失敗原因
        /// </summary>
        public string errMsg { get; set; }
        /// <summary>
        /// 會員資訊
        /// </summary>
        public List<MemberInfo> list { get; set; }

        public GetMemberListResponse()
        {
            this.ok = true;
            this.errMsg = "";
            this.list = new List<MemberInfo>();
        }
    }
}