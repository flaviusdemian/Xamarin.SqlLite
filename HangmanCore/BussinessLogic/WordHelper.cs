using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HangmanCore.Interfaces;

namespace HangmanCore.BussinessLogic
{
    public class WordHelper
    {
        private IDbManager _DbManager = null;

        public WordHelper(IDbManager dbManager)
        {
            _DbManager = dbManager;
        }
    }
}
