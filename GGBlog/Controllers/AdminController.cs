using GGBlog.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace GGBlog.Controllers
{
    public class AdminController : Controller
    {
        public string connectionString = " Server = 104.247.162.242\\MSSQLSERVER2017;Database=akadem58_sd;User Id = akadem58_sd; Password=Hfoe27!96;";

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Duzenle(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("SELECT id, slug, title, summary, content FROM Blog WHERE id = @id", connection);
                    command.Parameters.AddWithValue("@id", id);
                    var reader = command.ExecuteReader();

                    reader.Read();

                    var blogItem = new BlogPost();
                    blogItem.id = reader.GetInt32(0);
                    blogItem.slug = reader.GetString(1);
                    blogItem.title = reader.GetString(2);
                    blogItem.summary = reader.GetString(3);
                    blogItem.content = reader.GetString(4);

                    return View("IcerikDuzenle", blogItem);

                }
                catch
                {

                    ViewBag.Msg = "Hata oldu. Daha sonra tekrar deneyiniz.";
                    return View("Mesaj");
                }
            }
        }
        [HttpPost]
        public IActionResult Duzenle(BlogUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Msg = "Hatalı form gönderdiniz";
                return View("Mesaj");
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    var command = new SqlCommand(
                            "UPDATE BlogSET title = @title, summary = @summary, content = @content, slug = @slug, updated_on = @updated_on WHERE id = @id",
                            connection);

                    command.Parameters.AddWithValue("@id", model.id);
                    command.Parameters.AddWithValue("@title", model.title);
                    command.Parameters.AddWithValue("@summary", model.summary);
                    command.Parameters.AddWithValue("@content", model.content);
                    command.Parameters.AddWithValue("@slug", model.slug);
                    command.Parameters.AddWithValue("@updated_on", DateTime.Now);

                    command.ExecuteNonQuery();

                    return RedirectToAction("IcerikGuncellendi");

                }
                catch (Exception e)
                {
                    ViewBag.Msg = "Hata oldu. Daha sonra tekrar deneyiniz.";
                    return View("Mesaj");
                }
            }

        }
        public IActionResult IcerikGuncellendi()
        {
            ViewBag.MsgTitle = "İçerik güncellendi";
            ViewBag.Msg = "İçerik güncellendi";
            return View("Mesaj");
        }
        public IActionResult Sil(int id)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    var command = new SqlCommand(
                            "DELETE FROM contents WHERE id = @id",
                            connection);

                    command.Parameters.AddWithValue("@id", id);

                    command.ExecuteNonQuery();

                    ViewBag.MsgTitle = "İçerik Silindi";
                    ViewBag.Msg = "İçerik Silindi";
                    return View("Mesaj");

                }
                catch (Exception e)
                {
                    ViewBag.MsgTitle = "İçerik bulunamadı";
                    ViewBag.Msg = "Böyle bir içerik bulunamadı";
                    return View("Mesaj");
                }
            }

        }
    }
}
