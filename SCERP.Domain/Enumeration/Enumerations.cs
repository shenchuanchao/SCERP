using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [Description("客户")]
        Customer = 1,

        /// <summary>
        /// 供应商
        /// </summary>
        [Description("供应商")]
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
        [Description("活跃")]
        Active = 1,

        /// <summary>
        /// 停用
        /// </summary>
        [Description("停用")]
        Inactive = 2,

        /// <summary>
        /// 待审核
        /// </summary>
        [Description("待审核")]
        Pending = 3
    }

    /// <summary>
    /// 结算方式
    /// </summary>
    public enum SettlementMethods
    {
        /// <summary>
        /// 现金
        /// </summary>
        [Description("现金")]
        Cash = 1,
        /// <summary>
        /// 货到付款
        /// </summary>
        [Description("货到付款")]
        CashOnDelivery = 2,
        /// <summary>
        /// 月结
        /// </summary>
        [Description("月结")]
        MonthlySettlement = 3
    }


}
