namespace Studio
{
    using System;
    using System.Configuration;
    using System.Windows.Forms;
    using Nancy.Hosting.Self;
    using Ninject;

    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var listenPort = int.Parse(ConfigurationManager
                .AppSettings["studio.http.listenPort"]);
            var listenUrl = $"http://localhost:{listenPort}";

            var kernel = new StandardKernel();
            var form = kernel.Get<MainForm>();

            kernel.Bind<ITalkbackService>().ToConstant(form);

            var config = new HostConfiguration
            {
                UrlReservations = {CreateAutomatically = true}
            };

            using (var nancyHost = new NancyHost(
                new Bootstapper(kernel),
                config,
                new Uri(listenUrl)))
            {
                nancyHost.Start();
                Application.Run(form);
            }
        }
    }
}