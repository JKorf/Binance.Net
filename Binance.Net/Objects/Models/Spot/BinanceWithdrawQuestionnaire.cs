using System.Text.Json;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Withdrawal questionnaire base
    /// </summary>
    public abstract record BinanceWithdrawQuestionnaire
    {
        /// <summary>
        /// Create a questionnaire for a withdrawal for Japan
        /// </summary>
        [JsonIgnore]
        public static BinanceWithdrawQuestionnaireJapan Japan => new BinanceWithdrawQuestionnaireJapan();
        /// <summary>
        /// Create a questionnaire for a withdrawal for Kazakhstan
        /// </summary>
        [JsonIgnore]
        public static BinanceWithdrawQuestionnaireKazakhstan Kazakhstan => new BinanceWithdrawQuestionnaireKazakhstan();
        /// <summary>
        /// Create a questionnaire for a withdrawal for New Zealand
        /// </summary>
        [JsonIgnore]
        public static BinanceWithdrawQuestionnaireNewZealand NewZealand => new BinanceWithdrawQuestionnaireNewZealand();
        /// <summary>
        /// Create a questionnaire for a withdrawal for Bahrain
        /// </summary>
        [JsonIgnore]
        public static BinanceWithdrawQuestionnaireBahrain Bahrain => new BinanceWithdrawQuestionnaireBahrain();
        /// <summary>
        /// Create a questionnaire for a withdrawal for UAE
        /// </summary>
        [JsonIgnore]
        public static BinanceWithdrawQuestionnaireUae UAE => new BinanceWithdrawQuestionnaireUae();
        /// <summary>
        /// Create a questionnaire for a withdrawal for India
        /// </summary>
        [JsonIgnore]
        public static BinanceWithdrawQuestionnaireIndia India => new BinanceWithdrawQuestionnaireIndia();
        /// <summary>
        /// Create a questionnaire for a withdrawal for EU
        /// </summary>
        [JsonIgnore]
        public static BinanceWithdrawQuestionnaireEu Eu => new BinanceWithdrawQuestionnaireEu();
        /// <summary>
        /// Create a questionnaire for a withdrawal for South Africa
        /// </summary>
        [JsonIgnore]
        public static BinanceWithdrawQuestionnaireSouthAfrica SouthAfrica => new BinanceWithdrawQuestionnaireSouthAfrica();

#pragma warning disable IL2026 // Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code
#pragma warning disable IL3050 // Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.
        internal virtual string Serialize() => JsonSerializer.Serialize(this, SerializerOptions.WithConverters(BinanceExchange._serializerContext).GetTypeInfo(GetType()))!;
#pragma warning restore IL3050 // Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.
#pragma warning restore IL2026 // Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code
    }

    /// <summary>
    /// Japan withdraw questionnaire
    /// </summary>
    public record BinanceWithdrawQuestionnaireJapan : BinanceWithdrawQuestionnaire
    {
        /// <summary>
        /// 1:Send to myself, 2:Send to another beneficiary.
        /// </summary>
        [JsonPropertyName("isAddressOwner")]
        public int IsAddressOwner { get; set; }
        /// <summary>
        /// 0:Individual, 1:Corporate/Entity
        /// </summary>
        [JsonPropertyName("bnfType")]
        public int BnfType { get; set; }
        /// <summary>
        /// Kanji name, required when isAddressOwner = 2
        /// </summary>
        [JsonPropertyName("kanjiName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? KanjiName { get; set; }
        /// <summary>
        /// Kana name, required when isAddressOwner = 2
        /// </summary>
        [JsonPropertyName("kanaName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? KanaName { get; set; }
        /// <summary>
        /// Latin name, required when isAddressOwner = 2
        /// </summary>
        [JsonPropertyName("latinName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? LatinName { get; set; }
        /// <summary>
        /// Beneficiary country code, ISO 2 digit, lower case.
        /// </summary>
        [JsonPropertyName("country")]
        public string Country { get; set; } = string.Empty;
        /// <summary>
        /// City
        /// </summary>
        [JsonPropertyName("city")]
        public string City { get; set; } = string.Empty;
        /// <summary>
        /// 1:Crypto-asset services provider, 2:Unhosted Wallet
        /// </summary>
        [JsonPropertyName("sendTo")]
        public int SendTo { get; set; }
        /// <summary>
        /// Vasp CODE of the beneficiary, required when SendTo is 1
        /// </summary>
        [JsonPropertyName("vasp")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Vasp { get; set; }
        /// <summary>
        /// VASP country code, ISO 2 digit, lower case, required when SendTo is 1
        /// </summary>
        [JsonPropertyName("vaspCountry")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? VaspCountry { get; set; }
        /// <summary>
        /// VASP region. required when VaspCountry is cn(China) or ua(Ukraine). > 1. If VaspCountry is cn(China), VaspRegion must be notNortheasternProvinces (Jilin, Liaoning and Heilongjiang) or other. 2. If VaspCountry is ua(Ukraine), VaspRegion should not be crimea, donetsk or luhansk, should be other.
        /// </summary>
        [JsonPropertyName("vaspRegion")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? VaspRegion { get; set; }
        /// <summary>
        /// 1:Purchase of goods within Japan, 2:Inheritance, gift or living expenses, 3:Cross border trade, 4:Investment, 5:Use of services provided by the beneficiary VASP, 6:Loan repayment, 7:Gifts and Donations
        /// </summary>
        [JsonPropertyName("txnPurpose")]
        public int TxnPurpose { get; set; }
        /// <summary>
        /// Is attested
        /// </summary>
        [JsonPropertyName("isAttested")]
        public bool IsAttested { get; set; }
    }

    /// <summary>
    /// Kazakhstan withdraw questionnaire
    /// </summary>
    public record BinanceWithdrawQuestionnaireKazakhstan : BinanceWithdrawQuestionnaire
    {
        /// <summary>
        /// Is address owner
        /// </summary>
        [JsonPropertyName("isAddressOwner")]
        public bool IsAddressOwner { get; set; }
        /// <summary>
        /// 0:Individual, 1:Corporate/Entity, required if IsAddressOwner = false
        /// </summary>
        [JsonPropertyName("bnfType")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? BnfType { get; set; }
        /// <summary>
        /// Beneficiary name
        /// </summary>
        [JsonPropertyName("beneficiaryName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? BeneficiaryName { get; set; }
        /// <summary>
        /// Beneficiary country, ISO 2 digit, lower case.
        /// </summary>
        [JsonPropertyName("beneficiaryCountry")]
        public string BeneficiaryCountry { get; set; } = string.Empty;
        /// <summary>
        /// Beneficiary city
        /// </summary>
        [JsonPropertyName("beneficiaryCity")]
        public string BeneficiaryCity { get; set; } = string.Empty;
        /// <summary>
        /// Value: service, goods, p2p, charity, others
        /// </summary>
        [JsonPropertyName("txnPurpose")]
        public string TxnPurpose { get; set; } = string.Empty;
        /// <summary>
        /// Required info when TxnPurpose is `others`
        /// </summary>
        [JsonPropertyName("txnPurposeOthers")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? TxnPurposeOthers { get; set; }
        /// <summary>
        /// 2:Exchange, 1:Unhosted Wallet
        /// </summary>
        [JsonPropertyName("sendTo")]
        public int SendTo { get; set; }
        /// <summary>
        /// Vasp CODE of the beneficiary, required when SendTo is 2
        /// </summary>
        [JsonPropertyName("vasp")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Vasp { get; set; }
        /// <summary>
        /// VASP name, required when Vasp is `others`
        /// </summary>
        [JsonPropertyName("vaspName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? VaspName { get; set; }
        /// <summary>
        /// Is attested
        /// </summary>
        [JsonPropertyName("isAttested")]
        public bool IsAttested { get; set; }
    }

    /// <summary>
    /// New Zealand withdraw questionnaire
    /// </summary>
    public record BinanceWithdrawQuestionnaireNewZealand : BinanceWithdrawQuestionnaire
    {
        /// <summary>
        /// 1:Send to myself, 2:Send to another beneficiary.
        /// </summary>
        [JsonPropertyName("isAddressOwner")]
        public int IsAddressOwner { get; set; }
        /// <summary>
        /// 0:Individual, 1:Corporate/Entity, required if IsAddressOwner = 2
        /// </summary>
        [JsonPropertyName("bnfType")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? BnfType { get; set; }
        /// <summary>
        /// Beneficiary name, required when bnfType is 0
        /// </summary>
        [JsonPropertyName("bnfName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? BeneficiaryName { get; set; }
        /// <summary>
        /// Beneficiary country code, ISO 2 digit, lower case. Required when bnfType is 0
        /// </summary>
        [JsonPropertyName("country")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Country { get; set; }
        /// <summary>
        /// Beneficiary corporation name. Required when bnfType is 1
        /// </summary>
        [JsonPropertyName("bnfCorpName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? BnfCorpName { get; set; }
        /// <summary>
        /// Beneficiary corporation country code, ISO 2 digit, lower case. Required when bnfType is 1
        /// </summary>
        [JsonPropertyName("bnfCorpCountry")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? BnfCorpCountry { get; set; }
        /// <summary>
        /// 1:Private Wallet, 2:Another VASP
        /// </summary>
        [JsonPropertyName("sendTo")]
        public int SendTo { get; set; }
        /// <summary>
        /// Vasp CODE of the beneficiary, required when SendTo is 2
        /// </summary>
        [JsonPropertyName("vasp")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Vasp { get; set; }
        /// <summary>
        /// VASP name, required when Vasp is `others`
        /// </summary>
        [JsonPropertyName("vaspName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? VaspName { get; set; }
        /// <summary>
        /// Declaration confirmation
        /// </summary>
        [JsonPropertyName("declaration")]
        public bool Declaration { get; set; }
    }

    /// <summary>
    /// Bahrain withdraw questionnaire
    /// </summary>
    public record BinanceWithdrawQuestionnaireBahrain : BinanceWithdrawQuestionnaire
    {
        /// <summary>
        /// 1:Send to myself, 2:Send to another beneficiary
        /// </summary>
        [JsonPropertyName("isAddressOwner")]
        public int IsAddressOwner { get; set; }
        /// <summary>
        /// 0:Individual, 1:Corporate/Entity, required if IsAddressOwner = 2
        /// </summary>
        [JsonPropertyName("bnfType")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? BnfType { get; set; }
        /// <summary>
        /// Beneficiary first name, required when IsAddressOwner is 2
        /// </summary>
        [JsonPropertyName("bnfFirstName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? BeneficiaryFirstName { get; set; }
        /// <summary>
        /// Beneficiary last name, required when IsAddressOwner is 2
        /// </summary>
        [JsonPropertyName("bnfLastName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? BeneficiaryLastName { get; set; }
        /// <summary>
        /// Beneficiary country code, ISO 2 digit, lower case. Required when IsAddressOwner is 2
        /// </summary>
        [JsonPropertyName("country")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Country { get; set; }
        /// <summary>
        /// City. Required when IsAddressOwner is 2
        /// </summary>
        [JsonPropertyName("city")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? City { get; set; }
        /// <summary>
        /// 1:Private Wallet, 2:Another VASP
        /// </summary>
        [JsonPropertyName("sendTo")]
        public int SendTo { get; set; }
        /// <summary>
        /// Vasp CODE of the beneficiary, required when SendTo is 2
        /// </summary>
        [JsonPropertyName("vasp")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Vasp { get; set; }
        /// <summary>
        /// VASP name, required when Vasp is `others`
        /// </summary>
        [JsonPropertyName("vaspName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? VaspName { get; set; }
    }

    /// <summary>
    /// UAE withdraw questionnaire
    /// </summary>
    public record BinanceWithdrawQuestionnaireUae : BinanceWithdrawQuestionnaire
    {
        /// <summary>
        /// 1:Send to myself, 2:Send to another beneficiary
        /// </summary>
        [JsonPropertyName("isAddressOwner")]
        public int IsAddressOwner { get; set; }
        /// <summary>
        /// 0:Individual, 1:Corporate/Entity, required if IsAddressOwner = 2
        /// </summary>
        [JsonPropertyName("bnfType")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? BnfType { get; set; }
        /// <summary>
        /// Beneficiary name, required when IsAddressOwner is 2
        /// </summary>
        [JsonPropertyName("bnfName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? BnfName { get; set; }        
        /// <summary>
        /// Beneficiary country code, ISO 2 digit, lower case. Required when IsAddressOwner is 2
        /// </summary>
        [JsonPropertyName("country")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Country { get; set; }
        /// <summary>
        /// City. Required when IsAddressOwner is 2
        /// </summary>
        [JsonPropertyName("city")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? City { get; set; }
        /// <summary>
        /// 1:Private Wallet, 2:Another VASP
        /// </summary>
        [JsonPropertyName("sendTo")]
        public int SendTo { get; set; }
        /// <summary>
        /// Vasp CODE of the beneficiary, required when SendTo is 2
        /// </summary>
        [JsonPropertyName("vasp")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Vasp { get; set; }
        /// <summary>
        /// VASP name, required when Vasp is `others`
        /// </summary>
        [JsonPropertyName("vaspName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? VaspName { get; set; }
    }

    /// <summary>
    /// India withdraw questionnaire
    /// </summary>
    public record BinanceWithdrawQuestionnaireIndia : BinanceWithdrawQuestionnaire
    {
        /// <summary>
        /// 1:Send to myself, 2:Send to another beneficiary
        /// </summary>
        [JsonPropertyName("isAddressOwner")]
        public int IsAddressOwner { get; set; }
        /// <summary>
        /// 0:Individual, 1:Corporate/Entity, required if IsAddressOwner = 2
        /// </summary>
        [JsonPropertyName("bnfType")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? BnfType { get; set; }
        /// <summary>
        /// Beneficiary name, required when IsAddressOwner is 2
        /// </summary>
        [JsonPropertyName("bnfName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? BnfName { get; set; }
        /// <summary>
        /// Beneficiary country code, ISO 2 digit, lower case. Required when IsAddressOwner is 2
        /// </summary>
        [JsonPropertyName("country")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Country { get; set; }
        /// <summary>
        /// City
        /// </summary>
        [JsonPropertyName("city")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? City { get; set; }
        /// <summary>
        /// 1:Private Wallet, 2:Another VASP
        /// </summary>
        [JsonPropertyName("sendTo")]
        public int SendTo { get; set; }
        /// <summary>
        /// Vasp CODE of the beneficiary, required when SendTo is 2
        /// </summary>
        [JsonPropertyName("vasp")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Vasp { get; set; }
        /// <summary>
        /// VASP name, required when Vasp is `others`
        /// </summary>
        [JsonPropertyName("vaspName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? VaspName { get; set; }
    }

    /// <summary>
    /// EU withdraw questionnaire
    /// </summary>
    public record BinanceWithdrawQuestionnaireEu : BinanceWithdrawQuestionnaire
    {
        /// <summary>
        /// 1:Send to myself, 2:Send to another beneficiary
        /// </summary>
        [JsonPropertyName("isAddressOwner")]
        public int IsAddressOwner { get; set; }
        /// <summary>
        /// 0:Individual, 1:Corporate/Entity, required if IsAddressOwner = 2
        /// </summary>
        [JsonPropertyName("bnfType")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? BnfType { get; set; }
        /// <summary>
        /// Beneficiary first name, required when BnfType = 0
        /// </summary>
        [JsonPropertyName("bnfFirstName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? BeneficiaryFirstName { get; set; }
        /// <summary>
        /// Beneficiary last name, required when BnfType = 0
        /// </summary>
        [JsonPropertyName("bnfLastName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? BeneficiaryLastName { get; set; }
        /// <summary>
        /// Beneficiary country code, ISO 2 digit, lower case. Required when BnfType = 0
        /// </summary>
        [JsonPropertyName("country")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Country { get; set; }
        /// <summary>
        /// Beneficiary corporation name. Required when bnfType is 1
        /// </summary>
        [JsonPropertyName("bnfCorpName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? BnfCorpName { get; set; }
        /// <summary>
        /// Beneficiary corporation country code, ISO 2 digit, lower case. Required when bnfType is 1
        /// </summary>
        [JsonPropertyName("bnfCorpCountry")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? BnfCorpCountry { get; set; }
        /// <summary>
        /// 1:Private Wallet, 2:Another VASP
        /// </summary>
        [JsonPropertyName("sendTo")]
        public int SendTo { get; set; }
        /// <summary>
        /// Vasp CODE of the beneficiary, required when SendTo is 2
        /// </summary>
        [JsonPropertyName("vasp")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Vasp { get; set; }
        /// <summary>
        /// VASP name, required when Vasp is `others`
        /// </summary>
        [JsonPropertyName("vaspName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? VaspName { get; set; }
        /// <summary>
        /// Declaration confirmation
        /// </summary>
        [JsonPropertyName("declaration")]
        public bool Declaration { get; set; }
    }

    /// <summary>
    /// South Africa withdraw questionnaire
    /// </summary>
    public record BinanceWithdrawQuestionnaireSouthAfrica : BinanceWithdrawQuestionnaire
    {
        /// <summary>
        /// 1:Send to myself, 2:Send to another beneficiary
        /// </summary>
        [JsonPropertyName("isAddressOwner")]
        public int IsAddressOwner { get; set; }
        /// <summary>
        /// 0:Individual, 1:Corporate/Entity, required if IsAddressOwner = 2
        /// </summary>
        [JsonPropertyName("bnfType")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? BnfType { get; set; }
        /// <summary>
        /// Beneficiary name, required when BnfType is 0
        /// </summary>
        [JsonPropertyName("bnfName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? BnfName { get; set; }
        /// <summary>
        /// Beneficiary country code, ISO 2 digit, lower case. Required if BnfType is 0
        /// </summary>
        [JsonPropertyName("country")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Country { get; set; }
        /// <summary>
        /// Beneficiary corporation name. Required when bnfType is 1
        /// </summary>
        [JsonPropertyName("bnfCorpName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? BnfCorpName { get; set; }
        /// <summary>
        /// Beneficiary corporation country code, ISO 2 digit, lower case. Required when bnfType is 1
        /// </summary>
        [JsonPropertyName("bnfCorpCountry")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? BnfCorpCountry { get; set; }
        /// <summary>
        /// 1:Private Wallet, 2:Another VASP
        /// </summary>
        [JsonPropertyName("sendTo")]
        public int SendTo { get; set; }
        /// <summary>
        /// Vasp CODE of the beneficiary, required when SendTo is 2
        /// </summary>
        [JsonPropertyName("vasp")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Vasp { get; set; }
        /// <summary>
        /// VASP name, required when Vasp is `others`
        /// </summary>
        [JsonPropertyName("vaspName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? VaspName { get; set; }
        /// <summary>
        /// Declaration confirmation
        /// </summary>
        [JsonPropertyName("declaration")]
        public bool Declaration { get; set; }
    }
}
