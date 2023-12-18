namespace G19_20231125
{
    public class Person
    {
        public int Id { get; set; }
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public GenderType Gender { get; set; }
        public ICollection<Person> Children { get; set; }

        public Person()
        {
            Children = new List<Person>();
        }

        public Person(int id, string firstname, string lastname, DateTime birthDate, GenderType gender) : this()
        {
            this.Id = id;
            this.Firstname = firstname;
            this.Lastname = lastname;
            this.BirthDate = birthDate;
            this.Gender = gender;           
        }

        public override bool Equals(object? obj)
        {
            return (obj as Person).Id == this.Id;
        }

        public override string ToString()
        {

            //string result = $"{Id} {Firstname} {Lastname} {BirthDate:dd.MM.yyyy} {Gender}";

            //if (Children.Count >= 0)
            //{
            //    foreach (Person child in Children)
            //    {
            //        result += $"\n\tChildren: {child.ToString()};";


            //        if (child.Children.Count > 0)
            //        {
            //            foreach (var p in child.Children)
            //            {
            //                result += $"\tChildren of : {p.ToString()}";
            //            }
            //        }
            //    }

            //    // აი ამას რომ ტყუილად დავახარჯე საათები ძაან მაგარია. ბრავო
            return $"{Id} {Firstname} {Lastname} {BirthDate:dd.MM.yyyy} {Gender}";
        }
    }

    public enum GenderType : byte
    {
        Male = 0,
        Female = 1,
    }
}


