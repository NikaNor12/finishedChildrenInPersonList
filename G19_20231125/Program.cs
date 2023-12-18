namespace G19_20231125
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"D:/gela.txt";
            Person person1 = new Person(1, "Firstname1", "Lastname1", new DateTime(1993, 3, 1), GenderType.Male);
            Person person2 = new Person(2, "Firstname2", "Lastname2", new DateTime(1993, 3, 1), GenderType.Male);
            Person person3 = new Person(3, "Firstname3", "Lastname3", new DateTime(1993, 3, 1), GenderType.Male);
            //
            Person person4 = new Person(4, "Firstname4", "Lastname4", new DateTime(1993, 3, 1), GenderType.Male);
            Person person5 = new Person(5, "Firstname5", "Lastname5", new DateTime(1993, 3, 1), GenderType.Male);
            Person person6 = new Person(6, "Firstname6", "Lastname6", new DateTime(1993, 3, 1), GenderType.Male);
            Person person7 = new Person(7, "Firstname7", "Lastname7", new DateTime(1993, 3, 1), GenderType.Male);
            Person person8 = new Person(8, "Firstname7", "Lastname7", new DateTime(1993, 3, 1), GenderType.Male);

            person1.Children.Add(person2);
            person1.Children.Add(person3);

            person4.Children.Add(person5);
            person4.Children.Add(person6);
            person4.Children.Add(person8);

            person6.Children.Add(person7);

            PersonList2 persons = new();
            persons.Add(person1);
            persons.Add(person4);
           

            PrintPersons(persons);
            persons.Save(filePath);
           persons.Load(filePath);
        }

        // gadaaketet ise rom amobechdos personebi tavis shveilebtan da shvilishvilebtan da ase shemdeg. 
        private static void PrintPersons(IEnumerable<Person> persons)
        {
            foreach (var p in persons)
            {
                Console.WriteLine($"{p}");
                PrintChildren(p.Children, 1);
                Console.WriteLine();
            }
        }
        private static void PrintChildren(IEnumerable<Person> children, int level)
        {
            foreach (var child in children)
            {
                Console.WriteLine($"{new string('\t', level)} Child: {child}");
                PrintChildren(child.Children, level + 1);
            }
        }

    }
}
