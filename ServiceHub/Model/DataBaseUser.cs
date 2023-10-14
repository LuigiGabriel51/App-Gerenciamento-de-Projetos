using SQLite;

namespace ServiceHub.Model
{
    public class DataBaseUser
    {
        private readonly SQLiteConnection _database;

        public DataBaseUser()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "entities.db");
            _database = new SQLiteConnection(dbPath);
            _database.CreateTable<UserModel>();
        }

        public List<UserModel> List()
        {
            return _database.Table<UserModel>().ToList();
        }

        public int Create(UserModel entity)
        {
            return _database.Insert(entity);
        }

        public int Update(UserModel entity)
        {
            return _database.Update(entity);
        }

        public int Delete(UserModel entity)
        {
            return _database.Delete(entity);
        }
    }
}
