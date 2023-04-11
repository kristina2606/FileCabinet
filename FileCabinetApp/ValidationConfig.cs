using Newtonsoft.Json;
using System;

namespace FileCabinetApp
{
    public class ValidationConfig
    {
        [JsonProperty("default")]
        public ValidatorStructure Default { get; set; }

        [JsonProperty("custom")]
        public ValidatorStructure Custom { get; set; }
    }

    public class ValidatorStructure
    {
        [JsonProperty("firstName")]
        public Name FirstName { get; set; }

        [JsonProperty("lastName")]
        public Name LastName { get; set; }

        [JsonProperty("dateOfBirth")]
        public DateOfBirth DateOfBirth { get; set; }

        [JsonProperty("gender")]
        public Gender Gender { get; set; }

        [JsonProperty("height")]
        public Eight Height { get; set; }

        [JsonProperty("weight")]
        public Eight Weight { get; set; }
    }

    public class DateOfBirth
    {
        [JsonProperty("from")]
        public DateTime From { get; set; }

        [JsonProperty("to")]
        public DateTime To { get; set; }
    }

    public class Name
    {
        [JsonProperty("minLenght")]
        public int MinLenght { get; set; }

        [JsonProperty("maxLenght")]
        public int MaxLenght { get; set; }
    }

    public class Gender
    {
        [JsonProperty("requiredFirstValue")]
        public char RequiredFirstValue { get; set; }

        [JsonProperty("requiredSecondValue")]
        public char RequiredSecondValue { get; set; }

        [JsonProperty("stringComparison")]
        public string StringComparison { get; set; }
    }

    public class Eight
    {
        [JsonProperty("minValue")]
        public int MinValue { get; set; }

        [JsonProperty("maxValue")]
        public int MaxValue { get; set; }
    }
}