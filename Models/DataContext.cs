using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;

namespace BaiTapLab05_19522534.Models
{
    public class DataContext
    {
        public string ConnectionString { get; set; }
        public DataContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public int InsertDiemCachLy(DiemCachLyModel diemCachLy)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                string sql = "insert into diemcachly values(@madiemcachly, @tendiemcachly, @diachi)";
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("madiemcachly", diemCachLy.MaDiemCachLy);
                command.Parameters.AddWithValue("tendiemcachly", diemCachLy.TenDiemCachLy);
                command.Parameters.AddWithValue("diachi", diemCachLy.DiaChi);
                return (command.ExecuteNonQuery());
            }
        }

        public List<Object> ListByTime(int soLan)
        {
            List<Object> list = new List<Object>();
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                string sql = @"
                select tencongnhan, namsinh, nuocve, count(*) as sotrieuchung
                from congnhan cn join cntc c on cn.macongnhan = c.macongnhan
                group by c.macongnhan, tencongnhan, namsinh, nuocve
                having count(*) >= @soLan;
                ";
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("soLan", soLan);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new 
                        {
                            TenCongNhan = reader["tencongnhan"].ToString(),
                            NamSinh = Convert.ToInt32(reader["namsinh"]),
                            NuocVe = reader["nuocve"].ToString(),
                            SoTrieuChung = Convert.ToInt32(reader["sotrieuchung"])
                        });
                    }
                    reader.Close();
                }
                conn.Close();
                return list;
            }
        }

        public List<DiemCachLyModel> LietKeDiemCachLy()
        {
            List<DiemCachLyModel> list = new List<DiemCachLyModel>();
            using(SqlConnection conn = GetConnection())
            {
                conn.Open();
                string sql = "select * from diemcachly";
                SqlCommand command = new SqlCommand(sql, conn);
                using (var reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        list.Add(new DiemCachLyModel
                        {
                            MaDiemCachLy = reader["madiemcachly"].ToString(),
                            TenDiemCachLy = reader["tendiemcachly"].ToString()
                        }); ;
                    }
                    reader.Close();
                }
                conn.Close();
                return list;
            }
        }

        public List<CongNhanModel> ListCongNhanTheoDiemCachLy(string maDiemCachLy)
        {
            List<CongNhanModel> list = new List<CongNhanModel>();
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                string sql = "select * from congnhan where madiemcachly = @maDiemCachLy";
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("maDiemCachLy", maDiemCachLy);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new CongNhanModel
                        {
                            MaCongNhan = reader["macongnhan"].ToString(),
                            TenCongNhan = reader["tencongnhan"].ToString()
                        }); ;
                    }
                    reader.Close();
                }
                conn.Close();
                return list;
            }
        }

        public void XoaCongNhan(string maCongNhan)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                string sql = "delete from congnhan where macongnhan = @maCongNhan";
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("maCongNhan", maCongNhan);
                command.ExecuteNonQuery();
            }
        }

        public CongNhanModel ChiTietCongNhan(string maCongNhan)
        {
            CongNhanModel congNhanModel = null;
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                string sql = "select * from congnhan where macongnhan = @maCongNhan";
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("maCongNhan", maCongNhan);
                using (var reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        congNhanModel =  new CongNhanModel
                        {
                            MaCongNhan = reader["macongnhan"].ToString(),
                            TenCongNhan = reader["tencongnhan"].ToString(),
                            GioiTinh = reader["gioitinh"].ToString(),
                            NamSinh = Convert.ToInt32(reader["namsinh"]),
                            NuocVe = reader["nuocve"].ToString(),
                        };
                    }
                    reader.Close();
                }
                conn.Close();
                return congNhanModel;
            }
        }
    }
}
