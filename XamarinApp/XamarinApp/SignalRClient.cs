using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace XamarinApp
{
    public class SignalRClient : INotifyPropertyChanged
    {
        private HubConnection Connection;
        private IHubProxy ChatHubProxy;

        public SignalRClient(string url)
        {
            Connection = new HubConnection(url, new Dictionary<string, string>
                                   {
                                       { "ImatisServiceAuth", "0BEAA8EB10B78386E0CEE178285234150856D0636B4A9930E648DBE4B2B6487E58626CA58B2645811BD68E39E40D19140B8FA1CA1929BE25E72778228C58FD171F985E68EAAE359F8584E64E4FEF27CAEED6084AF38A16F097E93F2A12C90D893D26B932C6970FD72B26DD7FC5725955AFCF7B8C2D5ADA7EA72CB9CCDFB487B7C953DDA66A7EC53E0F7BF94A86B0B777F3DCB3738FED8B8D2A86976BAC1A29C06D1898BA83456160FB723CFB1A99879904B1995C83071B233E510F7A8894DA7846450F11E71ED3E2EAD0AB202830E94048920C25F67B927B6FF5EC5DE24DC935923B05A65BE3CDB93FDCFAE91F1502AAFC32C722E9C7CD3C5AB96FB072492A64973AFF90FD0D311655E0CB5F9ACB526A03B4E33CF59F3595CC941EED3E89BB83D8CF3B014907290370B298FA4A138B4649A0E7BF4451B9F3EC18F64BE0DFE3639597E1889424B3418AA8C2941FFE016DCC7736FD139D7892E5062BFD1CB96EF1DC93F2454EDE5987CB416419570B2AF3AE88955A535B62EC946EEFC5987259FD7A329FD1A650C87E0802068D5467E1B046E43B23F920BA6AC0565C346FB45AF57EF7E40B12E4F6AE969E69A5A920CA66A8CD20AA61A4B620B78A4F050ED6B86A8A4877041F832F796083220BA0AD4A90283D3871EC433F4700074AE10BB957A4A9DD186629E754290F8DFDF3D7F896060C40531814E4FE192D4CD83F76153DF134ED7A1E07493CD99B85E5AF5FEC04AA3832B4CB0D494E695E0058B479EB683A8335E6DD9D91792BB2DD24A244DE86DF2D6CB96C1E8F2CB9CE6144C343DDFED9A9B092A1520EB3D0285C1A2E8E177F4C3A73E9296899E4F69A018699212511DB3D4EE12709E5F998A467583CAF6B4DFE49986C989ABE5DC9C332DF6D78DF740144378068" }
                                   });

            Connection.StateChanged += (StateChange obj) => {
                OnPropertyChanged("ConnectionState");
            };

            ChatHubProxy = Connection.CreateHubProxy("mainHub");
        }

        public Task Start()
        {
            return Connection.Start();
        }

        public bool IsConnectedOrConnecting
        {
            get
            {
                return Connection.State != ConnectionState.Disconnected;
            }
        }

        public ConnectionState ConnectionState { get { return Connection.State; } }

        public static async Task<SignalRClient> CreateAndStart(string url)
        {
            var client = new SignalRClient(url);
            await client.Start();
            return client;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
