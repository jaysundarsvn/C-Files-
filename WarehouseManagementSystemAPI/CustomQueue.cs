using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagementSystemAPI
{
    public delegate void QueueEventHandler<T, U>(T sender, U eventArgs); // delegate declaration for event handler
    public class CustomQueue<T> where T : IEntityPrimaryProperties, IEntityAdditionalProperties
    {
        Queue<T> _queue = null; // delcaration of queue
        public event QueueEventHandler<CustomQueue<T>, QueueEventArgs> CustomQueueEvent; // event declaration
        //we use the delegate implementation for events because delegates are also used to implement for events 

        public CustomQueue()
        {
            _queue = new Queue<T>();
        }

        public int QueueLength
        {
            //getting queue length
            get { return _queue.Count; }
        }

        public void AddItem(T item)
        {
            //adding items to queue
            _queue.Enqueue(item);

           //message variable of QueueEventArgs has been implmemnted here for adding
            QueueEventArgs queueEventArgs = new QueueEventArgs { message = $"Date and time is {DateTime.Now.ToString(Constants.DateTimeFormat)} ID is {item.Id} \n Name is {item.Name}" +
                $" \n Type is {item.Type} \n Qantity is {item.Quantity} \n Unit Value is {item.UnitValue} has been added to the queue"};

            OnQueueChanged(queueEventArgs);
        }

        public T GetItem() //using generic type
        {
            T item = _queue.Dequeue(); //FIFO principle pf queue

            QueueEventArgs queueEventArgs = new QueueEventArgs
            {
                //message variable of QueueEventArgs has been implmemnted for removing
                message = $"Date and time is {DateTime.Now.ToString(Constants.DateTimeFormat)} ID is {item.Id} \n Name is {item.Name}" +
                $" \n Type is {item.Type} \n Qantity is {item.Quantity} \n Unit Value is {item.UnitValue} has been processed from the queue"
            };

            OnQueueChanged(queueEventArgs);

            return item;
        }

        protected virtual void OnQueueChanged(QueueEventArgs a) 
        {
            CustomQueueEvent(this, a);
        }

        public IEnumerator<T> GetEnumerator() 
        {
            // traversing through queue using enum bcausing of its multiple types in queue
            return _queue.GetEnumerator();
        }
    }

    public class QueueEventArgs : System.EventArgs
    {
        public string message { get; set; }
    }
}
