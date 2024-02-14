using System.Runtime.CompilerServices;

namespace Web_II_Labs.ExtensionMethods
{

    public static class AnimalExtension
    {
        public static String getFamily(this Animal animal)
        {
            return animal.Family;
        }

    }
}
