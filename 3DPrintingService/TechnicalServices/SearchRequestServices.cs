using _3DPrintingService.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace _3DPrintingService.TechnicalServices
{
    public class SearchRequestServices
    {
        public bool AddSearchRequest(string multiPart, string description)
        {
            bool Success = false;

            SqlConnection CentreHigh3DPrintingServices = new SqlConnection();
            CentreHigh3DPrintingServices.ConnectionString = @"Persist Security Info=False;Integrated Security=True;Database=CentreHigh3DPrintingServicesDB;server=DATABAIST;";
            CentreHigh3DPrintingServices.Open();

            SqlCommand AddCommand = new SqlCommand
            {
                Connection = CentreHigh3DPrintingServices,
                CommandType = CommandType.StoredProcedure,
                CommandText = "CreateSearchRequest"
            };

            SqlParameter AddCommandParameter;

            AddCommandParameter = new SqlParameter
            {
                ParameterName = "@MultiPart",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = multiPart
            };
            AddCommand.Parameters.Add(AddCommandParameter);

            AddCommandParameter = new SqlParameter
            {
                ParameterName = "@Description",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = description
            };
            AddCommand.Parameters.Add(AddCommandParameter);

            AddCommand.ExecuteNonQuery();

            CentreHigh3DPrintingServices.Close();

            Success = true;

            return Success;
        }

        public List<SearchRequest> GetSearchRequests()
        {


            SqlConnection CentreHigh3DPrintingServices = new SqlConnection();
            CentreHigh3DPrintingServices.ConnectionString = @"Persist Security Info=False;Integrated Security=True;Database=CentreHigh3DPrintingServicesDB;server=DATABAIST;";
            CentreHigh3DPrintingServices.Open();


            SqlDataReader SearchRequestDataReader;
            List<SearchRequest> Req = new List<SearchRequest>();

            SqlCommand GetSearchRequestsCommand = new SqlCommand
            {
                Connection = CentreHigh3DPrintingServices,
                CommandType = CommandType.StoredProcedure,
                CommandText = "GetSearchRequests"
            };


            SearchRequestDataReader = GetSearchRequestsCommand.ExecuteReader();

            while (SearchRequestDataReader.Read())
            {
                SearchRequest request = new SearchRequest
                {
                    MultiPart = SearchRequestDataReader["MultiPart"].ToString(),
                    Description = SearchRequestDataReader["Description"].ToString()
                };
                Req.Add(request);
            }







            SearchRequestDataReader.Close();
            CentreHigh3DPrintingServices.Close();



            return Req;


        }
    }
}
