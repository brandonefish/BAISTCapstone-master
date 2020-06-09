using _3DPrintingService.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _3DPrintingService.TechnicalServices
{
    public class AppFileServices
    {
        public bool UploadAppFile(AppFile newFile)
        {
            bool success = false;

            SqlConnection UPLOADDOWNLOAD = new SqlConnection();
            UPLOADDOWNLOAD.ConnectionString = @"Persist Security Info=False;Integrated Security=True;Database=CentreHigh3DPrintingServicesDB;Server=DATABAIST;";
            UPLOADDOWNLOAD.Open();

            SqlCommand AddCommand = new SqlCommand
            {
                Connection = UPLOADDOWNLOAD,
                CommandType = CommandType.StoredProcedure,
                CommandText = "UploadFile"
            };

            SqlParameter AddCommandParameter;
            AddCommandParameter = new SqlParameter
            {
                ParameterName = "@Content",
                SqlDbType = SqlDbType.VarBinary,
                Direction = ParameterDirection.Input,
                SqlValue = newFile.Content
            };
            AddCommand.Parameters.Add(AddCommandParameter);

            AddCommandParameter = new SqlParameter
            {
                ParameterName = "@FileName",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = newFile.FileName
            };
            AddCommand.Parameters.Add(AddCommandParameter);

            AddCommandParameter = new SqlParameter
            {
                ParameterName = "@FileType",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = newFile.FileType
            };
            AddCommand.Parameters.Add(AddCommandParameter);

            AddCommandParameter = new SqlParameter
            {
                ParameterName = "@ColorName",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = newFile.ColorName
            };
            AddCommand.Parameters.Add(AddCommandParameter);

            AddCommandParameter = new SqlParameter
            {
                ParameterName = "@Comments",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = newFile.Comments
            };
            AddCommand.Parameters.Add(AddCommandParameter);

            AddCommand.ExecuteNonQuery();
            UPLOADDOWNLOAD.Close();
            success = true;

            return success;
        }


        ////List of UploadedFiles For download 

        public List<AppFile> GetUploadedFiles()
        {
            List<AppFile> UploadedFiles = new List<AppFile>();

            SqlConnection UPLOADDOWNLOAD = new SqlConnection();
            UPLOADDOWNLOAD.ConnectionString = @"Persist Security Info=False;Integrated Security=True;Database=CentreHigh3DPrintingServicesDB;Server=DATABAIST;";
            UPLOADDOWNLOAD.Open();

            SqlCommand AddCommand = new SqlCommand
            {
                Connection = UPLOADDOWNLOAD,
                CommandType = CommandType.StoredProcedure,
                CommandText = "GetStoredFiles"
            };

            SqlDataReader DataReader;
            DataReader = AddCommand.ExecuteReader();

            AppFile UploadedAppFile;

            if (DataReader.HasRows)
            {
                while (DataReader.Read())
                {
                    UploadedAppFile = new AppFile();
                    UploadedAppFile.Id = int.Parse(DataReader["Id"].ToString());
                    UploadedAppFile.FileName = DataReader["FileName"].ToString();
                    UploadedAppFile.FileType = DataReader["FileType"].ToString();
                    UploadedAppFile.Content = DataReader["Content"] as Byte[];
                    UploadedAppFile.Comments = DataReader["Comments"].ToString();
                    UploadedAppFile.ColorName = DataReader["ColorName"].ToString();

                    UploadedFiles.Add(UploadedAppFile);
                }
            }
            UPLOADDOWNLOAD.Close();

            return UploadedFiles;
        }


        ///Download Files


        public AppFile DownloadAppFile(int id)
        {
            AppFile DownloadFile = new AppFile();

            SqlConnection UPLOADDOWNLOAD = new SqlConnection();
            UPLOADDOWNLOAD.ConnectionString = @"Persist Security Info=False;Integrated Security=True;Database=CentreHigh3DPrintingServicesDB;Server=DATABAIST;";
            UPLOADDOWNLOAD.Open();

            SqlCommand AddCommand = new SqlCommand
            {
                Connection = UPLOADDOWNLOAD,
                CommandType = CommandType.StoredProcedure,
                CommandText = "DownloadFile"
            };

            SqlParameter AddCommandParameter;
            AddCommandParameter = new SqlParameter
            {
                ParameterName = "@ID",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = id
            };
            AddCommand.Parameters.Add(AddCommandParameter);

            SqlDataReader DataReader;
            DataReader = AddCommand.ExecuteReader();

            if (DataReader.HasRows)
            {
                while (DataReader.Read())
                {
                    DownloadFile.Id = id;
                    DownloadFile.FileName = DataReader["FileName"].ToString();
                    DownloadFile.FileType = DataReader["FileType"].ToString();
                    DownloadFile.Content = DataReader["Content"] as Byte[];
                }
            }

            return DownloadFile;
        }
    }
}
