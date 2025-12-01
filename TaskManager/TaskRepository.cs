using Dapper;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace TaskManager
{
    public class TaskRepository
    {
        private readonly string _databasePath = "tasks.db";
        private readonly string _connectionString;

        public TaskRepository()
        {
            _connectionString = $"Data Source={_databasePath};Version=3;";
            CreateDatabaseAndTable();
        }

        private void CreateDatabaseAndTable()
        {
            // Cria arquivo do banco se não existir
            if (!File.Exists(_databasePath))
            {
                SQLiteConnection.CreateFile(_databasePath);
            }

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string tableQuery = @"CREATE TABLE IF NOT EXISTS Tasks (
                                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                        Title TEXT NOT NULL,
                                        Description TEXT,
                                        Status TEXT NOT NULL
                                      );";
                connection.Execute(tableQuery);
            }
        }

        // CREATE
        public void Add(Task task)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO Tasks (Title, Description, Status) VALUES (@Title, @Description, @Status)";
                connection.Execute(insertQuery, task);
            }
        }

        // READ ALL
        public List<Task> GetAll()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string selectQuery = "SELECT * FROM Tasks";
                return connection.Query<Task>(selectQuery).AsList();
            }
        }

        // READ BY ID
        public Task GetById(int id)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string selectQuery = "SELECT * FROM Tasks WHERE Id = @Id";
                return connection.QueryFirstOrDefault<Task>(selectQuery, new { Id = id });
            }
        }

        // UPDATE
        public void Update(Task task)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string updateQuery = "UPDATE Tasks SET Title = @Title, Description = @Description, Status = @Status WHERE Id = @Id";
                connection.Execute(updateQuery, task);
            }
        }

        // DELETE
        public void Delete(int id)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string deleteQuery = "DELETE FROM Tasks WHERE Id = @Id";
                connection.Execute(deleteQuery, new { Id = id });
            }
        }
    }
}
