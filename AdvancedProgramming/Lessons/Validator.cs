using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdvancedProgramming.Lessons
{

    
    abstract class ValidationAttribute : Attribute
    {
        
        public abstract string Details { get;  }
    
        public abstract bool IsValid(object? obj);

    }
    
    

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    class RequiredAttribute : ValidationAttribute
    {
        
        public override string Details => "Required Field/Property";

        public override bool IsValid(object? obj)
        {
            if (obj is null) return false; // Nullable<> , referenceType

            if (obj is string s) return string.IsNullOrEmpty(s);

            return true ;
        }
    
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    class RangeAttribute: ValidationAttribute
    {
        public int Maximum { get; set; }

        public int Minimum { get; set; }

        public override string Details { get; }

        public RangeAttribute(int maximum, int minimum)
        {
            Maximum = maximum;
            Minimum = minimum;
            Details = $"Range Must Be Between {minimum}-{maximum}";
        }

        
        public override bool IsValid(object? obj)
        {
            int value = (int)obj;

            return value <= Maximum && value >= Minimum;
        }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    class PatternAttribute : ValidationAttribute 
    {

        public string Pattern { get { return _regex.ToString(); } }
        
        public override string Details { get; } 

        private Regex _regex;

        public PatternAttribute(string pattern)
        {
            _regex = new Regex(pattern);
            Details = $"Pattern Must Be {pattern}";
        }

        public override bool IsValid(object? obj)
        {
            if (obj is null) return false;

            if (obj is string str)
                return _regex.IsMatch(str);

            return false;


        }
    }

    class Error
    {
        public string field;
        public string details;

        public Error(string field, string details)
        {
            this.field = field;
            this.details = details;
        }

        public override string ToString()
        {
            return $"fiels : {field} , details : {details}";
        }
    }

    internal static class Validator
    {


        // C#
        public static bool Validate(object? obj,List<Error>? errors = null)
        {
            if (obj is null) return false;
            bool isValid = true;
            var properties = obj.GetType().GetProperties();

            foreach (var property in properties)
            {
                var attrs = property.GetCustomAttributes<ValidationAttribute>();
                
                if (attrs is null) continue;

                foreach (var attr in attrs)
                {
                    var attrType = attr.GetType();
                    var attrMethod = attrType.GetMethod("IsValid");
                    var methodParams = attrMethod.GetParameters();

                    var value = property.GetValue(obj);
                    object[] invokeParams;
                    invokeParams = new object[] { value };
                    
                    object invokeResult = attrMethod.Invoke(attr, invokeParams);
                    

                    if( !(bool)invokeResult)
                    {
                        isValid = (bool)invokeResult;
                        var detailsProp = attrType.GetProperty("Details");
                        var details = detailsProp?.GetValue(attr)?.ToString() ?? string.Empty;
                        errors?.Add(new Error(property.Name, details));
                    }
                    
                }
            }
            return isValid;
        }

    }
}
