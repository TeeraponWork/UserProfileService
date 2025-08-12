using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public sealed class ChronicCondition
    {
        public string Name { get; private set; } = default!;
        public string? Notes { get; private set; }

        private ChronicCondition() { } // EF
        public ChronicCondition(string name, string? notes = null)
        {
            Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentException("name required") : name.Trim();
            Notes = string.IsNullOrWhiteSpace(notes) ? null : notes.Trim();
        }
    }
}
