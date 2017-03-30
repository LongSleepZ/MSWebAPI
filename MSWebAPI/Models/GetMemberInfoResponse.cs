using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSWebAPI.Models
{
    /// <summary>
    /// 取得指定會員資訊
    /// </summary>
    public class GetMemberInfoResponse
    {
        /// <summary>
        /// 回傳結果狀態，true表示成功，false表示失敗
        /// </summary>
        public bool ok { get; set; }
        /// <summary>
        /// 失敗時，顯示失敗原因
        /// </summary>
        public string errMsg { get; set; }
        public MemberInfo data { get; set; }

        public GetMemberInfoResponse()
        {
            this.ok = true;
            this.errMsg = "";
            this.data = new MemberInfo();
        }
    }
}