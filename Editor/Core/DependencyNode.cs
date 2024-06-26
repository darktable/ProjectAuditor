using System;
using System.Collections.Generic;
using System.Linq;
using Unity.ProjectAuditor.Editor.Utils;

namespace Unity.ProjectAuditor.Editor.Core
{
    /// <summary>
    /// Represents a node in a dependency tree for reporting issues in the UI.
    /// This is used to represent call trees, and dependencies between assets, assemblies, or packages.
    /// </summary>
    internal abstract class DependencyNode
    {
        /// <summary>
        /// A list of this node's children in the dependency tree
        /// </summary>
        protected List<DependencyNode> m_Children = new List<DependencyNode>(1);

        /// <summary>
        /// The location represented by this node
        /// </summary>
        public Location location;

        /// <summary>
        /// Whether this node forms part of a performance-critical context
        /// </summary>
        /// <description>
        /// If the node represents part of a code call stack, perfCriticalContext is true if that callstack
        /// includes a known update method (for example, a MonoBehaviour.Update())
        /// </description>
        public bool perfCriticalContext;

        /// <summary>
        /// This node's name
        /// </summary>
        public string name => GetName();

        /// <summary>
        /// A prettified, UI-friendly version of this node's name
        /// </summary>
        public string prettyName => GetPrettyName();

        /// <summary>
        /// Checks whether this node has a valid list of children
        /// </summary>
        /// <returns>True if the node has a valid list of children. Otherwise, returns false.</returns>
        public bool HasValidChildren()
        {
            return m_Children != null;
        }

        /// <summary>
        /// Checks whether this node has at least one valid child
        /// </summary>
        /// <returns>True if the node has at least one valid child. Otherwise, returns false.</returns>
        public bool HasChildren()
        {
            return m_Children != null && m_Children.Count > 0;
        }

        /// <summary>
        /// Adds a child to this node
        /// </summary>
        /// <param name="child">The node to add as a child of this one.</param>
        public void AddChild(DependencyNode child)
        {
            m_Children.Add(child);
        }

        /// <summary>
        /// Adds multiple children to this node
        /// </summary>
        /// <param name="children">An array of nodes to add as children of this one.</param>
        public void AddChildren(DependencyNode[] children)
        {
            // if any child is critical, make parent critical too
            // this is to propagate perfCriticalContext up to the root of the hierarchy
            if (children.Any(c => c.perfCriticalContext))
                perfCriticalContext = true;
            m_Children.AddRange(children);
        }

        /// <summary>
        /// Gets a child node with the specified index
        /// </summary>
        /// <param name="index">The index into the node's child list (defaults to 0)</param>
        /// <returns>The child node with the given index</returns>
        public DependencyNode GetChild(int index = 0)
        {
            return m_Children[index];
        }

        /// <summary>
        /// Gets the number of children that this node has
        /// </summary>
        /// <returns></returns>
        public int GetNumChildren()
        {
            return m_Children.Count;
        }

        /// <summary>
        /// Sorts this node's children by their prettyName, in ascending alphabetical order.
        /// </summary>
        public void SortChildren()
        {
            m_Children = m_Children.OrderBy(c => c.prettyName).ToList();
        }

        /// <summary>
        /// Gets the node's "raw" name
        /// </summary>
        /// <returns>The node's name</returns>
        public abstract string GetName();

        /// <summary>
        /// Gets the node's "pretty" name, suitable for UI display
        /// </summary>
        /// <returns>The node's prettified name</returns>
        public abstract string GetPrettyName();

        /// <summary>
        /// Gets whether this node represents a performance-critical issue
        /// </summary>
        /// <returns>True if the issue is performance critical. Otherwise, returns false.</returns>
        public abstract bool IsPerfCritical();
    }
}
