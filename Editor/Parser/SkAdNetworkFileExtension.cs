namespace Unity.Advertisement.IosSupport.Editor
{
    /// <summary>
    /// File extensions of the SKAdNetworks files that are read when building for iOS to collect SKAdNetwork identifiers.
    /// </summary>
    public static class SkAdNetworkFileExtension
    {
        /// <summary>
        /// Extension of an XML file containing SKAdNetwork identifiers.
        /// </summary>
        public const string XML = "xml";
        /// <summary>
        /// Extension of a JSON file containing SKAdNetwork identifiers.
        /// </summary>
        public const string JSON = "json";
        /// <summary>
        /// No extension, used for a file that lists URLs of XML or JSON files containing SKAdNetwork identifiers.
        /// </summary>
        public const string NONE = "";
    }
}
