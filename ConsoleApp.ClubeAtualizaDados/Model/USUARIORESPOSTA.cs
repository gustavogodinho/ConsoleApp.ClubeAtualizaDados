namespace ConsoleApp.ClubeAtualizaDados.Model
{
    public class USUARIORESPOSTA
    {


        public class Message
        {
            public string message { get; set; }
            public Data data { get; set; }
            public object redirect { get; set; }
            public bool success { get; set; }
        }

        public class Data
        {
            public string id { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string document { get; set; }
            public string birthday { get; set; }
            public object gender { get; set; }
            public object civil_state { get; set; }
            public object state { get; set; }
            public object city { get; set; }
            public object extra { get; set; }
            public object avatar { get; set; }
            public int created_at { get; set; }
            public string updated_at { get; set; }
            public object deleted_at { get; set; }
            public object phone { get; set; }
            public object last_name { get; set; }
        }




    }
}
