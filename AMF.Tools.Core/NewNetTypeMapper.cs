﻿using System.Collections.Generic;
using System.Linq;
using AMF.Parser.Model;
using Raml.Common;

namespace AMF.Tools.Core
{
    public class NewNetTypeMapper
    {
        private static readonly IDictionary<string, string> TypeStringConversion =
            new Dictionary<string, string>
            {
                {
                    "integer",
                    "int"
                },
                {
                    "string",
                    "string"
                },
                {
                    "boolean",
                    "bool"
                },
                {
                    "float",
                    "decimal"
                },
                {
                    "number",
                    "decimal"
                },
                {
                    "any",
                    "object"
                },
                {
                    "date",
                    "DateTime"
                },
                {
                    "datetime",
                    "DateTime"
                },
                {
                    "date-only",
                    "DateTime"
                },
                {
                    "time-only",
                    "DateTime"
                },
                {
                    "datetime-only",
                    "DateTime"
                },
                {
                    "file",
                    "byte[]"
                }
            };

        private static readonly IDictionary<string, string> NumberFormatConversion = new Dictionary<string, string>
        {
            {"double", "double"},
            {"float", "float"},
            {"int16", "short"},
            {"short", "short"},
            {"int64", "long"},
            {"long", "long"},
            {"int32", "int"},
            {"int", "int"},
            {"int8", "byte"}
        };

        private static readonly IDictionary<string, string> DateFormatConversion = new Dictionary<string, string>
        {
            {"rfc3339", "DateTime"},
            {"rfc2616", "DateTimeOffset"}
        };

        //TODO: check
        public static string GetNetType(Shape shape, IDictionary<string, ApiObject> existingObjects = null,
            IDictionary<string, ApiObject> newObjects = null)
        {
            if (!string.IsNullOrWhiteSpace(shape.LinkTargetName))
                return NetNamingMapper.GetObjectName(shape.LinkTargetName);

            if (shape is ScalarShape scalar)
                return GetNetType(scalar.DataType.Substring(scalar.DataType.LastIndexOf('#') + 1), scalar.Format);

            if (shape is ArrayShape array)
                return CollectionTypeHelper.GetCollectionType(GetNetType(array.Items, existingObjects, newObjects));

            if (shape is FileShape file)
                return TypeStringConversion["file"];

            if (shape.Inherits.Count() == 1)
            {
                if (shape is NodeShape nodeShape)
                {
                    if (nodeShape.Properties.Count() == 0)
                        return GetNetType(nodeShape.Inherits.First(), existingObjects, newObjects);
                }
                if (shape.Inherits.First() is ArrayShape arrayShape)
                    return CollectionTypeHelper.GetCollectionType(GetNetType(arrayShape.Items, existingObjects, newObjects));

                if (shape is AnyShape any)
                {
                    var key = NetNamingMapper.GetObjectName(any.Inherits.First().Name);
                    if (existingObjects != null && existingObjects.ContainsKey(key))
                        return key;
                    if (newObjects != null && newObjects.ContainsKey(key))
                        return key;
                }
            }
            if (shape.Inherits.Count() > 0)
            {
                //TODO: check
            }

            if (shape.GetType() == typeof(AnyShape))
            {
                return GetNetType("any", null);
            }

            return NetNamingMapper.GetObjectName(shape.Name);
        }

        public static string GetNetType(string type, string format)
        {
            string netType;
            if (!string.IsNullOrWhiteSpace(format) &&
                (NumberFormatConversion.ContainsKey(format.ToLowerInvariant()) || DateFormatConversion.ContainsKey(format.ToLowerInvariant())))
            {
                netType = NumberFormatConversion.ContainsKey(format.ToLowerInvariant())
                    ? NumberFormatConversion[format.ToLowerInvariant()]
                    : DateFormatConversion[format.ToLowerInvariant()];
            }
            else
            {
                netType = Map(type);
            }
            return netType;
        }

        public static string Map(string type)
        {
            if (type != null)
                type = type.Trim();

            return !TypeStringConversion.ContainsKey(type) ? null : TypeStringConversion[type];
        }

        private static readonly string[] OtherPrimitiveTypes = { "double", "float", "byte", "short", "long", "DateTimeOffset" };

        public static bool IsPrimitiveType(string type)
        {
            type = type.Trim();

            if (type.EndsWith("?"))
                type = type.Substring(0, type.Length - 1);

            if (OtherPrimitiveTypes.Contains(type))
                return true;

            return TypeStringConversion.Any(t => t.Value == type) || TypeStringConversion.ContainsKey(type);
        }

        private static readonly string[] NullableTypes = new string[] { "int", "long", "double", "short", "byte", "float", "bool" };
        public static bool IsNullableType(string type)
        {
            return !NullableTypes.Contains(type);
        }

    }
}