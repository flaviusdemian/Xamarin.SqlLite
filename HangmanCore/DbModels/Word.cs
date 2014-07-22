using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HangmanCore.Helpers;

namespace HangmanCore.DbModels
{
    public class Word
    {
        public Guid Id { get; set; }

        public String Value { get; set; }
        
        public String Hint { get; set; }

        public String Language { get; set; }

        public Enums.DifficultyLevel DifficultyLevel { get; set; }

        public String Category { get; set; }    
    }
}
