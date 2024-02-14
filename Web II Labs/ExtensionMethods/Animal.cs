namespace Web_II_Labs.ExtensionMethods
{
    public class Animal
    {
        public string Name { get; set; }
        public string Family {  get; set; }
        public Animal() { }
        public Animal(string name) { 
        }

        public String getType()
        {
            return this.Name;
        }
    }
}
