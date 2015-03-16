using System;
using ServiceStack;

namespace ToDoBackend
{
    public class ToDo
    {
        public string title { get; set; }
        public bool completed { get; set; }
        public string url { get; set; }
        public string itemid { get; set; }
        public int order { get; set; }

        public ToDo()
        {
            this.itemid = Guid
                .NewGuid()
                .ToString();

            this.url = new ViewRequest {id = this.itemid}
                .ToAbsoluteUri();
        }
    }
}