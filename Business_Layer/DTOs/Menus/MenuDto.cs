using Business_Layer.DTOs.CommonDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.Menus
{
    public class MenuDto 
    {
        public int MenuId { get; set; }

        public string MenuName { get; set; } = string.Empty;

        public int? ParentMenuId { get; set; }

        public string? Url { get; set; }

        public string? Icon { get; set; }

        public int? OrderNo { get; set; }

        public string MenuType { get; set; } = "Common";

        public bool IsActive { get; set; } = true;

        public bool CanView { get; set; } = true;

        public bool CanAdd { get; set; }

        public bool CanEdit { get; set; }

        public bool CanDelete { get; set; }

        public bool CanApprove { get; set; }
    }
}
