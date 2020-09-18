using System;
using System.Configuration;
using NetEti.Globals;
using System.Collections.Generic;

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

        #region IGetStringValue Members

        /// <summary>
        /// Liefert genau einen Wert zu einem Key. Wenn es keinen Wert zu dem
        /// Key gibt, wird defaultValue zurückgegeben.
        /// </summary>
        /// <param name="key">Der Zugriffsschlüssel (string)</param>
        /// <param name="defaultValue">Das default-Ergebnis (string)</param>
        /// <returns>Der Ergebnis-String</returns>
        public string GetStringValue(string key, string defaultValue)
        {
            string rtn = null;
            if (this._settingKeys.Contains(key))
            {
                try
                {
                    rtn = this._appSettingsReader.GetValue(key, typeof(string)).ToString();
                }
                catch (InvalidOperationException)
                {
                    rtn = defaultValue;
                }
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
        public string[] GetStringValues(string key, string[] defaultValues)
        {
            string rtn = GetStringValue(key, null);
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
            this._appSettingsReader = new AppSettingsReader();
            this._settingKeys = new List<string>();
            foreach (var key in ConfigurationManager.AppSettings.Keys)
            {
                this._settingKeys.Add(key.ToString());
            }
        }

        #endregion public members

        #region private members

        private System.Configuration.AppSettingsReader _appSettingsReader;

        private List<string> _settingKeys;

        #endregion private members

    }
}
