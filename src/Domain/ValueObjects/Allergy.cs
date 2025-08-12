using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public sealed class Allergy
    {
        public string Name { get; private set; } = default!;
        public string? Severity { get; private set; }

        private Allergy() { }
        public Allergy(string name, string? severity = null)
        {
            Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentException("name required") : name.Trim();
            Severity = string.IsNullOrWhiteSpace(severity) ? null : severity.Trim();
        }
    }
}
