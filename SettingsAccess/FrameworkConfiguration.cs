using System.Xml.Serialization;

namespace NetEti.FileTools
{
    /// <summary>
    /// Datenklasse für die Serialisierung und De-Serialisierung von User-Parametern
    /// im .Net-Framework Xml-Format (app.config/web.config).
    /// Hinweise:
    ///  - Berücksichtigt AppSettings und UserSettings;
    ///  - Eine Übersetzung des alten Framework-Xml-Formats in Json ist nicht vorgesehen;
    ///    Stattdessen soll für neuere Versionen (ab .Net-Core) das neue Json-Format
    ///    verwendet werden, welches einfach in ein Dictionary&lt;string, string&gt;
    ///    deserialisiert wird.
    /// </summary>
    /// <remarks>
    /// Autor: Erik Nagel
    /// 06.06.2025 Erik Nagel: erstellt.
    /// </remarks>
    [XmlRoot("configuration")]
    public class FrameworkConfiguration
    {
        /// <summary>
        /// Der Serializer versucht, das &lt;appSettings&gt;-Element in diese Eigenschaft zu mappen.
        /// </summary>
        [XmlElement("appSettings")]
        public FrameworkConfigAppSettings? AppSettings { get; set; }

        /// <summary>
        /// Der Serializer versucht, das &lt;userSettings&gt;-Element in diese Eigenschaft zu mappen.
        /// </summary>
        [XmlElement("userSettings")]
        public FrameworkConfigAppSettings? UserSettings { get; set; }

        /// <summary>
        /// Diese Eigenschaft wird bei der Serialisierung ignoriert.
        /// Sie dient als bequemer, einheitlicher Zugriffspunkt auf die Einstellungen,
        /// egal welcher Knotenname in der XML verwendet wurde.
        /// Der Null-Coalescing-Operator (??) gibt den ersten Wert zurück, der nicht null ist.
        /// </summary>
        [XmlIgnore]
        public FrameworkConfigAppSettings? ConfigAppSettings => AppSettings ?? UserSettings;
    }

    /// <summary>
    /// Container für eine Liste von AppSettings.
    /// </summary>
    public class FrameworkConfigAppSettings
    {
        /// <summary>
        /// Liste von AppSettings-Einträgen, die hinzugefügt werden sollen.
        /// </summary>
        [XmlElement("add")]
        public List<FrameworkConfigAppSettingEntry>? Add { get; set; }
    }

    /// <summary>
    /// Repräsentiert einen einzelnen Eintrag in den AppSettings.
    /// </summary>
    public class FrameworkConfigAppSettingEntry
    {
        /// <summary>
        /// Der Schlüssel des AppSettings-Eintrags.
        /// </summary>
        [XmlAttribute("key")]
        public string? Key { get; set; }

        /// <summary>
        /// Der Wert des AppSettings-Eintrags.
        /// </summary>
        [XmlAttribute("value")]
        public string? Value { get; set; }
    }
}
