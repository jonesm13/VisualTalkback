namespace Studio
{
    using Nancy.Bootstrappers.Ninject;
    using Ninject;

    public class Bootstapper : NinjectNancyBootstrapper
    {
        private readonly IKernel kernel;

        public Bootstapper(IKernel kernel)
        {
            this.kernel = kernel;
        }

        protected override IKernel GetApplicationContainer()
        {
            this.kernel.Load<FactoryModule>();
            return this.kernel;
        }
    }
}