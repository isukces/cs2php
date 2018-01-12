using Lang.Php.Compiler;
using Lang.Php.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using Lang.Php.Webserver.Properties;

namespace Lang.Php.Webserver
{
 
    public partial class ServerEngine
    {
        // Private Methods 

        private static void Send(Socket handler, byte[] byteData)
        {
            handler.BeginSend(byteData, 0, byteData.Length, 0, SendCallback, handler);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                var handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine(Resources.Sent__0__bytes_to_client_, bytesSent);

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private readonly TranslationInfo _translationInfo = new TranslationInfo(new AssemblySandbox(null));

        // Public Methods 

        private void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.
            allDone.Set();

            // Get the socket that handles the client request.
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            // Create the state object.
            var state = new StateObject { workSocket = handler };
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, ReadCallback, state);
        }

        public void Load(string n)
        {
            // todo:use Application domain instead of Assembly.Load
            var loadedAssembly = Assembly.LoadFile(n);
            _assemblies.Add(loadedAssembly);
            var types = loadedAssembly.GetTypes();
            foreach (var type in types)
            {
                var a1 = type.GetCustomAttribute<PageAttribute>();
                if (a1 == null) continue;
                var m = a1.ModuleShortName;
                _pages[m + ".php"] = type;
            }
        }

        private HttpResponse ProcessRequest(HttpRequest x)
        {
            DoLog(string.Format("{0} {1}", x.Method, x.RequestUri));
            string script = x.Script;
            var httpResponse = new HttpResponse();
            Type type;
            if (script.EndsWith("/"))
            {
                var g = _indexOrder.Split(' ');
                foreach (var gg in g)
                {
                    type = TypeByScript(script + gg);
                    if (type != null)
                        return ProcessRequestInSandbox(x, httpResponse, type);
                }
                httpResponse.StatusCode = HttpStatusCode.NotFound;
                return httpResponse;

            }
            type = TypeByScript(script);
            if (type != null)
                return ProcessRequestInSandbox(x, httpResponse, type);
            httpResponse.StatusCode = HttpStatusCode.NotFound;
            return httpResponse;
        }

        private void ReadCallback(IAsyncResult ar)
        {
            // Retrieve the state object and the handler socket
            // from the asynchronous state object.
            var state = (StateObject)ar.AsyncState;
            var handler = state.workSocket;

            // Read data from the client socket. 
            var bytesRead = handler.EndReceive(ar);

            if (bytesRead <= 0) return;
            // There  might be more data, so store the data received so far.
            state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

            // Check for end-of-file tag. If it is not there, read 
            // more data.
            var content = state.sb.ToString();
            if (content.IndexOf("\r\n", StringComparison.Ordinal) > -1)
            {
                var httpRequest = HttpRequest.Parse(content);
                {
                    httpRequest.Server.DocumentRoot = _documentRoot.Replace("\\", "/");

                    if (!httpRequest.Server.DocumentRoot.EndsWith("/"))
                        httpRequest.Server.DocumentRoot += "/";
                    httpRequest.Update();
                }
                var resp = ProcessRequest(httpRequest);
                var byteContent = resp.GetComplete(OutupEncoding);
                // All the data has been read from the 
                // client. Display it on the console.
                Console.WriteLine("Read {0} bytes from socket. \n Data : {1}", content.Length, content);
                // Echo the data back to the client.
                Send(handler, byteContent);
            }
            else
            {
                // Not all data received. Get more.
                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, ReadCallback, state);
            }
        }

        public void StartListening()
        {
            // Data buffer for incoming data.

            // Establish the local endpoint for the socket.
            // The DNS name of the computer
            // running the listener is "host.contoso.com".
            var ipHostInfo = Dns.Resolve(Dns.GetHostName());
            var ipAddress = ipHostInfo.AddressList[0];
            ipAddress = IPAddress.Loopback;
            var localEndPoint = new IPEndPoint(ipAddress, ListenPort);

            // Create a TCP/IP socket.
            var listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections.
            try
            {
                listener.Bind(localEndPoint);
                _work = true;
                listener.Listen(100);

                Thread t = new Thread(() =>
                {

                    while (true)
                    {
                        allDone.Reset();
                        Console.WriteLine("Waiting for a connection...");
                        listener.BeginAccept(AcceptCallback, listener);
                        allDone.WaitOne();
                        if (!_work)
                            break;
                    }
                });
                t.Start();



            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }

        public void Stop()
        {
            _work = false;
            allDone.Reset();
        }
        // Private Methods 

        void DoLog(string x)
        {
            if (OnLog == null)
                return;
            OnLog(this, new OnLogEventArgs { Text = x });
        }

        private HttpResponse ProcessRequest(HttpRequest req, Type type)
        {
            var y = new HttpResponse();
            ClassTranslationInfo ci = _translationInfo.GetOrMakeTranslationInfo(type);

            if (ci.PageMethod == null)
                throw new Exception(string.Format("Page method not found in type {0}", type.FullName));
            {
                // prepare sandbox
                Response.RuntimeResponse = y;
                Request.RuntimeRequest = req;
                Script.Get = req.Get;
                Script.Post = req.Post;
                Script.Server = req.Server;
            }

            Action phpMain = (Action)Delegate.CreateDelegate(typeof(Action), ci.PageMethod);
            phpMain(); // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! Wywołanie metody
            // y.Echo("Mock output");
            return y;
        }

        private HttpResponse ProcessRequestInSandbox(HttpRequest x, HttpResponse y, Type type)
        {
            try
            {
                y = ProcessRequest(x, type);
                DoLog(string.Format("{0}", y.StatusCode));
                return y;
            }
            catch (Exception e)
            {
                y.StatusCode = HttpStatusCode.InternalServerError;
                y.Echo(e.Message);
                DoLog(string.Format("{0} {1}", y.StatusCode, e.Message));
                return y;
            }
        }

        private Type TypeByScript(string script)
        {
            var a = _pages.Where(i => "/" + i.Key == script).ToArray();
            return a.Any() ? a.First().Value : null;
        }

        public static ManualResetEvent allDone = new ManualResetEvent(false);

        bool _work;

        // Events 

        public event EventHandler<OnLogEventArgs> OnLog;


        public class OnLogEventArgs : EventArgs
        {
            public string Text { get; set; }
        }


        /// <summary>
        /// Pole statyczne dla singletona
        /// </summary>
        private static ServerEngine _instance;
        private static readonly object __staticLockObject__ = new Object();

        // Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
        // see http://www.yoda.arachsys.com/csharp/beforefieldinit.html
        // see also http://msdn.microsoft.com/en-us/library/ff650316.aspx
        // see also http://www.albahari.com/threading/part4.aspx#_Memory_Barriers_and_Volatility
        static ServerEngine()
        {
        }

        /// <summary>
        /// Instancja obiektu - realizuje wzorzec Singleton
        /// </summary>
        public static ServerEngine Instance
        {
            get
            {
                if (_instance == (object)null)
                {
                    lock(__staticLockObject__)
                    {
                        if (_instance == (object)null)
                            _instance = new ServerEngine();
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// Tworzy instancję obiektu - metoda prywatna z uwagi na użycie wzorca Singleton
        /// </summary>
        private ServerEngine()
        {
        }

        /// <summary>
        /// Nazwa własności Assemblies; 
        /// </summary>
        public const string PropertyNameAssemblies = "Assemblies";
        /// <summary>
        /// Nazwa własności Pages; 
        /// </summary>
        public const string PropertyNamePages = "Pages";
        /// <summary>
        /// Nazwa własności IndexOrder; 
        /// </summary>
        public const string PropertyNameIndexOrder = "IndexOrder";
        /// <summary>
        /// Nazwa własności ListenPort; 
        /// </summary>
        public const string PropertyNameListenPort = "ListenPort";
        /// <summary>
        /// Nazwa własności OutupEncoding; 
        /// </summary>
        public const string PropertyNameOutupEncoding = "OutupEncoding";
        /// <summary>
        /// Nazwa własności DocumentRoot; 
        /// </summary>
        public const string PropertyNameDocumentRoot = "DocumentRoot";

        /// <summary>
        /// 
        /// </summary>
        public List<Assembly> Assemblies
        {
            get
            {
                return _assemblies;
            }
            set
            {
                _assemblies = value;
            }
        }
        private List<Assembly> _assemblies = new List<Assembly>();
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, Type> Pages
        {
            get
            {
                return _pages;
            }
            set
            {
                _pages = value;
            }
        }
        private Dictionary<string, Type> _pages = new Dictionary<string, Type>();
        /// <summary>
        /// 
        /// </summary>
        public string IndexOrder
        {
            get
            {
                return _indexOrder;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                _indexOrder = value;
            }
        }
        private string _indexOrder = "index.php index.htm index.html";
        /// <summary>
        /// 
        /// </summary>
        public int ListenPort { get; set; } = 81;

        /// <summary>
        /// 
        /// </summary>
        public Encoding OutupEncoding { get; set; } = Encoding.UTF8;

        /// <summary>
        /// 
        /// </summary>
        public string DocumentRoot
        {
            get
            {
                return _documentRoot;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                _documentRoot = value;
            }
        }
        private string _documentRoot = string.Empty;
    }
}
