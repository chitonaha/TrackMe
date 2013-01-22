using System;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel;
using System.Web;
using Microsoft.Practices.Unity;
using TrackMe.Support.IoC.Resources;

namespace TrackMe.Support.IoC.LifetimeManagers
{
    sealed class PerExecutionContextLifetimeManager : LifetimeManager
    {
        private Guid key;

        public PerExecutionContextLifetimeManager() : this(Guid.NewGuid())
        {
        }

        private PerExecutionContextLifetimeManager(Guid key)
        {
            if (key == Guid.Empty)
            {
                throw new ArgumentException(Messages.PerExecutionContextLifetimeManagerKeyCannotBeNull);
            }

            this.key = key;
        }

        public override object GetValue()
        {
            object result = null;

            if (OperationContext.Current != null)
            {
                ContainerExtension containerExtension = OperationContext.Current.Extensions.Find<ContainerExtension>();
                if (containerExtension != null)
                {
                    result = containerExtension.Value;
                }
            }
            else if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Items[key.ToString()] != null)
                {
                    result = HttpContext.Current.Items[key.ToString()];
                }
            }
            else
            {
                result = CallContext.GetData(key.ToString());
            }

            return result;
        }

        public override void RemoveValue()
        {
            if (OperationContext.Current != null)
            {
                ContainerExtension containerExtension = OperationContext.Current.Extensions.Find<ContainerExtension>();
                if (containerExtension != null)
                {
                    OperationContext.Current.Extensions.Remove(containerExtension);
                }
            }
            else if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Items[key.ToString()] != null)
                {
                    HttpContext.Current.Items[key.ToString()] = null;
                }
            }
            else
            {
                CallContext.FreeNamedDataSlot(key.ToString());
            }
        }

        public override void SetValue(object newValue)
        {
            if (OperationContext.Current != null)
            {
                ContainerExtension containerExtension = OperationContext.Current.Extensions.Find<ContainerExtension>();
                if (containerExtension == null)
                {
                    containerExtension = new ContainerExtension()
                    {
                        Value = newValue
                    };

                    OperationContext.Current.Extensions.Add(containerExtension);
                }
            }
            else if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Items[key.ToString()] == null)
                {
                    HttpContext.Current.Items[key.ToString()] = newValue;
                }
            }
            else
            {
                CallContext.SetData(key.ToString(), newValue);
            }
        }

        class ContainerExtension : IExtension<OperationContext>
        {
            public object Value { get; set; }

            public void Attach(OperationContext owner)
            {
            }

            public void Detach(OperationContext owner)
            {
            }
        }
    }
}
