using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer
{
    class UserController : IController
    {
        Dictionary<string, Action> methods = new Dictionary<string, Action>();
        private string[] args;
        private Client client;

        public UserController()
        {
            methods.Add("logout", new Action(Logout));
        }

        public bool HandleRequest(Client client, params string[] args)
        {
            this.client = client;

            if (args.Length <= 0)
            {
                return false;
            }

            this.args = args;

            if (methods.ContainsKey(args[0]))
            {
                methods[args[0]].Invoke();
            }

            return false;
        }

        public void Logout()
        {
            if (UserManager.Logout(this.client))
            {
                client.Redirect("../login");
            }
        }
    }
}
