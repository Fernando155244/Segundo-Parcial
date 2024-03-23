using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Java.Lang;
using System;
using System.Data;

namespace FernandoOrozco_Examen2P
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        DataSet ds;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            ListView lista = this.FindViewById<ListView>(Resource.Id.lsGaleria);
            clsDatos datos = new clsDatos();
            ds = datos.Listado();
            lista.Adapter = new galeria(this,ds);
            lista.ItemClick += Lista_ItemClick;
            
        }

        private void Lista_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            clsDatos datos = new clsDatos();
            View celda = e.View;
            ImageView imgPortada = celda.FindViewById<ImageView>(Resource.Id.imgTabPortada);
            int id = Convert.ToInt32((ds.Tables[0].Rows[e.Position]["id"]).ToString());
            string titulo = (ds.Tables[0].Rows[e.Position]["titulo"]).ToString();
            string clasificacion = (ds.Tables[0].Rows[e.Position]["clasificacion"]).ToString();
            string bmp1 = ds.Tables[0].Rows[e.Position]["foto"].ToString();
            Intent sp = new Intent(this, typeof(AcDescripcion));
            sp.PutExtra("id", id);
            sp.PutExtra("titulo", titulo);
            sp.PutExtra("portada", bmp1);
            sp.PutExtra("clasificacion", clasificacion);
            StartActivity(sp);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    internal class galeria : BaseAdapter
    {
        private MainActivity mainActivity;
        private DataSet ds;

        public galeria(MainActivity mainActivity, DataSet ds)
        {
            this.mainActivity = mainActivity;
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
            clsDatos datos = new clsDatos();
            View celda = convertView;
            if (celda == null)
            {
                celda = mainActivity.LayoutInflater.Inflate(Resource.Layout.tabGaleria, null);
            }
            ImageView imgportada = celda.FindViewById<ImageView>(Resource.Id.imgTabPortada);
            TextView lbltitulo  = celda.FindViewById<TextView>(Resource.Id.lblTabTitulo);
            TextView lblcategoria = celda.FindViewById<TextView>(Resource.Id.lblTabCategoria);
            Android.Graphics.Bitmap bmp1 = datos.descarga(ds.Tables[0].Rows[position]["foto"].ToString());
            lbltitulo.Text = ds.Tables[0].Rows[position]["titulo"].ToString();
            lblcategoria.Text = ds.Tables[0].Rows[position]["clasificacion"].ToString();
            imgportada.SetImageBitmap(bmp1);
            return celda;
        }
    }
}