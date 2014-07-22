using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HangmanCore.DbModels;
using HangmanCore.Helpers;
using HangmanCore.Interfaces;
using SQLite;

namespace HangmanIOS.Helpers
{
    internal class DbManager : IDbManager
    {
        private readonly String dbPath;
        private readonly TaskScheduler uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();
        private readonly List<Word> words = new List<Word>();
        private SQLiteAsyncConnection sqLConnection;
        private bool initialized;

        public DbManager()
        {
            dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), Constants.DB_NAME);
            sqLConnection = new SQLiteAsyncConnection(dbPath, true);
        }

        public async Task InitDatabase()
        {
            try
            {
                //Create the tables
                await sqLConnection.CreateTablesAsync<Word, User>();

                //Count number of assignments
                int count = await sqLConnection.Table<Word>().CountAsync();

                //If no assignments exist, insert our initial data
                if (count == 0)
                {
                    //Wait for inserts
                    await AddWords();
                }
                else
                {
                    await ReadWords();
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public async Task AddWord(Word word)
        {
            throw new NotImplementedException();
        }

        public async Task AddWords(List<Word> words)
        {
            throw new NotImplementedException();
        }

        public async Task AddWords()
        {
            //read from file and store data
            try
            {
                string word, hint;
                String[] elements;

                List<String> lines = File.ReadLines(String.Format("{0}{1}", Constants.CURRENT_LANGUAGE, ".txt")).ToList();
                if (lines != null)
                {
                    foreach (string line in lines)
                    {
                        elements = line.Split(new[] { "#" }, StringSplitOptions.RemoveEmptyEntries);
                        if (elements != null && elements.Length >= 2)
                        {
                            word = elements[0];
                            hint = elements[1];

                            words.Add(new Word
                            {
                                Value = word,
                                Hint = hint,
                                Language = "English",
                                DifficultyLevel = Enums.DifficultyLevel.Medium,
                                Id = Guid.NewGuid()
                            });
                        }
                    }

                    foreach (Word entry in words)
                    {
                        int result = await sqLConnection.InsertAsync(entry);
                        if (result != 1)
                        {
                            throw new Exception("failed at insert");
                        }
                    }
                    initialized = true;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public async Task ReadWords()
        {
            try
            {
                List<Word> words = await sqLConnection.Table<Word>().ToListAsync();
                int x = 0;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public async Task AddUser()
        {
            throw new NotImplementedException();
        }

        public async Task ClearTable(string tableName)
        {
            throw new NotImplementedException();
        }
    }
}