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

        // Encapsulate list. Good practice, although not really necessary 
        // in this case since, since there are no current business rules around
        // adding and removing 
        private List<Func<int ,string>> substitutionRules { get; set; }
        public IEnumerable<Func<int, string>> SubstitutionRules()
        {
            return substitutionRules;
        }
        public void AddSubstitutionRule(Func<int, string> rule)
        {
            substitutionRules.Add(rule);
        }
        public void RemoveSubstitutionRule(Func<int, string> rule)
        {
            substitutionRules.Remove(rule);
        }
        public bool IncludeDefaultSubs { get; set; }
        public IEnumerable<string> Execute(int beginning, int inclusiveEnd)
        {
            //TODO Better Error Handling
            if (beginning >= inclusiveEnd || beginning <= 0 || inclusiveEnd <= 0) { throw new Exception("Starting number must be less then ending number, greater than zero and must be positive"); }
            var result = new List<string>();
            if (this.IncludeDefaultSubs)
            {
                AddSubstitutionRule(this.addDefautSubstitutions());
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
