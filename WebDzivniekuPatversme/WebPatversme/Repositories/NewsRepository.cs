﻿using System;
using WebDzivniekuPatversme.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using WebDzivniekuPatversme.Repository.Interfaces;

namespace WebDzivniekuPatversme.Repository
{
    public class NewsRepository : INewsRepository
    {
        private readonly WebShelterDbContext _dbcontext;

        public NewsRepository(
            WebShelterDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public List<News> GetAllNews()
        {
            List<News> list = new List<News>();

            using (MySqlConnection conn = _dbcontext.GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand("select * from News", conn);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new News()
                        {
                            NewsID = Convert.ToString(reader["ID"]),
                            DateCreated = Convert.ToDateTime(reader["DateCreated"]),
                            Text = Convert.ToString(reader["Text"]),
                            ImagePath = Convert.ToString(reader["ImagePath"])
                        });
                    }
                }
            }
            return list;
        }

        public void CreateNewNews(News newNews)
        {
            using MySqlConnection conn = _dbcontext.GetConnection();
            conn.Open();

            string sqlQuerry = "INSERT INTO News (ID, DateCreated, Text, ImagePath) " +
                               "VALUES (\"" + newNews.NewsID + "\", \"" + newNews.DateCreated.Year + "-" + newNews.DateCreated.Month + "-" + newNews.DateCreated.Day + "\", \"" + newNews.Text + "\", \"" + newNews.ImagePath + "\");";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);

            var reader = cmd.ExecuteReader();
        }

        public void DeleteNews(News news)
        {
            using MySqlConnection conn = _dbcontext.GetConnection();
            conn.Open();

            string sqlQuerry = "Delete from news where ID = \"" + news.NewsID + "\";";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);

            var reader = cmd.ExecuteReader();
        }
    }
}