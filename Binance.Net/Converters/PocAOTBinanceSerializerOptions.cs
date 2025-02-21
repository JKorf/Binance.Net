//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Text.Json;

//namespace Binance.Net.Converters
//{
//    static class PocAOTBinanceSerializerOptions
//    {
//        public static JsonSerializerOptions WithConverters { get; } = new JsonSerializerOptions
//        {
//            NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.AllowNamedFloatingPointLiterals,
//            PropertyNameCaseInsensitive = false,
//            Converters =
//                    {
//                        new DateTimeConverter(),
////                        new EnumConverter(),
//                        new BoolConverter(),
//                        new DecimalConverter(),
//                        new IntConverter(),
//                        new LongConverter()
//                    },
//            TypeInfoResolver = PocAOTBinanceSourceGenerationContext.Default,
//        };
//    }
//}
