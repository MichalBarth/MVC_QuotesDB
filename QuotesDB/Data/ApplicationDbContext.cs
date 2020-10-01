using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuotesDB.Models;

namespace QuotesDB.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Quote>().HasData(new Quote { Id = 1, Text = "Kdo jinému jámu kopá, má u Luďka brigádu.", Date = new DateTime(2020, 8, 17) });
            builder.Entity<Quote>().HasData(new Quote { Id = 2, Text = "V hospodě musí být podtlak, nějak mě to vcuclo.", Date = new DateTime(2019, 12, 29) });
            builder.Entity<Quote>().HasData(new Quote { Id = 3, Text = "Mám rád pivo.", Date = new DateTime(2007, 3, 14) });

            builder.Entity<Tag>().HasData(new Tag { Id = 1, Name = "Michal Barth", Category = Category.Author });
            builder.Entity<Tag>().HasData(new Tag { Id = 2, Name = "Neznámý", Category = Category.Author });
            builder.Entity<Tag>().HasData(new Tag { Id = 3, Name = "Pouliční modra", Category = Category.Genre});   

            builder.Entity<TagQuote>().HasData(new TagQuote { QuoteId = 1, TagId = 2 });
            builder.Entity<TagQuote>().HasData(new TagQuote { QuoteId = 2, TagId = 1 });
            builder.Entity<TagQuote>().HasData(new TagQuote { QuoteId = 2, TagId = 3 });

            builder.Entity<TagQuote>()
                .HasOne(g => g.Quote)
                .WithMany(u => u.TagQuotes)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<TagQuote>()
                .HasOne(g => g.Tag)
                .WithMany(u => u.TagQuotes)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Tag> Tags { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<TagQuote> TagQuotes { get; set; }
    }
}
