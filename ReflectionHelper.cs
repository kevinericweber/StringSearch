using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StringSearch
{
    public static class ReflectionHelper
    {
        public static List<T> GetAllNonabstractClassesThatImplement<T>()
        {
            object[] emptyArgs = new object[0];
            return GetAllNonabstractClassesThatImplement<T>(emptyArgs);
        }
        public static List<T> GetAllNonabstractClassesThatImplement<T>(Object[] classConstructorArgs)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            List<T> retVal = new List<T>();
            IEnumerable<object> instances = from t in assembly.GetTypes()
                                            where t.GetInterfaces().Contains(typeof(T)) && !t.IsAbstract
                                            select Activator.CreateInstance(t, classConstructorArgs) as object;
            foreach (T instance in instances)
            {
                retVal.Add(instance);
            }
            return retVal;
        }
    }
}
