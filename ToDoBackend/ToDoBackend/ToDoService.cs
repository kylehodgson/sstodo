using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;

namespace ToDoBackend
{
    [Route("/todo","GET")]
    public class ListRequest : IReturn<List<ToDo>> 
    { }

    [Route("/todo/{id}","GET")]
    public class ViewRequest : IReturn<ToDo>
    {
        public string id { get; set; }
    }

    [Route("/todo","POST")]
    public class CreateRequest : IReturn<ToDo>
    {
        public string title { get; set; }
        public int order { get; set; }
    }

    [Route("/todo","DELETE")]
    [Route("/todo/{id}","DELETE")]
    public class DeleteRequest : IReturnVoid
    {
        public string id { get; set; }
    }

    [Route("/todo/{id}","PATCH")]
    public class UpdateRequest : IReturn<ToDo>
    {
        public string id { get; set; }
        public string title { get; set; }
        public bool? completed { get; set; }
        public int? order { get; set; }
    }

    public class ToDoService : IService
    {
        public IToDoRepo ToDoRepo { get; set; }

        public Object Get(ListRequest request)
        {
            return ToDoRepo.Items();
        }

        public Object Get(ViewRequest request)
        {
            return ToDoRepo.ById(request.id);
        }

        public Object Post(CreateRequest request)
        {
            var toDo = new ToDo()
                .PopulateWithNonDefaultValues(request);
            ToDoRepo.Add(toDo);
            return toDo;
        }

        public Object Patch(UpdateRequest request)
        {
            var item = ToDoRepo
                .ById(request.id)
                .PopulateWithNonDefaultValues(request);
            ToDoRepo.Save(item);
            return item;
        }

        public void Delete(DeleteRequest request)
        {
            if (request.id.IsNullOrEmpty())
            {
                ToDoRepo.DropItems();
            }
            else
            {
                ToDoRepo.DeleteItemById(request.id);
            }
        }
    }
}