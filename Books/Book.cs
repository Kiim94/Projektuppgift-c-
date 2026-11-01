using System;
using System.Collections.Generic;
using System.Text;

namespace ProjektUppgift.Book;
public class Book
{
    public string Title { get; private set; }
    public string Author { get; private set; }
    //bok är oläst som standard
    public bool IsRead { get; set; }
    //rating är 0 som standard
    public int Rating { get; set; }
    public int Pages { get; private set; }
    public string Genre { get; private set; }
    public DateTime DateAdded { get; set; } = DateTime.Now;

    public Book(string title, string author, int pages, string genre)
    {
        Title = title;
        Author = author;
        Pages = pages; 
        Genre = genre;
    }
}
