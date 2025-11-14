using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCERP.Domain.Models
{
    /// <summary>
    /// 物料分类
    /// </summary>
    public class MaterialType
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [MaxLength(50)]
        public string Id { get; set; }
        /// <summary>
        /// 分类编码
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string EnCode { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }
        /// <summary>
        /// 分类描述
        /// </summary>
        [MaxLength(100)]
        public string? Description { get; set; } = "";

    }
}
