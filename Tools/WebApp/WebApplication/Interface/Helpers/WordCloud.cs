using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Helpers
{
    public static class WordCloud
    {
        public static List<Word> Get(string words)
        {
            var punctuation = words.Where(Char.IsPunctuation).Distinct().ToArray();
            var splitWords = words.Split().Select(x => x.Trim(punctuation));

            int totaltags = words.Count();

            var groupedWords = splitWords
                .GroupBy(w => w)
                .Select(w => new { Word = w.Key, Total = w.Count() });

            var list = new List<Word>();

            foreach(var word in groupedWords)
            {
                list.Add(new Word
                {
                    Name = word.Word,
                    Value = word.Total
                });
            }

            return list;
        }
    }
}
