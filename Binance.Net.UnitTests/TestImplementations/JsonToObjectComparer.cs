using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Binance.Net.UnitTests.TestImplementations
{
    public class JsonToObjectComparer<T>
    {
        private Func<string, T> _clientFunc;

        public JsonToObjectComparer(Func<string, T> getClient)
        {
            _clientFunc = getClient;
        }

        public async Task ProcessSubject<K>(
           string folderPrefix,
           Func<T, K> getSubject,
           string[] parametersToSetNull = null,
           Dictionary<string, string> useNestedJsonPropertyForCompare = null,
           Dictionary<string, List<string>> ignoreProperties = null)
        {
            var methods = typeof(K).GetMethods();
            var callResultMethods = methods.Where(m => m.Name.EndsWith("Async")).ToList();
            var skippedMethods = new List<string>();

            foreach (var method in callResultMethods)
            {
                var path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
                FileStream file = null;
                try
                {
                    file = File.OpenRead(Path.Combine(path, $"JsonResponses", folderPrefix, $"{method.Name}.txt"));
                }
                catch (FileNotFoundException)
                {
                    skippedMethods.Add(method.Name);
                    continue;
                }

                var buffer = new byte[file.Length];
                await file.ReadAsync(buffer, 0, buffer.Length);
                file.Close();

                var json = Encoding.UTF8.GetString(buffer);
                var client = _clientFunc(json);

                var parameters = method.GetParameters();
                var input = new List<object>();
                foreach (var parameter in parameters)
                {
                    if (parametersToSetNull?.Contains(parameter.Name) == true)
                        input.Add(null);
                    else
                        input.Add(TestHelpers.GetTestValue(parameter.ParameterType, 1));
                }

                // act
                CallResult result = (CallResult)await TestHelpers.InvokeAsync(method, getSubject(client), input.ToArray());

                // asset
                Assert.Null(result.Error, method.Name);

                var resultData = result.GetType().GetProperty("Data", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).GetValue(result);
                var resultProperties = resultData.GetType().GetProperties().Select(p => (p, (JsonPropertyAttribute)p.GetCustomAttributes(typeof(JsonPropertyAttribute), true).SingleOrDefault()));
                var jsonObject = JToken.Parse(json);
                if (useNestedJsonPropertyForCompare?.ContainsKey(method.Name) == true)
                {
                    jsonObject = jsonObject[useNestedJsonPropertyForCompare[method.Name]];
                }

                if (resultData.GetType().GetInterfaces().Contains(typeof(IDictionary)))
                {
                    var dict = (IDictionary)resultData;
                    var jObj = (JObject)jsonObject;
                    var properties = jObj.Properties();
                    foreach (var dictProp in properties)
                    {
                        if (!dict.Contains(dictProp.Name))
                            throw new Exception($"{method}: Dictionary has no value for {dictProp.Name} while input json `{dictProp.Name}` has value {dictProp.Value}");

                        if (dictProp.Value.Type == JTokenType.Object)
                        {
                            // TODO Some additional checking for objects
                            foreach (var prop in ((JObject)dictProp.Value).Properties())
                                CheckObject(method.Name, prop, dict[dictProp.Name], ignoreProperties);
                        }
                        else
                        {
                            if (dict[dictProp.Name] == default && dictProp.Value.Type != JTokenType.Null)
                            {
                                // Property value not correct
                                throw new Exception($"{method}: Dictionary entry `{dictProp.Name}` has no value while input json has value {dictProp.Value}");
                            }
                        }
                    }
                }
                else if (jsonObject.Type == JTokenType.Array)
                {
                    var jObjs = (JArray)jsonObject;
                    var list = (IEnumerable)resultData;
                    var enumerator = list.GetEnumerator();
                    foreach (var jObj in jObjs)
                    {
                        enumerator.MoveNext();
                        if (jObj.Type == JTokenType.Object)
                        {
                            foreach (var subProp in ((JObject)jObj).Properties())
                            {
                                if (ignoreProperties?.ContainsKey(method.Name) == true && ignoreProperties[method.Name].Contains(subProp.Name))
                                    continue;
                                CheckObject(method.Name, subProp, enumerator.Current, ignoreProperties);
                            }
                        }
                        else if (jObj.Type == JTokenType.Array)
                        {
                            var resultObj = enumerator.Current;
                            var resultProps = resultObj.GetType().GetProperties().Select(p => (p, p.GetCustomAttributes(typeof(ArrayPropertyAttribute), true).Cast<ArrayPropertyAttribute>().SingleOrDefault()));
                            var arrayConverterProperty = resultObj.GetType().GetCustomAttributes(typeof(JsonConverterAttribute), true).FirstOrDefault();
                            var jsonConverter = (arrayConverterProperty as JsonConverterAttribute).ConverterType;
                            if (jsonConverter != typeof(ArrayConverter))
                                // Not array converter?
                                continue;

                            int i = 0;
                            foreach (var item in jObj.Values())
                            {
                                var arrayProp = resultProps.SingleOrDefault(p => p.Item2.Index == i).p;
                                if (arrayProp != null)
                                {
                                    CheckPropertyValue(method.Name, item, arrayProp.GetValue(resultObj), arrayProp.Name, "Array index " + i, arrayProp, ignoreProperties);
                                }
                                i++;
                            }
                        }
                        else
                        {
                            var value = enumerator.Current;
                            if (value == default && ((JValue)jObj).Type != JTokenType.Null)
                            {
                                throw new Exception($"{method}: Array has no value while input json array has value {jObj}");
                            }
                        }
                    }
                }
                else
                {
                    foreach (var item in jsonObject)
                    {
                        if (item is JProperty prop)
                        {
                            if (ignoreProperties?.ContainsKey(method.Name) == true && ignoreProperties[method.Name].Contains(prop.Name))
                                continue;

                            CheckObject(method.Name, prop, resultData, ignoreProperties);
                        }
                    }
                }

                Debug.WriteLine($"Successfully validated {method.Name}");
            }

            if (skippedMethods.Any())
                Debug.WriteLine("Skipped methods:");
            foreach(var method in skippedMethods)
                Debug.WriteLine(method);
        }

        private static void CheckObject(string method, JProperty prop, object obj, Dictionary<string, List<string>> ignoreProperties)
        {
            var resultProperties = obj.GetType().GetProperties().Select(p => (p, (JsonPropertyAttribute)p.GetCustomAttributes(typeof(JsonPropertyAttribute), true).SingleOrDefault()));

            // Property has a value
            var property = resultProperties.SingleOrDefault(p => p.Item2?.PropertyName == prop.Name).p;
            if (property is null)
                property = resultProperties.SingleOrDefault(p => p.p.Name == prop.Name).p;
            if (property is null)
                property = resultProperties.SingleOrDefault(p => p.p.Name.ToUpperInvariant() == prop.Name.ToUpperInvariant()).p;

            if (property is null)
            {
                // Property not found
                throw new Exception($"{method}: Missing property `{prop.Name}` on `{obj.GetType().Name}`");
            }

            var propertyValue = property.GetValue(obj);
            if(property.GetCustomAttribute<JsonPropertyAttribute>(true)?.ItemConverterType == null)
                CheckPropertyValue(method, prop.Value, propertyValue, property.Name, prop.Name, property, ignoreProperties);
        }

        private static void CheckPropertyValue(string method, JToken propValue, object propertyValue, string propertyName = null, string propName = null, PropertyInfo info = null, Dictionary<string, List<string>> ignoreProperties = null)
        {
            if (propertyValue == default && propValue.Type != JTokenType.Null && !string.IsNullOrEmpty(propValue.ToString()))
            {
                // Property value not correct
                throw new Exception($"{method}: Property `{propertyName}` has no value while input json `{propName}` has value {propValue}");
            }

            if (propertyValue == default && (propValue.Type == JTokenType.Null || string.IsNullOrEmpty(propValue.ToString())))
                return;

            if (propertyValue.GetType().GetInterfaces().Contains(typeof(IDictionary)))
            {
                var dict = (IDictionary)propertyValue;
                var jObj = (JObject)propValue;
                var properties = jObj.Properties();
                foreach (var dictProp in properties)
                {
                    if (!dict.Contains(dictProp.Name))
                        throw new Exception($"{method}: Property `{propertyName}` has no value while input json `{propName}` has value {propValue}");

                    if (dictProp.Value.Type == JTokenType.Object)
                    {
                        // TODO Some additional checking for objects
                    }
                    else
                    {
                        if (dict[dictProp.Name] == default && dictProp.Value.Type != JTokenType.Null)
                        {
                            // Property value not correct
                            throw new Exception($"{method}: Dictionary entry `{dictProp.Name}` has no value while input json has value {propValue} for");
                        }
                    }
                }
            }
            else if (propertyValue.GetType().GetInterfaces().Contains(typeof(IEnumerable))
                && propertyValue.GetType() != typeof(string))
            {
                var jObjs = (JArray)propValue;
                var list = (IEnumerable)propertyValue;
                var enumerator = list.GetEnumerator();
                foreach (JToken jtoken in jObjs)
                {
                    enumerator.MoveNext();
                    var typeConverter = enumerator.Current.GetType().GetCustomAttributes(typeof(JsonConverterAttribute), true);
                    if (typeConverter.Any() && ((JsonConverterAttribute)typeConverter.First()).ConverterType != typeof(ArrayConverter))
                        // Custom converter for the type, skip
                        continue;

                    if (jtoken.Type == JTokenType.Object)
                    {
                        foreach (var subProp in ((JObject)jtoken).Properties())
                        {
                            if (ignoreProperties?.ContainsKey(method) == true && ignoreProperties[method].Contains(subProp.Name))
                                continue;

                            CheckObject(method, subProp, enumerator.Current, ignoreProperties);
                        }
                    }
                    else if (jtoken.Type == JTokenType.Array)
                    {
                        var resultObj = enumerator.Current;
                        var resultProps = resultObj.GetType().GetProperties().Select(p => (p, p.GetCustomAttributes(typeof(ArrayPropertyAttribute), true).Cast<ArrayPropertyAttribute>().SingleOrDefault()));
                        var arrayConverterProperty = resultObj.GetType().GetCustomAttributes(typeof(JsonConverterAttribute), true).FirstOrDefault();
                        var jsonConverter = (arrayConverterProperty as JsonConverterAttribute).ConverterType;
                        if (jsonConverter != typeof(ArrayConverter))
                            // Not array converter?
                            continue;

                        int i = 0;
                        foreach (var item in jtoken.Values())
                        {
                            var arrayProp = resultProps.SingleOrDefault(p => p.Item2.Index == i).p;
                            if (arrayProp != null)
                                CheckPropertyValue(method, item, arrayProp.GetValue(resultObj), arrayProp.Name, "Array index " + i, arrayProp, ignoreProperties);

                            i++;
                        }
                    }
                    else
                    {
                        var value = enumerator.Current;
                        if (value == default && ((JValue)jtoken).Type != JTokenType.Null)
                        {
                            throw new Exception($"{method}: Property `{propertyName}` has no value while input json `{propName}` has value {jtoken}");
                        }

                        CheckValues(method, propertyName, (JValue)jtoken, value);
                    }
                }
            }
            else
            {
                if (propValue.Type == JTokenType.Object)
                {
                    foreach (var item in propValue)
                    {
                        if (item is JProperty prop)
                        {
                            if (ignoreProperties?.ContainsKey(method) == true && ignoreProperties[method].Contains(prop.Name))
                                continue;

                            CheckObject(method, prop, propertyValue, ignoreProperties);
                        }
                    }
                }
                else
                {
                    if (info.GetCustomAttribute<JsonConverterAttribute>(true) == null
                        && info.GetCustomAttribute<JsonPropertyAttribute>(true)?.ItemConverterType == null)
                        CheckValues(method, propertyName, (JValue)propValue, propertyValue);
                }
            }
        }

        private static void CheckValues(string method, string property, JValue jsonValue, object objectValue)
        {
            if (jsonValue.Type == JTokenType.String)
            {
                if(objectValue is decimal dec)
                {
                    if(jsonValue.Value<decimal>() != dec)
                        throw new Exception($"{method}: {property} not equal: {jsonValue.Value<decimal>()} vs {dec}");
                }
                else if(objectValue is DateTime time)
                {
                    // timestamp, hard to check..
                }
                else if (jsonValue.Value<string>().ToLowerInvariant() != objectValue.ToString().ToLowerInvariant())
                    throw new Exception($"{method}: {property} not equal: {jsonValue.Value<string>()} vs {objectValue.ToString()}");
            }
            else if (jsonValue.Type == JTokenType.Integer)
            {
                if (jsonValue.Value<long>() != Convert.ToInt64(objectValue))
                    throw new Exception($"{method}: {property} not equal: {jsonValue.Value<long>()} vs {Convert.ToInt64(objectValue)}");
            }
            else if (jsonValue.Type == JTokenType.Boolean)
            {
                if (jsonValue.Value<bool>() != (bool)objectValue)
                    throw new Exception($"{method}: {property} not equal: {jsonValue.Value<bool>()} vs {(bool)objectValue}");
            }
        }
    }
}
