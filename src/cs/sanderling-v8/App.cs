using System;
using System.IO;
using System.Threading;
using Espresso;
using System.Linq;
using Bib3.Geometrik;
using Sanderling;
using Sanderling.Interface;
using Sanderling.Interface.MemoryStruct;


namespace sanderling_v8
{
    class App : IDisposable
    {
        public static string HOME_PATH = System.IO.Directory.GetCurrentDirectory();
        public static string ROOT_PATH = System.IO.Path.GetFullPath(HOME_PATH + "\\..");
        public static string JS_SRC_PATH = System.IO.Path.GetFullPath(HOME_PATH + "\\src\\js\\");
        public static string VERSION = "0.0.0";

        private static App _singleton = null;

        private JsEngine _jsEngine = null;
        private JsContext _jsContext = null;
        private int? _eveClientProcessId = null;
        private Sensor _sensor = null;
        private bool _running = false;

        public static App initialize(string[] args) {
             if (_singleton != null) {
                throw new Exception("Singleton already constructed.");
            }

            App app = new App(args);
            _singleton = app;
            return app;
        }

        public void Dispose() {
            if (_jsContext != null) {
                _jsContext.Dispose();
                _jsContext = null;
            }
            if (_jsEngine != null) {
                _jsEngine.Dispose();
                _jsEngine = null;
            }
            if (_singleton != null) {
                _singleton = null;
            }
        }

        private App(string[] args)
        {
            _initialize();
        }
        
        private void _initialize()
        {
            _singleton = this;
            initV8();
            initSanderling();
        }

        public void log(string message) {
            System.Console.WriteLine(message);
        }

        public void start() {
            // try to connect to the client
            int retries = 0;
            while (!_running)
            {
                try
                {
                    startSanderling();
                    _running = true;
                }
                catch (TimeoutException e)
                {
                    if (retries++ == 0)
                    {
                        log(e.Message);
                        log("Waiting to start ...");
                    }

                    Thread.Sleep(10000); // 10 seconds
                }
            }

            loop();
        }

        public void stop()
        {
            _running = false;
            stopSanderling();
        }

        private void initV8()
        {
            Espresso.JsBridge.V8Init();
            _jsEngine = new JsEngine();
            _jsContext = _jsEngine.CreateContext();

            // load the bootstrap / interface class; EspressV8Interface
            loadJsFile("sanderling-v8-lib/require");
            _jsContext.Execute("(function(v8AppIface){ require(['sanderling-v8/bootstrap'], function(bootstrap) { bootstrap.init(todo); }); })(todo);");
            

            // test interface
            var version = _jsContext.Execute("(function() { var res = null; require(['sanderling-v8/v8/interface/Espresso'], function(EspressoV8Interface) { res = new EspressoV8Interface().getVersion(); } ); return res; })();");
            if (!VERSION.Equals(version)) {
                throw new Exception("Cannot init espresso: JS class file not loaded or version mismatch.");
            }

            log("JS interface v" + version + " loaded.");
            log("Initialized V8 JS engine.");
        }

        private void initSanderling() {
            _sensor = new Sensor();

            log("Initialized Sanderling Eve bot engine.");
        }

        private void startSanderling() {
            // determine client process id
            log("Searching for Eve client ...");
            _eveClientProcessId = findEveClientProcessId();
            if (_eveClientProcessId == null) {
                throw new TimeoutException("Cannot start sanderling: Eve client process not available.");
            }

            log("Found Eve client at process id: " + _eveClientProcessId.Value);

            // initial read takes longer; 15s. we throw this away
            var response = _sensor.MeasurementTakeNewRequest(_eveClientProcessId.Value);
            if (response == null) {
                throw new TimeoutException("Cannot start sanderling: Eve client unresponsive.");
            } else if (response.MemoryMeasurement != null)
            {
                log("Memory!");
            }

            log("Started Sanderling Eve interface.");
        }

        private void stopSanderling()
        {
            log("Stopped Sanderling Eve interface.");
        }

        private void loop()
        {
            log("Running.");

            while (_running) {
                var response = _sensor.MeasurementTakeNewRequest(_eveClientProcessId.Value);
                if (response == null) {
                    throw new TimeoutException("Cannot continue sanderling: Eve client unresponsive to measurement.");
                } else if (response.MemoryMeasurement == null) {
                    log("Memory scan not ready. Retrying ...");
                    Thread.Sleep(2000); // 1 second
                } else {
                    runTick(response.MemoryMeasurement);
                }
            }
        }

        private void runTick(BotEngine.Interface.FromProcessMeasurement<IMemoryMeasurement> eveMemory)
        {
            var ContextMenu = eveMemory?.Value?.Menu?.FirstOrDefault();
            var ContextMenuFirstEntry = ContextMenu?.Entry?.FirstOrDefault();

            var version = _jsContext.Execute("(function() { var res = null; require(['sanderling-v8/v8/interface/Espresso'], function(EspressoV8Interface) { res = EspressoV8Interface.singleton().onTick(); } ); return res; })();");
        }

        private void loadJsClassFile(string classpath, string classname)
        {
            string safeClasspath = classpath.Replace("..\\", "");
            string path = System.IO.Path.Combine(JS_SRC_PATH, safeClasspath + ".js");
            _jsContext.Execute(File.ReadAllText(path));
        }

        private void loadJsFile(string relativePath)
        {
            string safeClasspath = relativePath.Replace("..\\", "");
            string path = System.IO.Path.Combine(JS_SRC_PATH, safeClasspath + ".js");
            _jsContext.Execute(File.ReadAllText(path));
        }

        // from sanderlig project ...
        static private int? findEveClientProcessId() =>
			System.Diagnostics.Process.GetProcesses()
			?.FirstOrDefault(process =>
			{
				try
				{
					return string.Equals("ExeFile.exe", process?.MainModule?.ModuleName, StringComparison.InvariantCultureIgnoreCase);
				}
				catch { }

				return false;
			})
			?.Id;

    }
}