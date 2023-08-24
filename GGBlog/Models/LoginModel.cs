using System.ComponentModel.DataAnnotations;

namespace GGBlog.Models
{
    public class LoginModel
    {
        [Required]
        public string Mail { get; set; }

        [Required]
        public string Kullanici_sifre { get; set; }
    }
    public class LoginCreate
    {
        [Required]
        public string Kullanıcı_ad { get; set; }

        [Required]
        public string Kullanıcı_isimsoyisim { get; set; }

        [Required]
        public string Kullanıcı_sifre { get; set; }

        [Required]
        public string Kullanıcı_mail { get; set; }

        [Required]
        public string Kullanıcı_telefon { get; set; }


    }
}
