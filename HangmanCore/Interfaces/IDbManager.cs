using HangmanCore.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanCore.Interfaces
{
    public interface IDbManager
    {
        Task InitDatabase();

        Task AddWord(Word word);

        Task AddWords(List<Word> words);

        Task ReadWords();

        Task AddUser();

        Task ClearTable(String tableName);
    }
}
