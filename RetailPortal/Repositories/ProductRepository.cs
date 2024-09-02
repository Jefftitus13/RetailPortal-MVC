using RetailPortal.Models;
using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace RetailPortal.DataAccess
{
    public class ProductDetailsRepository
    {
        private readonly string _connectionString;

        public ProductDetailsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Method to get all ProductDetails
        public List<ProductDetails> GetAllProductDetails()
        {
            var productDetailsList = new List<ProductDetails>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("usp_GetAllProducts", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var productDetails = new ProductDetails
                            {
                                ProductID = reader.GetInt32(0),
                                ProductName = reader.GetString(1),
                                ProductType = reader.GetString(2),
                                CoverageAmount = reader.GetDecimal(3),
                                PremiumAmount = reader.GetDecimal(4),
                                EffectiveDate = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5),
                                ExpiryDate = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6),
                                PolicyNumber = reader.GetInt32(7),
                                MemberId = reader.GetInt32(8)
                            };

                            productDetailsList.Add(productDetails);
                        }
                    }
                }
            }

            return productDetailsList;
        }

        // Method to get ProductDetails by ProductID
        public ProductDetails? GetProductDetailsById(int productId)
        {
            ProductDetails? productDetails = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("usp_GetProductById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ProductID", productId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            productDetails = new ProductDetails
                            {
                                ProductID = reader.GetInt32(0),
                                ProductName = reader.GetString(1),
                                ProductType = reader.GetString(2),
                                CoverageAmount = reader.GetDecimal(3),
                                PremiumAmount = reader.GetDecimal(4),
                                EffectiveDate = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5),
                                ExpiryDate = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6),
                                PolicyNumber = reader.GetInt32(7),
                                MemberId = reader.GetInt32(8)
                            };
                        }
                    }
                }
            }

            return productDetails;
        }
    }
}
