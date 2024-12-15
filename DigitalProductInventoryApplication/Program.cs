using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DigitalProductInventoryApplication
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Create lists to store products and categories
            List<ProductBase> products = new List<ProductBase>();
            List<CategoryBase> categories = new List<CategoryBase>();

            // Example of creating a DigitalBook instance using the FactoryPattern
            ProductBase digitalBook = FactoryPattern<DigitalBook, ProductBase>.GetInstance();

            // Add properties to the DigitalBook instance
            AddPropertiesToProduct(digitalBook, 1, "The Old Man and the Sea", 1);

            // Add the DigitalBook instance to the products list
            products.Add(digitalBook);

            // Similar steps for creating instances of Movie and MusicRecording
            ProductBase movie = FactoryPattern<Movie, ProductBase>.GetInstance();
            AddPropertiesToProduct(movie, 2, "Highlander", 2);
            products.Add(movie);

            movie = FactoryPattern<Movie, ProductBase>.GetInstance();
            AddPropertiesToProduct(movie, 3, "Shawshank Redemption", 2);
            products.Add(movie);

            ProductBase album = FactoryPattern<MusicRecording, ProductBase>.GetInstance();
            AddPropertiesToProduct(album, 4, "Iron Man Soundtrack", 3);
            products.Add(album);

            // Similar steps for creating instances of CategoryBase and its derived classes
            CategoryBase digitalBookCategory = FactoryPattern<DigitalBookCategory, CategoryBase>.GetInstance();
            AddPropertiesToCategory(digitalBookCategory, 1, "Book", "Books digitised for download");
            categories.Add(digitalBookCategory);

            CategoryBase movieCategory = FactoryPattern<MovieCategory, CategoryBase>.GetInstance();
            AddPropertiesToCategory(movieCategory, 2, "Movie", "Movies digitised for download");
            categories.Add(movieCategory);

            CategoryBase musicCategory = FactoryPattern<MusicCategory, CategoryBase>.GetInstance();
            AddPropertiesToCategory(musicCategory, 3, "Music", "Music digitised for download");
            categories.Add(musicCategory);

            // Query products and categories, and display the results
            var queryResults = GetProducts(products, categories);

            foreach (var result in queryResults)
            {
                Console.WriteLine($"Product Id: {result.ProductId}");
                Console.WriteLine($"Title: {result.Title}");
                Console.WriteLine($"Category: {result.Category}");
                Console.WriteLine($"Category Description: {result.CategoryDescription}");
                Console.WriteLine();
            }
            Console.ReadKey();
        }

        // Query products and categories and return a list of ProductViewModel
        private static IEnumerable<ProductViewModel> GetProducts(List<ProductBase> products, List<CategoryBase> categories)
        {
            return from p in products
                   join c in categories on p.CategoryId equals c.ID
                   select new ProductViewModel
                   {
                       ProductId = p.ID,
                       Title = p.Title,
                       Category = c.Title,
                       CategoryDescription = c.Description
                   };
        }

        // Helper method to add properties to a CategoryBase instance
        private static void AddPropertiesToCategory(CategoryBase category, int id, string title, string description)
        {
            category.ID = id;
            category.Title = title;
            category.Description = description;
        }

        // Helper method to add properties to a ProductBase instance
        private static void AddPropertiesToProduct(ProductBase product, int id, string title, int categoryId)
        {
            product.ID = id;
            product.Title = title;
            product.CategoryId = categoryId;
        }
    }

    // Data model classes

    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string CategoryDescription { get; set; }
    }

    public interface IPrimaryProperties
    {
        int ID { get; set; }
        string Title { get; set; }
    }

    public abstract class ProductBase : IPrimaryProperties
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
    }

    public class Movie : ProductBase
    {
        public string Director { get; set; }
        public string Producer { get; set; }
    }

    public class DigitalBook : ProductBase
    {
        public string author { get; set; }
    }

    public class MusicRecording : ProductBase
    {
        public string RecordCompany { get; set; }
    }

    public abstract class CategoryBase : IPrimaryProperties
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class MovieCategory : CategoryBase
    {
    }

    public class DigitalBookCategory : CategoryBase
    {
    }

    public class MusicCategory : CategoryBase
    {
    }

    // Factory pattern class

    public static class FactoryPattern<T, U> where T : class, U, new()
                                            where U : class, IPrimaryProperties
    {
        public static U GetInstance()
        {
            U objT;
            objT = new T();
            return objT;
        }
    }
}
