using ProductoInvent.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace ProductoInvent
{
   public class ProductoInventRepository :BaseRepository
    {
        public SqlConnectionStringBuilder AdoSqlConnection;
        public ProductoInventRepository()
        {
            AdoSqlConnection = ADOSqlConnection;
        }
        public int AddNewProduct(ProductCollectionModel productCollectionModel)
        {
            int rowsAffected = 0;
            SqlConnection sqlConnection = new SqlConnection(AdoSqlConnection.ConnectionString);
            sqlConnection.Open();            
            using (var command = new SqlCommand("insert into salesLt.Product (Name,ProductNumber,standardcost,ListPrice,sellStartdate,ModifiedDate,ThumbNailPhotoFileName) values('"+productCollectionModel.ProductName+"','"+productCollectionModel.ProductNumber+"',"+productCollectionModel.Price+","+productCollectionModel.Price+",'"+productCollectionModel.CreatedDateTime.ToString("yyyy-MM-dd hh:mm") +"','"+DateTime.Now.ToString("yyyy-MM-dd hh: mm") +"','"+productCollectionModel.FileName +"')", sqlConnection))
            {
                rowsAffected = command.ExecuteNonQuery();
            }
            sqlConnection.Close();

            return rowsAffected;
        }

        public bool DeleteProduct(int productId)
        {
            int rowsAffected = 0;
            SqlConnection sqlConnection = new SqlConnection(AdoSqlConnection.ConnectionString);
            sqlConnection.Open();
            var productCollections = new List<ProductCollectionModel>();
            using (var command = new SqlCommand("update salesLt.Product set discontinuedDate ='"+DateTime.Now.ToString("yyyy-MM-dd hh:mm")+"' where ProductId ="+productId, sqlConnection))
            {
                rowsAffected=command.ExecuteNonQuery();
            }
            sqlConnection.Close();

            return rowsAffected>0;
        }

        public ProductCollectionModel GetProductDetails(int productId)
        {
            SqlConnection sqlConnection = new SqlConnection(AdoSqlConnection.ConnectionString);
            sqlConnection.Open();
            var productCollection = new ProductCollectionModel();
            using (var command = new SqlCommand("select ProductId,name,standardcost,sellstartdate,ModifiedDate,productnumber,ThumbnailPhotoFileName from SalesLt.Product where discontinueddate is null and Productid="+ productId +"order by 1 desc", sqlConnection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                       
                        productCollection.ProductId = reader.GetInt32(0);
                        productCollection.ProductName = reader.GetString(1);
                        productCollection.CreatedDateTime = reader.GetDateTime(3);
                        productCollection.ProductNumber = reader.GetString(5);
                        productCollection.Price = reader.GetDecimal(2);
                        productCollection.ModifiedDateTime = reader.GetDateTime(4);
                        productCollection.FileName = reader.GetString(6);
                    }
                }
            }
            sqlConnection.Close();
            return productCollection;
        }

        public int EditProduct(ProductCollectionModel productCollectionModel)
        {
            

             int rowsAffected = 0;
            SqlConnection sqlConnection = new SqlConnection(AdoSqlConnection.ConnectionString);
            sqlConnection.Open();
            using (var command = new SqlCommand("update SalesLt.Product set Name = '"+ productCollectionModel.ProductName
                +"', ProductNumber = '"+ productCollectionModel.ProductNumber
                +"', standardCost ="+productCollectionModel.Price
                +", sellStartdate = '"+productCollectionModel.CreatedDateTime
                +"', Thumbnailphotofilename = '"+productCollectionModel.FileName
                +"'where ProductId = "+productCollectionModel.ProductId, sqlConnection))
            {
                rowsAffected = command.ExecuteNonQuery();
            }
            sqlConnection.Close();

            return rowsAffected;
        }

        public List<ProductCollectionModel> GetProductCollections()
        {
            SqlConnection sqlConnection = new SqlConnection(AdoSqlConnection.ConnectionString);
            sqlConnection.Open();
            var productCollections = new List<ProductCollectionModel>();
            using (var command = new SqlCommand("select ProductId,name,standardcost,ModifiedDate,productNumber,ThumbnailPhotoFilename from SalesLt.Product where discontinueddate is null order by 1 desc", sqlConnection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ProductCollectionModel productCollection = new ProductCollectionModel();
                        productCollection.ProductId = reader.GetInt32(0);
                        productCollection.ProductName = reader.GetString(1);
                        productCollection.Price = reader.GetDecimal(2);
                        productCollection.ModifiedDateTime = reader.GetDateTime(3);
                        productCollection.ProductNumber = reader.GetString(4);
                        productCollection.FileName = reader.GetString(5);
                        productCollections.Add(productCollection);
                    }
                }
            }
            sqlConnection.Close();
            return productCollections;
        }

        public List<ProductCollectionModel> GetProductCollectionsWithImage()
        {
            SqlConnection sqlConnection = new SqlConnection(AdoSqlConnection.ConnectionString);
            sqlConnection.Open();
            var productCollections = new List<ProductCollectionModel>();
            using (var command = new SqlCommand("select top 4 ProductId,thumbnailphotofileName from salesLt.Product Order by ProductId desc", sqlConnection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ProductCollectionModel productCollection = new ProductCollectionModel();                        
                        productCollection.FileName = reader.GetString(1);
                        productCollections.Add(productCollection);
                    }
                }
            }
            sqlConnection.Close();
            productCollections.ElementAt(0).ActiveClass = "active";            
            return productCollections;
        }

    }
}