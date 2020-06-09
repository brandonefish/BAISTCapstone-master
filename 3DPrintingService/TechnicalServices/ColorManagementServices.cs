using _3DPrintingService.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace _3DPrintingService.TechnicalServices
{
    public class ColorManagementServices
    {
        public bool AddColor(string colorName)
        {
            bool Success = false;

            SqlConnection CapstoneColors = new SqlConnection();
            CapstoneColors.ConnectionString = @"Persist Security Info=False;Integrated Security=True;Database=CentreHigh3DPrintingServicesDB;Server=DATABAIST;";
            CapstoneColors.Open();

            SqlCommand Command = new SqlCommand
            {
                Connection = CapstoneColors,
                CommandType = CommandType.StoredProcedure,
                CommandText = "AddColor"
            };

            SqlParameter Parameter;
            Parameter = new SqlParameter
            {
                ParameterName = "@ColorName",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = colorName
            };
            Command.Parameters.Add(Parameter);

            Command.ExecuteNonQuery();
            CapstoneColors.Close();
            Success = true;

            return Success;
        }
        public bool RemoveColor(string colorNameRemove)
        {
            bool Success = false;

            SqlConnection CapstoneColors = new SqlConnection();
            CapstoneColors.ConnectionString = @"Persist Security Info=False;Integrated Security=True;Database=CentreHigh3DPrintingServicesDB;Server=DATABAIST;";
            CapstoneColors.Open();

            SqlCommand Command = new SqlCommand
            {
                Connection = CapstoneColors,
                CommandType = CommandType.StoredProcedure,
                CommandText = "RemoveColor"
            };

            SqlParameter Parameter;
            Parameter = new SqlParameter
            {
                ParameterName = "@ColorNameRemove",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = colorNameRemove
            };
            Command.Parameters.Add(Parameter);

            Command.ExecuteNonQuery();
            CapstoneColors.Close();
            Success = true;

            return Success;
        }
        public List<string> GetAvailableColors()
        {
            List<string> availableColors = new List<string>();
            SqlConnection CapstoneColors = new SqlConnection();
            CapstoneColors.ConnectionString = @"Persist Security Info=False;Integrated Security=True;Database=CentreHigh3DPrintingServicesDB;Server=DATABAIST;";
            CapstoneColors.Open();

            SqlCommand Command = new SqlCommand
            {
                Connection = CapstoneColors,
                CommandType = CommandType.StoredProcedure,
                CommandText = "GetAvailableColors"
            };

            SqlDataReader DR;
            DR = Command.ExecuteReader();

            if (DR.HasRows)
            {
                while (DR.Read())
                {
                    for (int i = 0; i < DR.FieldCount; i++)
                    {
                        availableColors.Add(DR[i].ToString());
                    }
                }
            }

            DR.Close();
            CapstoneColors.Close();
            return availableColors;
        }
        public bool SetAvailable(string colorName)
        {
            bool Success = false;
            SqlConnection CapstoneColors = new SqlConnection();
            CapstoneColors.ConnectionString = @"Persist Security Info=False;Integrated Security=True;Database=CentreHigh3DPrintingServicesDB;Server=DATABAIST;";
            CapstoneColors.Open();

            SqlCommand Command = new SqlCommand
            {
                Connection = CapstoneColors,
                CommandType = CommandType.StoredProcedure,
                CommandText = "SetAvailable"
            };
            SqlParameter Parameter;
            Parameter = new SqlParameter
            {
                ParameterName = "@ColorName",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = colorName
            };
            Command.Parameters.Add(Parameter);

            Command.ExecuteNonQuery();
            CapstoneColors.Close();
            Success = true;

            return Success;
        }
        public List<Color> GetAllColors()
        {
            List<Color> allColors = new List<Color>();
            SqlConnection CapstoneColors = new SqlConnection();
            CapstoneColors.ConnectionString = @"Persist Security Info=False;Integrated Security=True;Database=CentreHigh3DPrintingServicesDB;Server=DATABAIST;";
            CapstoneColors.Open();

            Color color = new Color();

            SqlCommand Command = new SqlCommand
            {
                Connection = CapstoneColors,
                CommandType = CommandType.StoredProcedure,
                CommandText = "GetAllColors"
            };

            SqlDataReader DataReader;
            DataReader = Command.ExecuteReader();

            if (DataReader.HasRows)
            {
                while (DataReader.Read())
                {
                    color = new Color();
                    color.ColorName = DataReader["ColorName"].ToString();
                    color.Available = Convert.ToBoolean(DataReader["Available"]);
                    allColors.Add(color);
                }
            }

            DataReader.Close();
            CapstoneColors.Close();
            return allColors;
        }
        public bool SetUnavailable(string colorName)
        {
            bool Success = false;
            SqlConnection CapstoneColors = new SqlConnection();
            CapstoneColors.ConnectionString = @"Persist Security Info=False;Integrated Security=True;Database=CentreHigh3DPrintingServicesDB;Server=DATABAIST;";
            CapstoneColors.Open();

            SqlCommand Command = new SqlCommand
            {
                Connection = CapstoneColors,
                CommandType = CommandType.StoredProcedure,
                CommandText = "SetUnavailable"
            };
            SqlParameter Parameter;
            Parameter = new SqlParameter
            {
                ParameterName = "@ColorName",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = colorName
            };
            Command.Parameters.Add(Parameter);

            Command.ExecuteNonQuery();
            CapstoneColors.Close();
            Success = true;

            return Success;
        }
    }
}
