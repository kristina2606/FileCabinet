using System;
using Newtonsoft.Json;

namespace FileCabinetApp
{
    /// <summary>
    /// Create validation from .json configuration file.
    /// </summary>
    public class ValidationConfig
    {
        /// <summary>
        /// Gets or sets default validation parametrs.
        /// </summary>
        /// <value>
        /// Default validation parametrs.
        /// </value>
        [JsonProperty("default")]
        public ValidationConfigValidatorStructure Default { get; set; }

        /// <summary>
        /// Gets or sets custom validation parametrs.
        /// </summary>
        /// <value>
        /// Custom validation parametrs.
        /// </value>
        [JsonProperty("custom")]
        public ValidationConfigValidatorStructure Custom { get; set; }
    }
}