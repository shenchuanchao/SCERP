using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCERP.Domain.DTOs
{
    /// <summary>
    /// 物料数据传输对象
    /// </summary>
    public class MaterialRequest
    {
        public string EnCode { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public MaterialRequest() { }


    }
}
