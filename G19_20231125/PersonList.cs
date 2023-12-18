namespace G19_20231125
{
    // aseve unda vuzrunvelyot rom siashi ar moxvdes ori ertidaigive id mqonde adamiani.
    public class PersonList : List<Person>
    {
        private HashSet<int> _ids = new HashSet<int>();
        public new void Add(Person value)
        {
            if (!_ids.Add(value.Id))
            {
                throw new ArgumentException("Id is not unique");
            }
            //foreach (var person in this)
            //{
            //	if (person.Id == value.Id)
            //	{
            //		throw new ArgumentException("Id is not unique");
            //	}
            //}
            base.Add(value);       
        }

        public new void Remove(Person value)
        {
            _ids.Remove(value.Id);
            base.Remove(value);
        }

        public void Save(string filePath)
        {
            using (BinaryWriter writer = new(new FileStream(filePath, FileMode.Create)))
            {
                foreach (var person in this)
                {
                    writer.Write(person.Id);
                    writer.Write(person.Firstname);
                    writer.Write(person.Lastname);
                    writer.Write(person.BirthDate.ToBinary());
                    writer.Write((byte) person.Gender);
                }
            }
        }

        public new void Load(string filePath)
        {
            this.Clear();
            this._ids.Clear();
            using (BinaryReader reader = new(new FileStream(filePath, FileMode.Open)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    Person person = new Person
                    {
                        Id = reader.ReadInt32(),
                        Firstname = reader.ReadString(),
                        Lastname = reader.ReadString(),
                        BirthDate = DateTime.FromBinary(reader.ReadInt64()),
                        Gender = (GenderType) reader.ReadByte()
                    };
                    this.Add(person);
                }
            }
        }
    }
}