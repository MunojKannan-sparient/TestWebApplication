using Microsoft.AspNetCore.Mvc;
using TestWebApplication.Data;
using TestWebApplication.Models.Domain;
using TestWebApplication.Models.ViewModels;

namespace TestWebApplication.Controllers
{
    public class AdminTagsController : Controller
    {
        private BlogDBContext _blogDBContext;
        public AdminTagsController(BlogDBContext blogDBContext)
        {
            _blogDBContext = blogDBContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddTagRequest addTagRequest)
        {
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };
            _blogDBContext.Tags.Add(tag);
            _blogDBContext.SaveChanges();
            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public IActionResult List()
        {
            var tags = _blogDBContext.Tags.ToList();
            return View(tags);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            //var tag=_blogDBContext.Tags.Find(id);
            var tag = _blogDBContext.Tags.FirstOrDefault(x => x.Id == id);
            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };
                return View(editTagRequest);
            }
            return View(null);
        }

        [HttpPost]
        public IActionResult Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };
            var existingTag = _blogDBContext.Tags.Find(tag.Id);
            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;
                _blogDBContext.SaveChanges();
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

        [HttpPost]
        public IActionResult Delete(EditTagRequest editTagRequest)
        {
            var tag = _blogDBContext.Tags.Find(editTagRequest.Id);
            if (tag != null)
            {
                _blogDBContext.Tags.Remove(tag);
                _blogDBContext.SaveChanges();
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new {id = editTagRequest.Id});
        }
    }
}
