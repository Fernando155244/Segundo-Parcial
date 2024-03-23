using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;

namespace FernandoOrozco_Examen2P
{
    [Activity(Label = "clsDatos")]
    public class clsDatos : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
        }
        public DataSet Listado ()
        {
            WebReference.Service1 ws = new WebReference.Service1();
            DataSet ds = new DataSet();
            ds = ws.CargaPeliculas();
            return ds;
        }
        public string destcripcion (int id)
        {
            WebReference.Service1 ws = new WebReference.Service1();
            string ds;
            ds = ws.CargaDescripcion(id);
            return ds;
        }
        public DataSet horarios(int id)
        {
            WebReference.Service1 ws = new WebReference.Service1();
            DataSet ds = new DataSet();
            ds = ws.CargaHorarios(id);
            return ds;
        }
        public Bitmap descarga(string url)
        {
            WebClient wc = new WebClient();
            byte[] arr = wc.DownloadData(url);
            Bitmap bmp = BitmapFactory.DecodeByteArray(arr, 0, arr.Length);
            return bmp;
        }
    }
}