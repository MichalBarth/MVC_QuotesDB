using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuotesDB.Data;
using QuotesDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuotesDB.Controllers
{
    public class QuoteController
    {
        private Random random;
        private ApplicationDbContext _db;
        private List<Tag> tags;

        public QuoteController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET api/<QuoteController>
        // get random quote
        [HttpGet]
        public ActionResult<Quote> Get()
        {
            random = new Random();
            int num = _db.Quotes.Count();
            int randomNum = random.Next(0, num + 1);

            return _db.Quotes.Find(randomNum);
        }

        // POST api/<QuoteController>
        // insert new quote (without tags)
        [HttpPost]
        public ActionResult<Quote> Insert([FromBody] Quote value)
        {
            if (value == null) return NotFound();
            else
            {
                _db.Quotes.Add(value);
                _db.SaveChanges();
                return value;
            }
        }

        // GET api/<QuoteController/5>
        // get quote with id 5
        [HttpGet("{id}")]
        public ActionResult<Quote> Get(int id)
        {
            if (_db.Quotes.Contains(new Quote { Id = id })) return _db.Quotes.Find(id);
            else
            {
                return NotFound();
            }
        }

        // DELETE api/<QuoteController>/5
        // delete quote with id 5
        [HttpDelete("{id?}")]
        public ActionResult<Quote> Delete(int id)
        {
            if (_db.Quotes.Contains(new Quote { Id = id }))
            {
                var temp = _db.Quotes.Find(id);
                _db.Quotes.Remove(temp);
                _db.SaveChanges();
                return Success();
            } else
            {
                return NotFound();
            }
        }

        // POST api/<QuoteController/5/tags>
        // link new tags with quote 5
        [HttpPost("{id}/tags")]
        public ActionResult<IEnumerable<Tag>> InsertTags([FromBody] IEnumerable<int> tagIds, int id)
        {
            if (_db.Quotes.Contains(new Quote { Id = id }))
            {
                //select vybraného citátu
                var quote = _db.Quotes.Find(id);
                foreach (var i in tagIds)
                {
                    //pro každý tag s id vybraném v IEnumerable se vytvoří vazba s quote
                    var tag = _db.Tags.Find(i);
                    _db.TagQuotes.Add(new TagQuote { Quote = quote, Tag = tag });
                }
                _db.SaveChanges();
                return Success();
            } else
            {
                return NotFound();
            }
        }

        // DELETE api/<QuoteController/5/tags>
        // unlink tags connected with quote 5
        [HttpDelete("{id}/tags")]
        public ActionResult<IEnumerable<Tag>> DeleteTags([FromBody] IEnumerable<int> tagIds, int id)
        {
            if (_db.Quotes.Contains(new Quote { Id = id}))
            {
                foreach (var i in tagIds)
                {
                    var tagQuote = _db.TagQuotes.Find(i);
                    _db.TagQuotes.Remove(_db.TagQuotes.Where(x => x.QuoteId == id).SingleOrDefault(x => x.Tag == tag));
                }
                _db.SaveChanges();
                return Success();
            } else
            {
                return NotFound();
            }
        }

        // GET api/<QuoteController/5/tags>
        // get linked tags with quote 5
        [HttpGet("{id}/tags")]
        public ActionResult<IEnumerable<Tag>> GetTags(int id)
        {
            if (_db.Quotes.Contains(new Quote { Id = id }))
            {
                var quote = _db.Quotes.Include(t => t.TagQuotes).SingleOrDefault(x => x.Id == id);
                tags = new List<Tag>();

                foreach (var i in quote.TagQuotes)
                {
                    //přidání vybraných tagů, obsažených ve "variable quote" obsahující vazby do listu
                    tags.Add(i.Tag);
                }
                return tags;
            } else
            {
                return NotFound();
            }
        }
    }
}
