using GGBlog.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GGBlog.Controllers
{
    public class HomeController : Controller
    {
        public string connectionString =" Server = ;Database=;User Id = ; Password=;";

        public IActionResult Index()
        {
           

            BlogListModel model = new BlogListModel();

            var blogPosts = new List<BlogPost>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT id, title , summary, slug, created_on  FROM Blog ORDER BY created_on DESC", connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var blogItem = new BlogPost();
                    blogItem.id = reader.GetInt32(0);
                    blogItem.title = reader.GetString(1);
                    blogItem.summary = reader.GetString(2);
                    blogItem.slug = reader.GetString(3);
                    blogItem.created_on= reader.GetDateTime(4);
                    blogPosts.Add(blogItem);

                }
            }

            model.Cats = getAllCats();
            model.BlogPosts= blogPosts;

            return View(model);
        }

        [Route("{slug}")]
        public IActionResult Detay(string slug)
        {
           
           

            var blogItem = new BlogPostDetail();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("SELECT id,title, content ,created_on FROM Blog WHERE slug = @slug", connection);
                    command.Parameters.AddWithValue("@slug", slug);
                    var reader = command.ExecuteReader();

                    reader.Read();
                    blogItem.id = reader.GetInt32(0);
                    blogItem.title = reader.GetString(1);
                    blogItem.content = reader.GetString(2);
                    blogItem.created_on = reader.GetDateTime(3);
                    connection.Close();

                     connection.Open();
                     var command2 = new SqlCommand("SELECT comment FROM Yorumlar WHERE content_id = @id", connection);
                     command2.Parameters.AddWithValue("@id", blogItem.id);
                     var reader2 = command2.ExecuteReader();

                     var blogComment = new List<BlogCommentItem>();

                     while (reader2.Read())
                     {

                         blogComment.Add(new BlogCommentItem()
                         {
                             comment = reader2.GetString(0)
                         });
                         blogItem.Comments = blogComment;
                     }
                    return View(blogItem);
                }
                catch(Exception a)
                {
                    HttpContext.Response.StatusCode = 404;
                    return View("SayfaBulunamadi" + a.ToString());
                }

            }

            return View(blogItem);
        }
        public IActionResult IcerikEkle()
        {  
            
            return View();
        }
        [HttpPost]
        public IActionResult IcerikEkle(BlogModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Msg = "Eksik İçerik Var";
                return View("IcerikEkle");
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand(
                         "INSERT INTO Blog (title, summary, content, slug, created_on, updated_on) VALUES (@title, @summary, @content, @slug, @created_on, @updated_on)",
                        connection);
                    command.Parameters.AddWithValue("@title", model.title);
                    command.Parameters.AddWithValue("@summary", model.summary);
                    command.Parameters.AddWithValue("@content", model.content);
                    command.Parameters.AddWithValue("@slug", model.slug);
                    command.Parameters.AddWithValue("@created_on", DateTime.Now);
                    command.Parameters.AddWithValue("@updated_on", DateTime.Now);

                    command.ExecuteNonQuery();
                    return RedirectToAction("IcerikEklendi");

                }
                catch (Exception e)
                {

                    ViewBag.Msg = "Eklenemedi. Hata oldu. Git bir bak istersen. " + e.ToString();
                    return View("IcerikEkle");

                }
            }



        }
        public IActionResult IcerikEklendi()
        {
            ViewBag.MsgTitle = "Blog Eklendi.";
            ViewBag.Msg = "Blog Eklendi.";
            return View("Mesaj");
        }
       
        [HttpPost]
        public IActionResult Ara(string ara)
        {
            var blogPosts = new List<BlogPost>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT id, title , summary, slug FROM Blog ORDER BY created_on DESC WHERE title LIKE '%title%' title = @title", connection);
                command.Parameters.AddWithValue("@title", ara);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var blogItem = new BlogPost();
                    blogItem.id = reader.GetInt32(0);
                    blogItem.title = reader.GetString(1);
                    blogItem.summary = reader.GetString(2);
                    blogItem.slug = reader.GetString(3);
                    blogPosts.Add(blogItem);

                }
            }
            return View(blogPosts);

        }
        public IActionResult Kategoriler(int katId)
        {
            var kategoris = new List<BlogKat>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT kat_ad FROM Kategoriler WHERE kat_id = @katId", connection);
                command.Parameters.AddWithValue("@katId", katId);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var blogKat = new BlogKat();
                    blogKat.kat_ad = reader.GetString(0);
                    kategoris.Add(blogKat);

                }
                SelectList categoryList = new SelectList(kategoris,  "Kategoriler");
                ViewBag.kategoris = categoryList;
            }
            return View(kategoris);

        }

        public List<string> getAllCats()
        {
            var kategoris = new List<string>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT kat_ad FROM Kategoriler", connection);
                //command.Parameters.AddWithValue("@katId", katId);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    //var blogKat = new BlogKat();
                    //blogKat.kat_ad = reader.GetString(0);
                    kategoris.Add(reader.GetString(0));

                }

                return kategoris;
            }

        }




    }
}
