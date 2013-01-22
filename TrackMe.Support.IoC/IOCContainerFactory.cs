namespace TrackMe.Support.IoC
{
    public sealed class IOCContainerFactory
    {
        #region private fields

        private static readonly IOCContainerFactory instance = new IOCContainerFactory();
        private IIOCContainer currentContainer;

        #endregion

        #region constructor

        private IOCContainerFactory()
        {
            this.CurrentContainer = new IOCUnityContainer();
        }
        
        #endregion

        #region public properties

        public static IOCContainerFactory Instance
        {
            get
            {
                return instance;
            }
        }

        #endregion

        #region public methods

        public IIOCContainer CurrentContainer
        {
            get
            {
                return this.currentContainer;
            }

            private set
            {
                this.currentContainer = value;
            }
        }

        #endregion
    }
}
