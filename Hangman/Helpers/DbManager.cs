using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using HangmanCore.DbModels;
using HangmanCore.Helpers;
using HangmanCore.Interfaces;
using SQLite;
using Exception = Java.Lang.Exception;

namespace Hangman.Helpers
{
    public class DbManager : IDbManager
    {
        private readonly String dbPath;
        private readonly SQLiteAsyncConnection sqLConnection;
        private readonly TaskScheduler uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();
        private readonly List<Word> words = new List<Word>();
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

                    //Mark database created
                    initialized = true;
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

        public async Task ReadWords()
        {
            try
            {
                List<Word> words = await sqLConnection.Table<Word>().ToListAsync();
                int x = 0;
            }
            catch (System.Exception ex)
            {
                ex.ToString();
            }
        }

        public Task AddWord(Word word)
        {
            throw new NotImplementedException();
        }

        public Task AddWords(List<Word> words)
        {
            throw new NotImplementedException();
        }


        public Task AddUser()
        {
            throw new NotImplementedException();
        }

        public Task ClearTable(string tableName)
        {
            throw new NotImplementedException();
        }

        public async Task AddWords()
        {
            try
            {
                string line, word, hint;
                String[] elements;

                //read from file and store data
                Stream input =
                    SplashScreenActivity.CurrentActivity.Assets.Open(String.Format("{0}{1}", Constants.CURRENT_LANGUAGE,
                        ".txt"));

                // Use a StreamReader to read the data
                using (var sr = new StreamReader(input))
                {
                    while ((line = sr.ReadLine()) != null)
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
                }

                foreach (Word entry in words)
                {
                    int result = await sqLConnection.InsertAsync(entry);
                    if (result != 1)
                    {
                        throw new Exception("failed at insert!");
                    }
                }
            }
            catch (System.Exception ex)
            {
                ex.ToString();
            }
        }
    }
}