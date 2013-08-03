using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Jumbleblocks.Net.Files
{
    public class ValidationResultCollection : IEnumerable<ValidationResult>
    {
        private readonly List<ValidationResult> _validationResults = new List<ValidationResult>(); 
        
        public void Add(ValidationResult result)
        {
            _validationResults.Add(result);
        }

        public void AddRange(IEnumerable<ValidationResult> results)
        {
            _validationResults.AddRange(results);
        }
        
        public IEnumerable<ValidationResult> AllResults
        {
            get { return _validationResults; }
        }

        public IEnumerable<ValidationResult> InvalidResults
        {
            get { return SelectResults(x => !x.IsValid); }
        }

        public IEnumerable<ValidationResult> ValidResults
        {
            get { return SelectResults(x => x.IsValid); }
        } 

        public bool AllRulesAreValid
        {
            get { return !InvalidResults.Any(); }
        }

        private IEnumerable<ValidationResult> SelectResults(Func<ValidationResult, bool> selector)
        {
            return _validationResults.Where(selector).ToArray();
        }

        public IEnumerator<ValidationResult> GetEnumerator()
        {
            return _validationResults.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
