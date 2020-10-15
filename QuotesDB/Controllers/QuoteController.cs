﻿using Microsoft.AspNetCore.Mvc;
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
        Random random = new Random();
        private ApplicationDbContext _db;
        private int NumberOfQuotes;

        public QuoteController(ApplicationDbContext db)
        {
            _db = db;

            NumberOfQuotes = _db.Quotes.Count();
        }

        // GET api/<QuoteController>
        // get random quote
        [HttpGet]
        public ActionResult<Quote> Get()
        {
            int temp = random.Next(0, NumberOfQuotes + 1);
            return _db.Quotes.SingleOrDefault(x => x.Id == temp);
        }

        // POST api/<QuoteController>
        // insert new quote (without tags)
        [HttpPost]
        public ActionResult<Quote> Insert([FromBody] Quote value)
        {
            _db.Quotes.Add(value);
            _db.SaveChanges();
            return value;
        }

        // GET api/<QuoteController/5>
        // get quote with id 5
        [HttpGet("{id}")]
        public ActionResult<Quote> Get(int id)
        {
            return _db.Quotes.Find(id);
        }

        // DELETE api/<QuoteController>/5
        // delete quote with id 5
        [HttpDelete("{id?}")]
        public ActionResult<Quote> Delete(int id)
        {
            //var temp = _db.Quotes.Single(x => x.Id == id);
            var temp = _db.Quotes.Find(id);
            _db.Quotes.Remove(temp);
            _db.SaveChanges();
            return temp;
        }

        // POST api/<QuoteController/5/tags>
        // link new tags with quote 5
        [HttpPost("{id}/tags")]
        public ActionResult<IEnumerable<Tag>> InsertTags([FromBody] IEnumerable<int> tagIds)
        {

        }

        // DELETE api/<QuoteController/5/tags>
        // unlink tags connected with quote 5
        [HttpDelete("{id}/tags")]
        public ActionResult<IEnumerable<Tag>> DeleteTags(int id, [FromBody] IEnumerable<int> tagIds)
        {

        }

        // GET api/<QuoteController/5/tags>
        // get linked tags with quote 5
        [HttpGet("{id}/tags")]
        public ActionResult<IEnumerable<Tag>> GetTags(int id)
        {
            var temp = _db.Quotes.Where(x => x.Id == id).Include(t => t.TagQuotes).ThenInclude(g => g.Tag);
            //return ...;
        }
    }
}
