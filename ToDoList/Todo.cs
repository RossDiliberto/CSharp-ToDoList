using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Todo
    {
        public int id { get; set; }
        public bool done { get; set; }
        public string task { get; set; }

        public Todo(bool done,string task)
        {
            this.done = done;
            this.task = task;
            
        }

        /*
         *  Per avere una lista completa:
         *  
                public string Extended
                {
                    get
                    {
                        return $" { id } { done } { task } ";
                    }
                }
        */
    }
}