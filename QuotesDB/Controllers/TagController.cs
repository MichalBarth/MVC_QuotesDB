using Microsoft.AspNetCore.Mvc;
using QuotesDB.Data;
using QuotesDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuotesDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private ApplicationDbContext _db;

        public TagController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Tag>> GetTags()
        {
            return _db.Tags.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Tag> GetTag(int id)
        {
            var temp = _db.Tags.Find(id);
            return temp;
        }

        [HttpPut("{id}")]
        public IActionResult PutTag(int id, Tag tag)
        {

        }

        [HttpPost]
        public ActionResult<Tag> PostTag(Tag tag)
        {
            _db.Tags.Add(tag);
            _db.SaveChanges();
            return tag;
        }

        [HttpDelete("{id}")]
        public ActionResult<Tag> DeleteTag(int id)
        {
            var temp = _db.Tags.SingleOrDefault(x => x.Id == id);
            _db.Tags.Remove(temp);
            _db.SaveChanges();
            return temp;
        }
    }
}
