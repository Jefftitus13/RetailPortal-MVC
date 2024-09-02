using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using RetailPortal.Models;

namespace RetailPortal.DataAccess
{
    public class SponsorDetailsRepository
    {
        private readonly string _connectionString;

        public SponsorDetailsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Get all sponsor details
        public IEnumerable<SponsorDetails> GetSponsorDetails()
        {
            var sponsorDetailsList = new List<SponsorDetails>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("usp_GetAllSponsors", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var sponsorDetails = new SponsorDetails
                            {
                                SponsorId = Convert.ToInt32(reader["Sponsor_id"]),
                                SponsorName = reader["Sponsor_name"].ToString(),
                                SponsorEmail = reader["Sponsor_email"].ToString(),
                                PhoneNumber = Convert.ToInt32(reader["Phone_number"]),
                                DateOfBirth = reader["Date_of_birth"] as DateTime?,
                                Gender = reader["Gender"].ToString()
                            };

                            sponsorDetailsList.Add(sponsorDetails);
                        }
                    }
                }
            }

            return sponsorDetailsList;
        }

        // Get sponsor details by ID
        public SponsorDetails? GetSponsorDetailsById(int sponsorId)
        {
            SponsorDetails? sponsorDetails = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("usp_GetSponsorDetailsById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Sponsor_id", sponsorId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            sponsorDetails = new SponsorDetails
                            {
                                SponsorId = Convert.ToInt32(reader["Sponsor_id"]),
                                SponsorName = reader["Sponsor_name"].ToString(),
                                SponsorEmail = reader["Sponsor_email"].ToString(),
                                PhoneNumber = Convert.ToInt32(reader["Phone_number"]),
                                DateOfBirth = reader["Date_of_birth"] as DateTime?,
                                Gender = reader["Gender"].ToString()
                            };
                        }
                    }
                }
            }

            return sponsorDetails;
        }

        // Add a new sponsor
        public void AddSponsorDetails(SponsorDetails sponsorDetails)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("usp_InsertSponsorDetails", connection))
                {
                    command.CommandType= CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Sponsor_name", sponsorDetails.SponsorName);
                    command.Parameters.AddWithValue("@Sponsor_email", sponsorDetails.SponsorEmail);
                    command.Parameters.AddWithValue("@Phone_number", sponsorDetails.PhoneNumber);
                    command.Parameters.AddWithValue("@Date_of_birth", sponsorDetails.DateOfBirth.HasValue ? (object)sponsorDetails.DateOfBirth.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@Gender", sponsorDetails.Gender);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Update sponsor details
        public void UpdateSponsorDetails(SponsorDetails sponsorDetails)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("usp_UpdateSponsorDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Sponsor_id", sponsorDetails.SponsorId);
                    command.Parameters.AddWithValue("@Sponsor_name", sponsorDetails.SponsorName);
                    command.Parameters.AddWithValue("@Sponsor_email", sponsorDetails.SponsorEmail);
                    command.Parameters.AddWithValue("@Phone_number", sponsorDetails.PhoneNumber);
                    command.Parameters.AddWithValue("@Date_of_birth", sponsorDetails.DateOfBirth.HasValue ? (object)sponsorDetails.DateOfBirth.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@Gender", sponsorDetails.Gender);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Delete sponsor details
        public void DeleteSponsorDetails(int sponsorId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("usp_DeleteSponsorDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Sponsor_id", sponsorId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
