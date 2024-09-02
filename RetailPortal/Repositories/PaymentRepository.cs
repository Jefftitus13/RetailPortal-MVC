using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using RetailPortal.Models;

namespace RetailPortal.DataAccess
{
    public class PaymentRepository
    {
        private readonly string _connectionString;

        public PaymentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Method to get payment details by PaymentID
        public Payment? GetPaymentById(int paymentId)
        {
            Payment? payment = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("GetPaymentById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PaymentID", paymentId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            payment = new Payment
                            {
                                PaymentID = (int)reader["PaymentID"],
                                CardNumber = reader["CardNumber"].ToString(),
                                ExpiryDate = (DateTime)reader["ExpiryDate"],
                                CVV = reader["CVV"].ToString(),
                                PaymentAmount = (decimal)reader["PaymentAmount"],
                                PaymentDate = (DateTime)reader["PaymentDate"],
                                PolicyNumber = (int)reader["PolicyNumber"],
                                MemberId = (int)reader["MemberId"]
                            };
                        }
                    }
                }
            }

            return payment;
        }


        // Method to add a payment
        public void AddPayment(Payment payment)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand("GetPaymentById",connection);

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CardNumber", payment.CardNumber);
                command.Parameters.AddWithValue("@ExpiryDate", payment.ExpiryDate);
                command.Parameters.AddWithValue("@CVV", payment.CVV);
                command.Parameters.AddWithValue("@PaymentAmount", payment.PaymentAmount);
                command.Parameters.AddWithValue("@PaymentDate", payment.PaymentDate);
                command.Parameters.AddWithValue("@PolicyNumber", payment.PolicyNumber);
                command.Parameters.AddWithValue("@MemberId", payment.MemberId);

                command.ExecuteNonQuery();
            }
        }

        internal object GetPayments()
        {
            throw new NotImplementedException();
        }
    }
}
