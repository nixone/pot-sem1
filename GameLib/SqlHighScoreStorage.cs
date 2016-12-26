using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    /// <summary>
    /// Implementation of Sql high score storage
    /// </summary>
    public class SqlHighScoreStorage : IHighScoreStorage
    {
        /// <summary>
        /// Database context for entity framework
        /// </summary>
        public class HighScoreContext : DbContext
        {
            public HighScoreContext(String connectionString) : base(connectionString)
            {
                SaveChanges();
            }

            public DbSet<GameResult> Results { get; set; }
        }

        private String _connectionString;
            
        /// <summary>
        /// Creates new sql high score storage with a specified connection string
        /// </summary>
        /// <param name="connectionString">database connection string</param>
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
                var matching = (from r in context.Results
                                where r.PlayerName.Equals(result.PlayerName)
                                select r);
                if (matching.Count() > 1)
                {
                    var existing = matching.First();
                    if (existing.Score < result.Score)
                    {
                        existing.Score = result.Score;
                        existing.StartTime = result.StartTime;
                        existing.StopTime = result.StopTime;
                        existing.MachineName = result.MachineName;
                    }
                }
                else
                {
                    context.Results.Add(result);
                }

                context.SaveChanges();
            }
        }
    }
}
