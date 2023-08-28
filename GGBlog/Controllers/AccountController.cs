using GGBlog.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GGBlog.Controllers
{

    public class AccountController : Controller
    {
        public string connectionString = " Server = ;Database=;User Id = ; Password=;";

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel Model)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // seriii üzgün

                var command = new SqlCommand("select * from Kullanici where mail = @mail and kullanici_sifre = @kullanici_sifre", connection);
                command.Parameters.AddWithValue("@kullanici_sifre", Model.Kullanici_sifre);
                command.Parameters.AddWithValue("@mail", Model.Mail);

                var reader = command.ExecuteReader();

                if (!ModelState.IsValid)
                {
                    ViewBag.mesaj = "Mail adresi veya şifre yanlış.";
                    return View("Login");
                }

                else if (reader.HasRows)
                {
                    HttpContext.Session.SetString("mail", Model.Mail);
                    return View();

                }


            }
            ViewBag.hata("Mail adresi veya şifre yanlış");

            return View("Login");
        }

        public IActionResult KayıtOl()
        {
            return View();
        }
        [HttpPost]
        public IActionResult KayıtOl(LoginCreate create)
        {
            

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                //if (!ModelState.IsValid)
                //{
                //    ViewBag.mesaj = "Eksik içerikleri lütfen doldurunuz.";
                //    return View();
                //}
                //try
                {
                    connection.Open();
                    var command = new SqlCommand(
                        "INSERT INTO Kullanici (kullanıcı_ad,kullanıcı_isimsoyisim,kullanıcı_sifre,kullanıcı_mail,kullanıcı_telefon) VALUES (@Kullanıcı_ad,@Kullanıcı_isimsoyisim,@Kullanıcı_sifre,@Kullanıcı_mail,@Kullanıcı_telefon)", connection
                        );
                    command.Parameters.AddWithValue("@Kullanıcı_ad", create.Kullanıcı_ad);
                    command.Parameters.AddWithValue("@Kullanıcı_isimsoyisim", create.Kullanıcı_isimsoyisim);
                    command.Parameters.AddWithValue("@Kullanıcı_sifre", create.Kullanıcı_sifre);
                    command.Parameters.AddWithValue("@Kullanıcı_mail", create.Kullanıcı_mail);
                    command.Parameters.AddWithValue("@Kullanıcı_telefon", create.Kullanıcı_telefon);

                    command.ExecuteNonQuery();
                    return Json(create);
                }
                //catch (Exception)
                //{

                //    return View();

                //}

                return Content("0");
            }





        }
    }
}
