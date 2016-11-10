using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    public class SqlHighScoreStorage : IHighScoreStorage
    {
        public class HighScoreContext : DbContext
        {
            public HighScoreContext(String connectionString) : base(connectionString)
            {
            }

            public DbSet<GameResult> Results { get; set; }
        }

        private String _connectionString;
            
        public SqlHighScoreStorage(String connectionString)
        {
            _connectionString = connectionString;
        }

        public IList<GameResult> GetTopTen()
        {
            using (HighScoreContext context = new HighScoreContext(_connectionString))
            {
                return (
                    from r in context.Results
                    orderby r.Score descending
                    select r
                        ).Take(10).ToList();
            }
        }

        public void SaveResult(GameResult result)
        {
            using (HighScoreContext context = new HighScoreContext(_connectionString))
            {
                context.Results.Add(result);
                context.SaveChanges();
            }
        }
    }
}
