namespace DirectorServer
{
    public class UnityClient
    {
        //======================================================================================
        // Purpose: Class for holding unity client info
        // =====================================================================================
        private string name = "";
        private bool publicServer = false;
        
        public string Name {
            get { return name; }
            set { name = value; }
        }
        public bool PublicServer {
            get { return publicServer; }
            set { publicServer = value; }
        }
    }
}