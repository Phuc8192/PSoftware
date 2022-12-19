using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PSoftware.Models
{
    public class LoginModels
    {
        [Key]

        [Required(ErrorMessage = "Bạn phải nhập tên đăng nhập")]
        public string TenDN { set; get; }

        [Required(ErrorMessage = "Bạn phải nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string MK { set; get; }
    }
}