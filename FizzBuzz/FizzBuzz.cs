using System;
using System.Collections.Generic;
using System.Linq;

namespace FizzBuzzLibrary
{
    public class FizzBuzz
    {
        public FizzBuzz()
        {
            substitutionRules = new List<Func<int, string>>();
        }

        public List<Func<int ,string>> substitutionRules { get; set; }
        public bool includeDefaultSubs { get; set; }
        public IEnumerable<string> Execute(int beginning, int inclusiveEnd)
        {
            //TODO Better Error Handling
            if (beginning >= inclusiveEnd || beginning <= 0 || inclusiveEnd <= 0) { throw new Exception("Starting number must be less then ending number, greater than zero and must be positive"); }
            var result = new List<string>();
            if (includeDefaultSubs)
            {
                substitutionRules.Add(this.addDefautSubstitutions());
            } 
            while (beginning <= inclusiveEnd)
            {
                var value = string.Empty;
                foreach (var func in substitutionRules)
                {
                    try
                    {
                        value = func.Invoke(beginning);
                    }
                    catch (Exception ex)
                    {
                        //TODO Log Exception somehow
                    }
                    if (!string.IsNullOrEmpty(value))
                    {
                        break;
                    }
                }
                result.Add(!string.IsNullOrEmpty(value) ? value : beginning.ToString());
                beginning++;
            }
            return result;
        }

        private Func<int, string> addDefautSubstitutions()
        {
            return x =>
                { 
                    var sub = x % 3 == 0 ? "fizz" : "";
                    if (x % 5 == 0)
                    {
                        if (!string.IsNullOrEmpty(sub))
                        {
                            sub += "\\";
                        }
                        sub += "buzz";
                    }
                    return sub;
                };
        }
    }
}
