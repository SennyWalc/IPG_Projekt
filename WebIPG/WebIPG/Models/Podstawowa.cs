using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebIPG.Models
{
    interface IServicePodstawowa
    {
        List<Podstawowa> GetAll();
        Podstawowa GetById(int value);
        void Update(Podstawowa value);
    }
    public class Podstawowa
    {
        public int id { get; set; }
        public string nazwa { get; set; }
      
      
        public DateTime dataPoczatek { get; set; }
        public DateTime dataKoniec { get; set; }
        public string osobaOdpowiedzialna { get; set; }
        public int? typ { get; set; }
    }
    public class ServicePodstawowa : IServicePodstawowa
    {
        public List<Podstawowa> GetAll()
        {
            SqlConnection con = null;
            List<Podstawowa> result = new List<Podstawowa>();
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString());
                SqlCommand cmd = new SqlCommand("AllPodstawowa", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Podstawowa podstawowa = new Podstawowa();
                    podstawowa.id = (int)reader[0];
                    podstawowa.nazwa = reader[1].ToString();
                    podstawowa.dataPoczatek=(DateTime)reader[2];
                    podstawowa.dataKoniec = (DateTime)reader[3];
                    podstawowa.osobaOdpowiedzialna = reader[4].ToString();
                    podstawowa.typ = (int)reader[5];
                    result.Add(podstawowa);
                }
                return result;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        public Podstawowa GetById(int value)
        {
            SqlConnection con = null;
            Podstawowa podstawowa = new Podstawowa();
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString());
                SqlCommand cmd = new SqlCommand("PodstawowaID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", value);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
    
                    podstawowa.id = (int)reader[0];
                    podstawowa.nazwa = reader[1].ToString();
                    podstawowa.dataPoczatek = (DateTime)reader[2];
                    podstawowa.dataKoniec = (DateTime)reader[3];
                    podstawowa.osobaOdpowiedzialna = reader[4].ToString();
                    podstawowa.typ = (int)reader[5];
                }
                return podstawowa;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        public void Update(Podstawowa value)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString());
                SqlCommand cmd = new SqlCommand("UpdatePodstawowa", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", value.id);
                cmd.Parameters.AddWithValue("@nazwa", value.nazwa);
                cmd.Parameters.AddWithValue("@dataPoczatek", Convert.ToDateTime( value.dataPoczatek));
                cmd.Parameters.AddWithValue("@dataKoniec", Convert.ToDateTime (value.dataKoniec));
                cmd.Parameters.AddWithValue("@osobaOdpowiedzialna", value.osobaOdpowiedzialna);
                cmd.Parameters.AddWithValue("@typ", value.typ);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}