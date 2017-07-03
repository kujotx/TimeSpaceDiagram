namespace TimeSpaceDiagram.Controls
{
    using System;

    using System.Windows.Controls;

    using Ninject;

    using TimeSpaceDiagram.IoC;

    public abstract class TsUserControl : UserControl, IDisposable
    {
        protected readonly IKernel Kernel;

        protected TsUserControl()
        {
            Kernel = new StandardKernel(new TrafficRegistry());
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                Kernel.Dispose();
            }
            // free native resources if there are any.
        }
    }
}