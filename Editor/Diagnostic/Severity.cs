namespace Unity.ProjectAuditor.Editor.Diagnostic
{
    /// <summary>
    /// Severity of an issue
    /// </summary>
    public enum Severity
    {
        /// <summary>
        /// Default severity
        /// </summary>
        Default = 0,

        /// <summary>
        /// An error that will prevent a successful build
        /// </summary>
        /// <description>For example, a code compile error encountered during code analysis</description>
        Error = 1,

        /// <summary>
        /// Critical impact on performance, quality or functionality
        /// </summary>
        Critical,

        /// <summary>
        /// Significant impact
        /// </summary>
        Major,

        /// <summary>
        /// Moderate impact
        /// </summary>
        Moderate,

        /// <summary>
        /// Minor impact
        /// </summary>
        Minor,

        /// <summary>
        /// A compiler warning encountered during code analysis
        /// </summary>
        Warning = Moderate,

        /// <summary>
        /// Something which is reported for informational purposes only, not necessarily a problem
        /// </summary>
        Info = Minor,

        /// <summary>
        /// Suppressed, ignored by UI and build
        /// </summary>
        None,

        /// <summary>
        /// Not visible to user
        /// </summary>
        Hidden,
    }
}
