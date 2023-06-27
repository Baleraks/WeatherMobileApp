using LiteDB;
using WeatherApp.Model;

namespace WeatherApp.Helpers
{
    internal class DataHelper
    {

        public List<Review> GetAll()
        {
            using var db = new LiteDatabase(GetFilePath());

            var collection = db.GetCollection<Review>("reviewes");
            var reviewes = collection.Query().ToList();
            return reviewes;

        }
        public void Save(Review review)
        {
            using var db = new LiteDatabase(GetFilePath());

            var collection = db.GetCollection<Review>("reviewes");
            collection.Insert(review);

        }

        private string GetFilePath()
        {
            var path = FileSystem.Current.AppDataDirectory;
            return Path.Combine(path, "reviewes.db");
        }

    }
}
