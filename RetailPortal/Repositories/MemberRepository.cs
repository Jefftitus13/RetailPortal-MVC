using RetailPortal.Models;
using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace RetailPortal.DataAccess
{
    public class MemberDetailsRepository
    {
        private readonly string _connectionString;

        public MemberDetailsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<MemberDetails> GetMemberDetails()
        {
            var memberDetailsList = new List<MemberDetails>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("GetAllMembers", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            memberDetailsList.Add(new MemberDetails
                            {
                                MemberId = reader.GetInt32(0),
                                PolicyNumber = reader.GetInt32(1),
                                MemberName = reader.GetString(2),
                                MemberDOB = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3),
                                PhoneNumber = reader.GetString(4),
                                Gender = reader.GetString(5),
                                MaritalStatus = reader.GetString(6),
                                State = reader.GetString(7),
                                District = reader.GetString(8),
                                CurrentSalary = reader.GetString(9),
                                Height = reader.GetDecimal(10),
                                Weight = reader.GetDecimal(11),
                                RelationshipToPolicyholder = reader.GetString(12)
                            });
                        }
                    }
                }
            }
            return memberDetailsList;
        }

        public MemberDetails? GetMemberDetailsById(int id)
        {
            MemberDetails? memberDetails = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("GetMembersById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@MemberId", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            memberDetails = new MemberDetails
                            {
                                MemberId = reader.GetInt32(0),
                                PolicyNumber = reader.GetInt32(1),
                                MemberName = reader.GetString(2),
                                MemberDOB = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3),
                                PhoneNumber = reader.GetString(4),
                                Gender = reader.GetString(5),
                                MaritalStatus = reader.GetString(6),
                                State = reader.GetString(7),
                                District = reader.GetString(8),
                                CurrentSalary = reader.GetString(9),
                                Height = reader.GetDecimal(10),
                                Weight = reader.GetDecimal(11),
                                RelationshipToPolicyholder = reader.GetString(12)
                            };
                        }
                    }
                }
            }
            return memberDetails;
        }

        public void AddMemberDetails(MemberDetails memberDetails)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("InsertMember",connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PolicyNumber", memberDetails.PolicyNumber);
                    command.Parameters.AddWithValue("@MemberName", memberDetails.MemberName);
                    command.Parameters.AddWithValue("@MemberDOB", memberDetails.MemberDOB as object ?? DBNull.Value);
                    command.Parameters.AddWithValue("@PhoneNumber", memberDetails.PhoneNumber);
                    command.Parameters.AddWithValue("@Gender", memberDetails.Gender);
                    command.Parameters.AddWithValue("@MaritalStatus", memberDetails.MaritalStatus);
                    command.Parameters.AddWithValue("@State", memberDetails.State);
                    command.Parameters.AddWithValue("@District", memberDetails.District);
                    command.Parameters.AddWithValue("@CurrentSalary", memberDetails.CurrentSalary);
                    command.Parameters.AddWithValue("@Height", memberDetails.Height);
                    command.Parameters.AddWithValue("@Weight", memberDetails.Weight);
                    command.Parameters.AddWithValue("@RelationshipToPolicyholder", memberDetails.RelationshipToPolicyholder);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateMemberDetails(MemberDetails memberDetails)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("UpdateMember", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@MemberId", memberDetails.MemberId);
                    command.Parameters.AddWithValue("@PolicyNumber", memberDetails.PolicyNumber);
                    command.Parameters.AddWithValue("@MemberName", memberDetails.MemberName);
                    command.Parameters.AddWithValue("@MemberDOB", memberDetails.MemberDOB as object ?? DBNull.Value);
                    command.Parameters.AddWithValue("@PhoneNumber", memberDetails.PhoneNumber);
                    command.Parameters.AddWithValue("@Gender", memberDetails.Gender);
                    command.Parameters.AddWithValue("@MaritalStatus", memberDetails.MaritalStatus);
                    command.Parameters.AddWithValue("@State", memberDetails.State);
                    command.Parameters.AddWithValue("@District", memberDetails.District);
                    command.Parameters.AddWithValue("@CurrentSalary", memberDetails.CurrentSalary);
                    command.Parameters.AddWithValue("@Height", memberDetails.Height);
                    command.Parameters.AddWithValue("@Weight", memberDetails.Weight);
                    command.Parameters.AddWithValue("@RelationshipToPolicyholder", memberDetails.RelationshipToPolicyholder);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteMemberDetails(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("DeleteMember", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@MemberId", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
