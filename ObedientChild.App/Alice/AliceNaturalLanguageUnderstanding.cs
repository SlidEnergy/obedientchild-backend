using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObedientChild.App.Alice
{
    public class AliceNaturalLanguageUnderstanding
    {
        public IEnumerable<string> Tokens { get; set; }
        public IEnumerable<AliceEntity> Entitites { get; set; }

        public dynamic Intents { get; set; }
    }

    public class AliceEntity
    {
        public string Type { get; set; }

        public AliceEntityTokens Tokens { get; set; }

        public dynamic Value { get; set; }
    }

    public class AliceEntityTokens
    {
        public int Start { get; set; }

        public int End { get; set; }
    }
}
