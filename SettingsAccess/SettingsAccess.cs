using NetEti.Globals;
using NetEti.FileTools;
using Microsoft.Extensions.Configuration;

namespace NetEti.ApplicationEnvironment
{
    /// <summary>
    /// Zugriffe auf die über das .Net-Framework verwalteten AppSettings<br></br>
    ///           Implementiert IGetStringValue.
    /// </summary>
    /// <remarks>
    /// File: SettingsAccess.cs<br></br>
    /// Autor: Erik Nagel, NetEti<br></br>
    ///<br></br>
    /// 13.03.2012 Erik Nagel: erstellt<br></br>
    /// </remarks>
    public class SettingsAccess : IGetStringValue
    {
        /// <summary>
        /// Enthält alle aus der XmlDatei eingelesenen (Anwendungs-)Einstellungen.
        /// </summary>
        public Dictionary<string, string?>? Settings { get; private set; }

        #region IGetStringValue Members

        /// <summary>
        /// Liefert genau einen Wert zu einem Key. Wenn es keinen Wert zu dem
        /// Key gibt, wird defaultValue zurückgegeben.
        /// </summary>
        /// <param name="key">Der Zugriffsschlüssel (string)</param>
        /// <param name="defaultValue">Das default-Ergebnis (string)</param>
        /// <returns>Der Ergebnis-String</returns>
        public string? GetStringValue(string key, string? defaultValue)
        {
            string? rtn = defaultValue;
            if (this.Settings != null && this.Settings.ContainsKey(key))
            {
                rtn = this.Settings[key];
            }
            return rtn;
        }

        /// <summary>
        /// Liefert ein string-Array zu einem Key. Wenn es keinen Wert zu dem
        /// Key gibt, wird defaultValue zurückgegeben.
        /// Liefert nur einen Einzelwert als Array verpackt, muss ggf. spaeter
        /// erweitert werden.
        /// </summary>
        /// <param name="key">Der Zugriffsschlüssel (string)</param>
        /// <param name="defaultValues">Das default-Ergebnis (string[])</param>
        /// <returns>Das Ergebnis-String-Array</returns>
        public string?[]? GetStringValues(string key, string?[]? defaultValues)
        {
            string? rtn = GetStringValue(key, null);
            if (rtn != null)
            {
                return new string[] { rtn };
            }
            else
            {
                return defaultValues;
            }
        }

        /// <summary>
        /// Liefert einen beschreibenden Namen dieses StringValueGetters,
        /// z.B. Name plus ggf. Quellpfad.
        /// </summary>
        public string Description { get; set; }

        #endregion IGetStringValue Members

        #region public members

        /// <summary>
        /// Konstruktor - setzt den internen Reader
        /// </summary>
        public SettingsAccess()
        {
            this.Description = "app.config";
            this.Settings = new Dictionary<string, string?>();

            // Zuerst die AppSettings im klassischen Format verarbeiten
            /*
             *  <?xml version="1.0"?>
             *  <configuration>
             *      <appSettings>
             *          <add key="Harry" value="Hirsch"/>
             *      </appSettings>
             *  </configuration>
             */
            System.Collections.Specialized.NameValueCollection settings1 = System.Configuration.ConfigurationManager.AppSettings;
            if (settings1.Count > 0)
            {
                foreach (string key in settings1.Keys)
                {
                    if (key != null)
                    {
                        this.Settings.Add(key, settings1[key]);
                    }
                }
            }

            // Dann die AppSettings im .Net 7.0 Format verarbeiten
            /* <?xml version="1.0" encoding="utf-8" ?>
             *  <configuration>
             *      <configSections>
             *          <sectionGroup name="userSettings" type="System.Configuration.UserSettingsGroup, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" >
             *              <section name="NetEti.DemoApplications.Properties.Settings" type="System.Configuration.ClientSettingsSection, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" allowExeDefinition="MachineToLocalUser" requirePermission="false" />
             *          </sectionGroup>
             *      </configSections>
             *      <userSettings>
             *          <NetEti.DemoApplications.Properties.Settings>
             *              <setting name="Harry" serializeAs="String">
             *                  <value>Hirsch</value>
             *              </setting>
             *          </NetEti.DemoApplications.Properties.Settings>
             *      </userSettings>
             *  </configuration>
             */
            string configPath = "";
            string? appName = System.Reflection.Assembly.GetEntryAssembly()?.GetName().Name;
            if (appName != null)
            {
                configPath = String.Format($"{appName}.dll.config");
                if (!File.Exists(configPath))
                {
                    configPath = String.Format($"{appName}.exe.config");
                }
                if (File.Exists(configPath))
                {
                    XmlAccess? xmlAccessor = new XmlAccess(configPath);
                    Dictionary<string, string?>? settings2 = xmlAccessor?.Settings;
                    if (settings2?.Count > 0)
                    {
                        foreach (string key in settings2.Keys)
                        {
                            if (!this.Settings.ContainsKey(key))
                            {
                                this.Settings.Add(key, settings2[key]);
                            }
                        }
                    }
                }
            }

            // Zuletzt noch die AppSettings aus einer möglichen app.settings.json verarbeiten
            /*
             *  {
             *      "Butzemann": "Biber",
             *      "Eberhard": "Schwein",
             *      "Pierre": "Robbe"
             *  }
             */
            configPath = "appsettings.json";
            if (File.Exists(configPath))
            {
                var configuration = new ConfigurationBuilder().AddJsonFile(configPath);
                var config = configuration.Build();
                IEnumerable<IConfigurationSection> children = config.GetChildren();
                foreach (var child in children)
                {
                    if (!this.Settings.ContainsKey(child.Key))
                    {
                        this.Settings.Add(child.Key, child.Value);
                    }
                }
            }

        }

        #endregion public members

    }
}
