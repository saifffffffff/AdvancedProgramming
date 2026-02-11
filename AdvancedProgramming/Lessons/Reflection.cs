using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedProgramming.Lessons
{


    internal static class Reflection
    {
        public static class ObtainingTypes
        {

            public static void WriteNestedTypesToConsole(Type type)
            {

                Console.WriteLine("\nNested Types");
                Type[] nestedTypes = type.GetNestedTypes();

                foreach (var nestedType in nestedTypes)
                    Console.WriteLine($"\tFullName: {nestedType.FullName}");


            }

            public static void WriteEnumsToConsole(Type type)
            {
                Console.WriteLine("Enums : ");
                string[] enums = type.GetEnumNames();

                foreach (var enum_ in enums)
                    Console.WriteLine($"\tenum name : {enum_}");

            }

            public static void WriteFieldsToConsole(Type type)
            {
                Console.WriteLine("\nFields : ");
                FieldInfo[] fields = type.GetFields();
                foreach (var field in fields)
                {
                    Console.WriteLine($"\nName : {fields.Length}");
                }
            }

            public static void WritePropertiesToConsole(Type type)
            {
                Console.WriteLine("\nProperties : ");
                PropertyInfo[] properities = type.GetProperties();


                foreach (var property in properities)
                    Console.WriteLine(
                        $"Name ; {property.Name,-20} Type : {property.PropertyType.Name,-20} Getter Method: {property.GetGetMethod()?.Name,-20} Setter Method : {property.GetSetMethod()?.Name,-20}"
                    );

            }

            public static void WriteInterfacesToConsole(Type type)
            {
                Console.WriteLine("\nInterfaces : ");
                Type[] interfaces = type.GetInterfaces();

                foreach (var interface_ in interfaces)
                    Console.WriteLine($"\tinterface name : {interface_}");

            }

            public static void WriteEventsToConsole(Type type)
            {
                EventInfo[] events = type.GetEvents();
                foreach (var e in events)
                    Console.WriteLine(e);
            }

            public static void WriteTypeMembersToConsole(Type type)
            {

                MemberInfo[] members = type.GetMembers(BindingFlags.Public | BindingFlags.Instance); // Flag Enums ( bitwise operator are used ) 
                foreach (var member in members)
                {
                    Console.WriteLine(member);
                }



            }

            public static void WriteConstructorsToConsole(Type type)
            {
                ConstructorInfo[] constructors = type.GetConstructors();

                foreach (var constructor in constructors)
                    Console.WriteLine($"Constructor Name: {constructor,-60} Paramerters: {string.Join<ParameterInfo>("   ", constructor.GetParameters())}");
            }

            public static MemberInfo GetMemberByName(Type type, string memberName)
            {
                MemberInfo[] member = type.GetMember(memberName); // array in case of overloading
                return member[0];
            }

            public static void InvokeMethod<T>(T obj, string methodName, object[] parameters)
            {

                MethodInfo? method = typeof(T).GetMethod(methodName );
                method.Invoke(obj, parameters);

            }


        }
        public static class InstantiatingObjects
            {
                public class Zombie
                {
                    const int Health = 100;
                    const int Speed = 100;
                    const int Damage = 14;

                    public override string ToString()
                    {
                        return $"Health : {Health} \tSpeed : {Speed}\tDamage{Damage}";
                    }
                }
                public class Skeleton
                {
                    const int Health = 70;
                    const int Speed = 80;
                    const int Damage = 30;

                    public override string ToString()
                    {
                        return $"Health : {Health} \tSpeed : {Speed}\tDamage{Damage}";
                    }
                }
                public class Creeper
                {
                    const int Health = 80;
                    const int Speed = 140;
                    const int Damage = 50;

                    public override string ToString()
                    {
                        return $"Health : {Health} \tSpeed : {Speed}\tDamage{Damage}";
                    }
                }

                public static void GetEnemyDescription(object enemy)
                {
                    Console.WriteLine(enemy.ToString());
                }
                public static void StartGame()
                {
                    var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;

                    do
                    {

                        Console.Write("Send Enemy : ");
                        var input = Console.ReadLine()!.Trim(); // this supposed to be any message 

                        try
                        {
                            ObjectHandle? objectHandler = Activator.CreateInstance(assemblyName, $"AdvancedProgramming.Lessons.Reflection+InstantiatingObjects+{input}"); // nested types seperated with +
                            object? obj = objectHandler?.Unwrap();
                            GetEnemyDescription(obj);
                        }
                        catch { }

                    } while (true);


                }
            }
        public static class Examples
        {
            public class AutoMapper
            {
                public static void Map<TSrc, TDest>(TSrc source, TDest destination)
                {
                    PropertyInfo[] sourceMembers = typeof(TSrc).GetProperties();
                    PropertyInfo[] destinationMembers = typeof(TDest).GetProperties();
                    foreach (var s_member in sourceMembers)
                    {
                        foreach (var d_member in destinationMembers)
                        {
                            if (s_member.Name == d_member.Name)
                            {
                                d_member.SetValue(destination, s_member.GetValue(source));
                                break;
                            }
                        }
                    }

                }
                public static void MapFields<TSrc, TDest>(TSrc source, TDest destination)
                {


                    foreach (var sourceField in source!.GetType().GetFields())
                    {
                        var destinationField = destination!.GetType().GetField(sourceField.Name);

                        if (destinationField != null)
                            destinationField.SetValue(destination, sourceField.GetValue(source));

                    }
                }
                public static void MapProperties<TSrc, TDest>(TSrc source, ref TDest destination)
                {

                    foreach (var sourceProperty in source!.GetType().GetProperties())
                    {
                        var destinationProperty = destination!.GetType().GetProperty(sourceProperty.Name);

                        if (destinationProperty != null)
                        {
                            if (destinationProperty.PropertyType == sourceProperty.PropertyType)
                                destinationProperty.SetValue(destination, sourceProperty.GetValue(source));
                            else
                            {
                                object? sourceValue = sourceProperty.GetValue(source);
                                object? destinationValue = destinationProperty.GetValue(destination);

                                destinationValue ??= Activator.CreateInstance(destinationProperty.PropertyType); ;

                                MapProperties(sourceValue, ref destinationValue);

                                destinationProperty.SetValue(destination, destinationValue);

                            }

                        }


                    }
                }
                public static void MapEnums<TSrc, TDest>(TSrc source, TDest destination)
                {

                    var sourceEnums = typeof(TSrc).GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic).Where(t => t.IsEnum);
                    var destinationEnums = typeof(TDest).GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic).Where(t => t.IsEnum);

                    var destinationEnumsMembers = typeof(TDest).GetMembers().Where(member => destinationEnums.Contains(member.GetType()));
                    var sourceEnumsMembers = typeof(TSrc).GetMembers().Where(member => sourceEnums.Contains(member.GetType()));

                    foreach (var s_enum in sourceEnums)
                    {
                        foreach (var d_enum in destinationEnums)
                        {
                            if (s_enum.Name == d_enum.Name)
                            {
                                var d_enum_value = destinationEnumsMembers.Where(member => member.GetType() == d_enum).First();
                                var s_enum_value = sourceEnumsMembers.Where(member => member.GetType() == s_enum).First();

                                if (d_enum_value is PropertyInfo d_property_value && s_enum_value is PropertyInfo s_property_value)
                                {
                                    d_property_value.SetValue(destination, s_property_value.GetValue(source));
                                    break;
                                }

                                if (d_enum_value is FieldInfo d_field_value && s_enum_value is FieldInfo s_field_value)
                                {
                                    d_field_value.SetValue(destination, s_field_value.GetValue(source));
                                    break;
                                }

                            }
                        }
                    }


                }
            }
        }
    }
}
