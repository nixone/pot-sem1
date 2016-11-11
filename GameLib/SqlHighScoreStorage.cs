﻿using System;
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
                var existing = (from r in context.Results
                                where r.PlayerName.Equals(result.PlayerName)
                                select r).First();

                if (existing != null)
                {
                    existing.Score = Math.Max(existing.Score, result.Score);
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