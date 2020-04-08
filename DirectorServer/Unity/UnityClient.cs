namespace DirectorServer
{
    public class UnityClient
    {
        private int id = -1;
        private string token = null;
        private string name = null;
        private bool publicServer = false;
        
        public int Id {
            get { return id; }
            set {
                if (id == -1)
                    id = value;
            }
        }
        public string Token {
            get { return token; }
            set {
                if (token == null)
                    token = value;
            }
        }
        public string Name {
            get { return name; }
            set {
                if (name == null)
                    name = value;
            }
        }
        public bool PublicServer {
            get { return publicServer; }
            set { publicServer = value; }
        }
    }
}