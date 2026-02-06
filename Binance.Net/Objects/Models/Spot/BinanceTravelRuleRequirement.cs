namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Travel rule requirement
    /// </summary>
    public record BinanceTravelRuleRequirement
    {
        /// <summary>
        /// Country code of the questionnaire required. NIL if not required.    
        /// </summary>
        [JsonPropertyName("questionnaireCountryCode")]
        public string QuestionnaireCountryCode { get; set; } = string.Empty;
    }
}
