using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FernandoOrozco_Examen2P
{
    [Activity(Label = "AcDescripcion")]
    public class AcDescripcion : Activity
    {
        DataSet ds;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Descriptcion);

            clsDatos datos = new clsDatos();
            int id = this.Intent.GetIntExtra("id", 0);
            ds = datos.horarios(id);



            ImageView imgPortada = this.FindViewById<ImageView>(Resource.Id.imgPelicula);
            TextView lblTitulo = this.FindViewById<TextView>(Resource.Id.lblPelibulaTitulo);
            TextView lblCategoria = this.FindViewById<TextView>(Resource.Id.lblPeliculaCategoria);
            TextView lblDescripcion = this.FindViewById<TextView>(Resource.Id.lblPeliculaDescripcion);
            ListView lsHorarios = this.FindViewById<ListView>(Resource.Id.lsPeliculaHorarios);

            Android.Graphics.Bitmap bandera = datos.descarga(this.Intent.GetStringExtra("portada"));
            imgPortada.SetImageBitmap(bandera);
            lblTitulo.Text = this.Intent.GetStringExtra("titulo");
            lblCategoria.Text = this.Intent.GetStringExtra("clasificacion");
            lblDescripcion.Text = datos.destcripcion(id);
            lsHorarios.Adapter = new Adaph(this, ds);
        }
    }

    internal class Adaph : BaseAdapter
    {
        private AcDescripcion acDescripcion;
        private DataSet ds;

        public Adaph(AcDescripcion acDescripcion, DataSet ds)
        {
            this.acDescripcion = acDescripcion;
            this.ds = ds;
        }

        public override int Count
        {
            get
            {
                return ds.Tables[0].Rows.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return "";
        }

        public override long GetItemId(int position)
        {
            return position;
        }


        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View celda = convertView;
            if (celda == null)
            {
                celda = acDescripcion.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2, null);
            }
            TextView txthora = celda.FindViewById<TextView>(Android.Resource.Id.Text1);

            txthora.Text = ds.Tables[0].Rows[position]["horario"].ToString();
            return celda;
        }
    }
 
}