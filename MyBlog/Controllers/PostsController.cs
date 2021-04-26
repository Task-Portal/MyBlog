using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyBlog.Data;
using MyBlog.Models;
using MyBlog.ViewModels;

namespace MyBlog.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _env;
        private readonly string[] permittedExtensions = {".jpg", ".jpeg", ".png", ".gif"};


        public PostsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _env = hostEnvironment;
        }

        // GET: Posts
        public async Task<IActionResult> Index(int? categoryId, int pageNumber = 1)
        {
            // 1 Формирование коллекции выводимых постов:
            var posts = _context.Posts.ToList();
            if (categoryId != null && categoryId != 0) posts = posts.Where(x => x.CategoryId == categoryId).ToList();


            // 2Разбивка коллекции постов на страницы пагинации
            var pageSize = 3;
            var count = posts.Count();
            var items = posts
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToList();

            //3Создание пагинатора
            var paginator = new PageVM(count, pageNumber, pageSize);

            //4  Формарование коллекции категорий
            var categories = _context.Categories.ToList();
            categories.Insert(0, new Category {Id = 0, Name = "All categories"});

            // 5 Создание модели представления постов
            var viewModel = new PostVM
            {
                Posts = items,
                Paginator = paginator,
                Categories = new SelectList(categories, "Id", "Name")
            };
            return View(viewModel);
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            var categories = _context.Categories.ToList();
            ViewData["Categoryid"] = new SelectList(categories, "Id", "Name");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Title,Description,Content,ImagePath,Id,PublishDate,PublishTime,CategoryId")]
            Post post,
            IFormFile uploadFile)
        {
            if (ModelState.IsValid)
            {
                //* File Upload: 
                if (uploadFile != null)
                {
                    var name = uploadFile.FileName;

                    var ext = Path.GetExtension(name).ToLowerInvariant();

                    if (permittedExtensions.Contains(ext))
                    {
                        var path = $"/files/{name}";
                        var serverPath = _env.WebRootPath + path;

                        using var fs = new FileStream(serverPath, FileMode.Create, FileAccess.ReadWrite);

                        await uploadFile.CopyToAsync(fs);

                        post.ImagePath = path;
                    }
                    else
                    {
                        return RedirectToAction("ExtentionError", "Errors");
                    }
                }

                _context.Posts.Add(post); // context.Add
                    await _context.SaveChangesAsync();


                    return RedirectToAction(nameof(Index));
            }

            var categories = _context.Categories.ToList();
            ViewData["categoryId"] = new SelectList(categories, "Id", "Name", post.CategoryId);
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Title,Description,Content,ImagePath,Id,PublishDate,PublishTime")]
            Post post
            , IFormFile uploadFile)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
