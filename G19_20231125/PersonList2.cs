using System.Collections;

namespace G19_20231125
{
    public class PersonList2 : IList<Person>
    {
        private List<Person> _list;
        private HashSet<int> _ids;
        private ICollection<Person> _children;
        public PersonList2()
        {
            _list = new List<Person>();
            _ids = new HashSet<int>();
            _children = new List<Person>();
        }

        public void Add(Person item)
        {
            if (!_ids.Add(item.Id))
            {
                throw new ArgumentException("Id is not unique");
            }

            _list.Add(item);

            //_children.Add((Person)item.Children);       
        }

        public void AddRange(Person[] items)
        {
            foreach (var person in items)
            {
                if (_ids.Contains(person.Id))
                {
                    throw new ArgumentException("Id is not unique");
                }
            }
            foreach (var person in items)
            {
                _ids.Add(person.Id);
            }

            _list.AddRange(items);
        }

        public void Save(string filePath)
        {
            using (BinaryWriter writer = new BinaryWriter(new FileStream(filePath, FileMode.Create)))
            {
                foreach (var person in this)
                {
                    writer.Write(person.Id);
                    writer.Write(person.Firstname);
                    writer.Write(person.Lastname);
                    writer.Write(person.BirthDate.ToBinary());
                    writer.Write((byte)person.Gender);

                    writer.Write(person.Children.Count);
                    foreach (var child in person.Children)
                    {
                        writer.Write(child.Id);
                        writer.Write(child.Firstname);
                        writer.Write(child.Lastname);
                        writer.Write(child.BirthDate.ToBinary());
                        writer.Write((byte)child.Gender);
                    }
                }
            }
        }


        public void Load(string filePath)
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
                        Gender = (GenderType)reader.ReadByte(),
                        //Children = (ICollection<Person>)reader.Read()


                    };

                    int numberOfchildrenCount = reader.ReadInt32();

                    List<Person> children = new List<Person>();
                    for (int i = 0; i < numberOfchildrenCount; i++)
                    {
                        Person child = new Person
                        {
                            Id = reader.ReadInt32(),
                            Firstname = reader.ReadString(),
                            Lastname = reader.ReadString(),
                            BirthDate = DateTime.FromBinary(reader.ReadInt64()),
                            Gender = (GenderType)reader.ReadByte(),
                        };

                        children.Add(child);
                    }

                    person.Children = children;
                    this.Add(person);
                }
            }
        }


        public Person this[int index]
        {
            get
            {
                return _list[index];
            }
            set
            {
                if (_list[index].Id != value.Id && !_ids.Add(value.Id))
                {
                    throw new ArgumentException("Id is not unique");
                }

                _list[index] = value;
            }
        }

        public int Count => _list.Count;

        public bool IsReadOnly => false;

        public void Clear()
        {
            _ids.Clear();
            _list.Clear();
        }

        public bool Contains(Person item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(Person[] array, int arrayIndex)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                array.SetValue(_list[i], arrayIndex);
            }
        }

        public IEnumerator<Person> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        public int IndexOf(Person item)
        {
            return _list.IndexOf(item);
            //{ 
            //for (int i = 0; i < _list.Count; i++)
            //{
            //    if (_list[i].Equals(item))
            //    {
            //        return i;
            //    }
            //}
            //return -1;
        }

        public void Insert(int index, Person item)
        {
            if (index < 0 || index > _list.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range");
            }

            if (!_ids.Add(item.Id))
            {
                throw new ArgumentException("Id is not unique");
            }

            _list.Insert(index, item);
        }

        public void InsertRange(int index, Person[] items)
        {
            foreach (var person in items)
            {
                if (_ids.Contains(person.Id))
                {
                    throw new ArgumentException("Id is not unique");
                }
            }

            foreach (var item in items)
            {
                _ids.Add(item.Id);
            }

            _list.InsertRange(index, items);
        }

        public bool Remove(Person item)
        {
            _ids.Remove(item.Id);
            return _list.Remove(item);
        }

        public void RemoveAt(int index)
        {
            _ids.Remove(_list[index].Id);
            _list.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
