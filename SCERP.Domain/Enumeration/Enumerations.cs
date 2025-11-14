using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCERP.Domain
{
    /// <summary>
    /// 合作伙伴类别
    /// </summary>
    public enum PartnerCategory
    {
        /// <summary>
        /// 客户
        /// </summary>
        Customer = 1,

        /// <summary>
        /// 供应商
        /// </summary>
        Supplier = 2
    }

    /// <summary>
    /// 合作伙伴状态
    /// </summary>
    public enum PartnerStatus
    {
        /// <summary>
        /// 活跃
        /// </summary>
        Active = 1,

        /// <summary>
        /// 停用
        /// </summary>
        Inactive = 2,

        /// <summary>
        /// 待审核
        /// </summary>
        Pending = 3
    }



}
