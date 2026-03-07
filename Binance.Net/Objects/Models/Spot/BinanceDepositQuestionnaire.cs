using System.Text.Json;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Deposit questionnaire base
    /// </summary>
    public abstract record BinanceDepositQuestionnaire
    {
        /// <summary>
        /// Create a questionnaire for a deposit for Japan
        /// </summary>
        [JsonIgnore]
        public static BinanceDepositQuestionnaireJapan Japan => new BinanceDepositQuestionnaireJapan();
        /// <summary>
        /// Create a questionnaire for a deposit for Kazakhstan
        /// </summary>
        [JsonIgnore]
        public static BinanceDepositQuestionnaireKazakhstan Kazakhstan => new BinanceDepositQuestionnaireKazakhstan();
        /// <summary>
        /// Create a questionnaire for a deposit for Bahrain
        /// </summary>
        [JsonIgnore]
        public static BinanceDepositQuestionnaireBahrain Bahrain => new BinanceDepositQuestionnaireBahrain();
        /// <summary>
        /// Create a questionnaire for a deposit for UAE
        /// </summary>
        [JsonIgnore]
        public static BinanceDepositQuestionnaireUae UAE => new BinanceDepositQuestionnaireUae();
        /// <summary>
        /// Create a questionnaire for a deposit for India
        /// </summary>
        [JsonIgnore]
        public static BinanceDepositQuestionnaireIndia India => new BinanceDepositQuestionnaireIndia();
        /// <summary>
        /// Create a questionnaire for a deposit for EU
        /// </summary>
        [JsonIgnore]
        public static BinanceDepositQuestionnaireEu Eu => new BinanceDepositQuestionnaireEu();
        /// <summary>
        /// Create a questionnaire for a deposit for South Africa
        /// </summary>
        [JsonIgnore]
        public static BinanceDepositQuestionnaireSouthAfrica SouthAfrica => new BinanceDepositQuestionnaireSouthAfrica();

#pragma warning disable IL2026 // Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code
#pragma warning disable IL3050 // Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.
        internal virtual string Serialize() => JsonSerializer.Serialize(this, SerializerOptions.WithConverters(BinanceExchange._serializerContext).GetTypeInfo(GetType()))!;
#pragma warning restore IL3050 // Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.
#pragma warning restore IL2026 // Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code
    }

    /// <summary>
    /// Japan Deposit questionnaire
    /// </summary>
    public record BinanceDepositQuestionnaireJapan : BinanceDepositQuestionnaire
    {
        /// <summary>
        /// ["<c>depositOriginator</c>"] 0:Myself, 1:Not Myself
        /// </summary>
        [JsonPropertyName("depositOriginator")]
        public int DepositOriginator { get; set; }
        /// <summary>
        /// ["<c>bnfType</c>"] 0:Individual, 1:Corporate/Entity
        /// </summary>
        [JsonPropertyName("bnfType")]
        public int BnfType { get; set; }
        /// <summary>
        /// ["<c>country</c>"] Originator country, required when DepositOriginator is 1
        /// </summary>
        [JsonPropertyName("country")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Country { get; set; }
        /// <summary>
        /// ["<c>region</c>"] Originator region, required when country is cn(China) or ua(Ukraine). > 1. If country is cn(China), region must be notNortheasternProvinces (Jilin, Liaoning and Heilongjiang) or other. 2. If country is ua(Ukraine), region should not be crimea, donetsk or luhansk，should be other.
        /// </summary>
        [JsonPropertyName("region")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Region { get; set; }
        /// <summary>
        /// ["<c>city</c>"] Originator city, required when DepositOriginator is 1
        /// </summary>
        [JsonPropertyName("city")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? City { get; set; }
        /// <summary>
        /// ["<c>kanjiName</c>"] Kanji name, required when DepositOriginator is 1
        /// </summary>
        [JsonPropertyName("kanjiName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? KanjiName { get; set; }
        /// <summary>
        /// ["<c>kanaName</c>"] Kana name, required when DepositOriginator is 1
        /// </summary>
        [JsonPropertyName("kanaName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? KanaName { get; set; }
        /// <summary>
        /// ["<c>latinName</c>"] Latin name, required when DepositOriginator is 1
        /// </summary>
        [JsonPropertyName("latinName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? LatinName { get; set; }
        /// <summary>
        /// ["<c>vaspName</c>"] VASP name
        /// </summary>
        [JsonPropertyName("vaspName")]
        public string VaspName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>isAttested</c>"] Is attested
        /// </summary>
        [JsonPropertyName("isAttested")]
        public bool IsAttested { get; set; }
    }

    /// <summary>
    /// Kazakhstan Deposit questionnaire
    /// </summary>
    public record BinanceDepositQuestionnaireKazakhstan : BinanceDepositQuestionnaire
    {
        /// <summary>
        /// ["<c>depositOriginator</c>"] 0:Myself, 1:Not Myself
        /// </summary>
        [JsonPropertyName("depositOriginator")]
        public int DepositOriginator { get; set; }
        /// <summary>
        /// ["<c>country</c>"] Originator country
        /// </summary>
        [JsonPropertyName("country")]
        public string Country { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>city</c>"] Originator city
        /// </summary>
        [JsonPropertyName("city")]
        public string City { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>txnPurpose</c>"] Value: service, goods, p2p, charity, others
        /// </summary>
        [JsonPropertyName("txnPurpose")]
        public string TxnPurpose { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>txnPurposeOthers</c>"] Required when TxnPurpose is `others`
        /// </summary>
        [JsonPropertyName("txnPurposeOthers")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? TxnPurposeOthers { get; set; }
    }

    /// <summary>
    /// Bahrain Deposit questionnaire
    /// </summary>
    public record BinanceDepositQuestionnaireBahrain : BinanceDepositQuestionnaire
    {
        /// <summary>
        /// ["<c>depositOriginator</c>"] 1:Myself, 2:Not Myself
        /// </summary>
        [JsonPropertyName("depositOriginator")]
        public int DepositOriginator { get; set; }
        /// <summary>
        /// ["<c>orgType</c>"] 0:Individual, 1:Corporate/Entity, required when DepositOriginator is 2
        /// </summary>
        [JsonPropertyName("orgType")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? OrgType { get; set; }
        /// <summary>
        /// ["<c>orgFirstName</c>"] Originator's first name, required when DepositOriginator is 2
        /// </summary>
        [JsonPropertyName("orgFirstName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? OrgFirstName { get; set; }
        /// <summary>
        /// ["<c>orgLastName</c>"] Originator's last name, required when DepositOriginator is 2
        /// </summary>
        [JsonPropertyName("orgLastName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? OrgLastName { get; set; }
        /// <summary>
        /// ["<c>country</c>"] Originator's country, required when DepositOriginator is 2
        /// </summary>
        [JsonPropertyName("country")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Country { get; set; }
        /// <summary>
        /// ["<c>city</c>"] Originator's city, required when DepositOriginator is 2
        /// </summary>
        [JsonPropertyName("city")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? City { get; set; }
        /// <summary>
        /// ["<c>receiveFrom</c>"] 1:Private Wallet, 2:Another VASP
        /// </summary>
        [JsonPropertyName("receiveFrom")]
        public int ReceiveFrom { get; set; }
        /// <summary>
        /// ["<c>vasp</c>"] VASP, received when ReceiveFrom is 2
        /// </summary>
        [JsonPropertyName("vasp")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Vasp { get; set; }
        /// <summary>
        /// ["<c>vaspName</c>"] VASP name, received when Vasp is `others`
        /// </summary>
        [JsonPropertyName("vaspName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? VaspName { get; set; }
    }

    /// <summary>
    /// UAE Deposit questionnaire
    /// </summary>
    public record BinanceDepositQuestionnaireUae : BinanceDepositQuestionnaire
    {
        /// <summary>
        /// ["<c>depositOriginator</c>"] 1:Myself, 2:Not Myself
        /// </summary>
        [JsonPropertyName("depositOriginator")]
        public int DepositOriginator { get; set; }
        /// <summary>
        /// ["<c>orgType</c>"] 0:Individual, 1:Corporate/Entity, required when DepositOriginator is 2
        /// </summary>
        [JsonPropertyName("orgType")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? OrgType { get; set; }
        /// <summary>
        /// ["<c>orgName</c>"] Originator's name, required when DepositOriginator is 2
        /// </summary>
        [JsonPropertyName("orgName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? OrgName { get; set; }
        /// <summary>
        /// ["<c>country</c>"] Originator's country, required when DepositOriginator is 2
        /// </summary>
        [JsonPropertyName("country")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Country { get; set; }
        /// <summary>
        /// ["<c>city</c>"] Originator's city, required when DepositOriginator is 2
        /// </summary>
        [JsonPropertyName("city")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? City { get; set; }
        /// <summary>
        /// ["<c>receiveFrom</c>"] 1:Private Wallet, 2:Another VASP
        /// </summary>
        [JsonPropertyName("receiveFrom")]
        public int ReceiveFrom { get; set; }
        /// <summary>
        /// ["<c>vasp</c>"] VASP, received when ReceiveFrom is 2
        /// </summary>
        [JsonPropertyName("vasp")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Vasp { get; set; }
        /// <summary>
        /// ["<c>vaspName</c>"] VASP name, received when Vasp is `others`
        /// </summary>
        [JsonPropertyName("vaspName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? VaspName { get; set; }
    }

    /// <summary>
    /// India Deposit questionnaire
    /// </summary>
    public record BinanceDepositQuestionnaireIndia : BinanceDepositQuestionnaire
    {
        /// <summary>
        /// ["<c>depositOriginator</c>"] 1:Myself, 2:Not Myself
        /// </summary>
        [JsonPropertyName("depositOriginator")]
        public int DepositOriginator { get; set; }
        /// <summary>
        /// ["<c>orgType</c>"] 0:Individual, 1:Corporate/Entity, required when DepositOriginator is 2
        /// </summary>
        [JsonPropertyName("orgType")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? OrgType { get; set; }
        /// <summary>
        /// ["<c>orgName</c>"] Originator's name, required when DepositOriginator is 2
        /// </summary>
        [JsonPropertyName("orgName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? OrgName { get; set; }
        /// <summary>
        /// ["<c>pan</c>"] Permanent Account Number (PAN) or National ID Number, required when DepositOriginator is 2
        /// </summary>
        [JsonPropertyName("pan")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Pan { get; set; }
        /// <summary>
        /// ["<c>country</c>"] Originator's country, required when DepositOriginator is 2
        /// </summary>
        [JsonPropertyName("country")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Country { get; set; }
        /// <summary>
        /// ["<c>state</c>"] Originator's state, required when DepositOriginator is 2
        /// </summary>
        [JsonPropertyName("state")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? State { get; set; }
        /// <summary>
        /// ["<c>city</c>"] Originator's city, required when DepositOriginator is 2
        /// </summary>
        [JsonPropertyName("city")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? City { get; set; }
        /// <summary>
        /// ["<c>pinCode</c>"] Pin code, required when DepositOriginator is 2
        /// </summary>
        [JsonPropertyName("pinCode")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? PinCode { get; set; }
        /// <summary>
        /// ["<c>address</c>"] Address, required when DepositOriginator is 2
        /// </summary>
        [JsonPropertyName("address")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Address { get; set; }
        /// <summary>
        /// ["<c>receiveFrom</c>"] 1:Private Wallet, 2:Another VASP
        /// </summary>
        [JsonPropertyName("receiveFrom")]
        public int ReceiveFrom { get; set; }
        /// <summary>
        /// ["<c>vasp</c>"] VASP, received when ReceiveFrom is 2
        /// </summary>
        [JsonPropertyName("vasp")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Vasp { get; set; }
        /// <summary>
        /// ["<c>vaspName</c>"] VASP name, received when Vasp is `others`
        /// </summary>
        [JsonPropertyName("vaspName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? VaspName { get; set; }
    }

    /// <summary>
    /// EU Deposit questionnaire
    /// </summary>
    public record BinanceDepositQuestionnaireEu : BinanceDepositQuestionnaire
    {
        /// <summary>
        /// ["<c>depositOriginator</c>"] 1:Myself, 2:Not Myself
        /// </summary>
        [JsonPropertyName("depositOriginator")]
        public int DepositOriginator { get; set; }
        /// <summary>
        /// ["<c>orgType</c>"] 0:Individual, 1:Corporate/Entity, required when DepositOriginator is 2
        /// </summary>
        [JsonPropertyName("orgType")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? OrgType { get; set; }
        /// <summary>
        /// ["<c>orgFirstName</c>"] Originator's first name, required when OrgType is 0
        /// </summary>
        [JsonPropertyName("orgFirstName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? OrgFirstName { get; set; }
        /// <summary>
        /// ["<c>orgLastName</c>"] Originator's last name, required when OrgType is 0
        /// </summary>
        [JsonPropertyName("orgLastName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? OrgLastName { get; set; }
        /// <summary>
        /// ["<c>country</c>"] Originator's country, required when OrgType is 0
        /// </summary>
        [JsonPropertyName("country")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Country { get; set; }
        /// <summary>
        /// ["<c>corpName</c>"] Originator's (corporation) Name, required when OrgType is 1
        /// </summary>
        [JsonPropertyName("corpName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? CorpName { get; set; }
        /// <summary>
        /// ["<c>corpCountry</c>"] Originator's (corporation) nationality code, required when OrgType is 1
        /// </summary>
        [JsonPropertyName("corpCountry")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? CorpCountry { get; set; }
        /// <summary>
        /// ["<c>receiveFrom</c>"] 1:Private Wallet, 2:Another VASP
        /// </summary>
        [JsonPropertyName("receiveFrom")]
        public int ReceiveFrom { get; set; }
        /// <summary>
        /// ["<c>vasp</c>"] VASP, received when ReceiveFrom is 2
        /// </summary>
        [JsonPropertyName("vasp")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Vasp { get; set; }
        /// <summary>
        /// ["<c>vaspName</c>"] VASP name, received when Vasp is `others`
        /// </summary>
        [JsonPropertyName("vaspName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? VaspName { get; set; }
        /// <summary>
        /// ["<c>declaration</c>"] Declaration
        /// </summary>
        [JsonPropertyName("declaration")]
        public bool Declaration { get; set; }
    }

    /// <summary>
    /// South Africa Deposit questionnaire
    /// </summary>
    public record BinanceDepositQuestionnaireSouthAfrica : BinanceDepositQuestionnaire
    {
        /// <summary>
        /// ["<c>depositOriginator</c>"] 1:Myself, 2:Not Myself
        /// </summary>
        [JsonPropertyName("depositOriginator")]
        public int DepositOriginator { get; set; }
        /// <summary>
        /// ["<c>orgType</c>"] 0:Individual, 1:Corporate/Entity, required when DepositOriginator is 2
        /// </summary>
        [JsonPropertyName("orgType")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? OrgType { get; set; }
        /// <summary>
        /// ["<c>orgName</c>"] Originator's name, required when OrgType is 0
        /// </summary>
        [JsonPropertyName("orgName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? OrgName { get; set; }
        /// <summary>
        /// ["<c>country</c>"] Originator's country, required when OrgType is 0
        /// </summary>
        [JsonPropertyName("country")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Country { get; set; }
        /// <summary>
        /// ["<c>corpName</c>"] Originator's (corporation) Name, required when OrgType is 1
        /// </summary>
        [JsonPropertyName("corpName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? CorpName { get; set; }
        /// <summary>
        /// ["<c>corpCountry</c>"] Originator's (corporation) nationality code, required when OrgType is 1
        /// </summary>
        [JsonPropertyName("corpCountry")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? CorpCountry { get; set; }
        /// <summary>
        /// ["<c>receiveFrom</c>"] 1:Private Wallet, 2:Another VASP
        /// </summary>
        [JsonPropertyName("receiveFrom")]
        public int ReceiveFrom { get; set; }
        /// <summary>
        /// ["<c>vasp</c>"] VASP, received when ReceiveFrom is 2
        /// </summary>
        [JsonPropertyName("vasp")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Vasp { get; set; }
        /// <summary>
        /// ["<c>vaspName</c>"] VASP name, received when Vasp is `others`
        /// </summary>
        [JsonPropertyName("vaspName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? VaspName { get; set; }
        /// <summary>
        /// ["<c>declaration</c>"] Declaration
        /// </summary>
        [JsonPropertyName("declaration")]
        public bool Declaration { get; set; }
    }
}

