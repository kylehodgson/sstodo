using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using ServiceStack;

namespace ToDoBackend
{
    public interface IToDoRepo
    {
        List<ToDo> Items();
        void DropItems();
        void Add(ToDo toDo);
        ToDo ById(string id);
        void Save(ToDo toDo);
        void DeleteItemById(string id);
    }

    public class StaticHashSetToDoRepo : IToDoRepo
    {
        static HashSet<ToDo> _items = new HashSet<ToDo>(); 

        public List<ToDo> Items()
        {
            return _items
                .AsEnumerable()
                .ToList();
        }

        public void DropItems()
        {
            _items = new HashSet<ToDo>();
        }

        public void Add(ToDo toDo)
        {
            _items.Add(toDo);
        }

        public ToDo ById(string id)
        {
            return _items.Single(toDo => toDo.itemid.Equals(id));
        }

        public void Save(ToDo toDo)
        {
            this.DeleteItemById(toDo.itemid);
            this.Add(toDo);
        }

        public void DeleteItemById(string id)
        {
            _items.Remove(this.ById(id));
        }
    }
}