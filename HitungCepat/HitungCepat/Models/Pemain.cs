using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HitungCepat.Models
{
    public class Pemain
    {
        public string _id;
        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string _nama;
        [JsonProperty(PropertyName = "nama")]
        public string Nama
        {
            get { return _nama; }
            set { _nama = value; }
        }

        public int _skorMudah;
        [JsonProperty(PropertyName = "skorMudah")]
        public int SkorMudah
        {
            get { return _skorMudah; }
            set { _skorMudah= value; }
        }

        public int _skorSedang;
        [JsonProperty(PropertyName = "skorSedang")]
        public int SkorSedang
        {
            get { return _skorSedang; }
            set { _skorSedang= value; }
        }

        public int _skorSulit;
        [JsonProperty(PropertyName = "skorSulit")]
        public int SkorSulit
        {
            get { return _skorSulit; }
            set { _skorSulit= value; }
        }

        [Version]
        public string Version { get; set; }
    }
}
