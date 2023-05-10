﻿using System;
using Newtonsoft.Json;

namespace FileCabinetApp.Serializers.Json
{
    /// <summary>
    ///  Represents the date of birth validation rule loaded from a JSON configuration file.
    /// </summary>
    public class DateOfBirthValidationRule
    {
        /// <summary>
        /// Gets or sets the minimum date of birth for validation.
        /// </summary>
        /// <value>The minimum date of birth for validation.</value>
        [JsonProperty("from")]
        public DateTime From { get; set; }

        /// <summary>
        /// Gets or sets the maximum date of birth for validation.
        /// </summary>
        /// <value>The maximum date of birth for validation.</value>
        [JsonProperty("to")]
        public DateTime To { get; set; }
    }
}
