using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using RetailPortal.Models;

public class PolicyDetailsRepository
{
    private readonly string _connectionString;

    public PolicyDetailsRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    // Get all PolicyDetails
    public List<PolicyDetails> GetAllPolicyDetails()
    {
        var policyDetailsList = new List<PolicyDetails>();

        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("GetAllPolicies", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var policyDetails = new PolicyDetails
                {
                    PolicyNumber = (int)reader["PolicyNumber"],
                    PolicyType = reader["PolicyType"].ToString(),
                    CoverageAmount = (decimal)reader["CoverageAmount"],
                    PremiumAmount = (decimal)reader["PremiumAmount"],
                    PolicyStatus = reader["PolicyStatus"].ToString(),
                    PolicyholderName = reader["PolicyholderName"].ToString(),
                    ContactInformation = reader["ContactInformation"].ToString(),
                    InsuredName = reader["InsuredName"].ToString(),
                    LastPaymentDate = reader["LastPaymentDate"] as DateTime?,
                    NextDueDate = reader["NextDueDate"] as DateTime?,
                    PaymentStatus = reader["PaymentStatus"].ToString(),
                    SponsorId = (int)reader["SponsorId"]
                };

                policyDetailsList.Add(policyDetails);
            }
        }

        return policyDetailsList;
    }

    // Get all Sponsors
    public List<SponsorDetails> GetAllSponsors()
    {
        var sponsorDetailsList = new List<SponsorDetails>();

        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM SponsorDetails", conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var sponsorDetails = new SponsorDetails
                {
                    SponsorId = (int)reader["SponsorId"],
                    SponsorName = reader["SponsorName"].ToString(),
                    SponsorEmail = reader["SponsorEmail"].ToString(),
                    PhoneNumber = (int)reader["PhoneNumber"],
                    DateOfBirth = reader["DateOfBirth"] as DateTime?,
                    Gender = reader["Gender"].ToString()
                };

                sponsorDetailsList.Add(sponsorDetails);
            }
        }

        return sponsorDetailsList;
    }

    // Get a single PolicyDetails by PolicyNumber
    public PolicyDetails? GetPolicyDetails(int policyNumber)
    {
        PolicyDetails? policyDetails = null;

        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("GetPolicyByNumber", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PolicyNumber", policyNumber);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                policyDetails = new PolicyDetails
                {
                    PolicyNumber = (int)reader["PolicyNumber"],
                    PolicyType = reader["PolicyType"].ToString(),
                    CoverageAmount = (decimal)reader["CoverageAmount"],
                    PremiumAmount = (decimal)reader["PremiumAmount"],
                    PolicyStatus = reader["PolicyStatus"].ToString(),
                    PolicyholderName = reader["PolicyholderName"].ToString(),
                    ContactInformation = reader["ContactInformation"].ToString(),
                    InsuredName = reader["InsuredName"].ToString(),
                    LastPaymentDate = reader["LastPaymentDate"] as DateTime?,
                    NextDueDate = reader["NextDueDate"] as DateTime?,
                    PaymentStatus = reader["PaymentStatus"].ToString(),
                    SponsorId = (int)reader["SponsorId"]
                };
            }
        }

        return policyDetails;
    }
}
