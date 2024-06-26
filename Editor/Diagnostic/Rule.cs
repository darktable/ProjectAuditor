using System;

namespace Unity.ProjectAuditor.Editor.Diagnostic
{
    [Serializable]
    internal class Rule : IEquatable<Rule>
    {
        public Severity severity;
        public string filter;

        public string id;

        public bool Equals(Rule other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (other.GetType() != GetType()) return false;
            return id == other.id && filter == other.filter && severity == other.severity;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Rule);
        }

        public static bool operator==(Rule a, Rule b)
        {
            if (ReferenceEquals(a, null))
            {
                if (ReferenceEquals(b, null))
                    return true;
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator!=(Rule a, Rule b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 23 + id.GetHashCode();
                hash = hash * 23 + filter.GetHashCode();
                hash = hash * 23 + severity.GetHashCode();
                return hash;
            }
        }
    }
}
