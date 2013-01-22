using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrackMe.Model.Interfaces;
using TrackMe.Support.Context;

namespace TrackMe.Business
{
    public abstract class BaseManager
    {
        private readonly IUnitOfWork unitOfWork;

        protected IUnitOfWork UnitOfWork { get { return unitOfWork; } }

        public BaseManager(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
    }
}
