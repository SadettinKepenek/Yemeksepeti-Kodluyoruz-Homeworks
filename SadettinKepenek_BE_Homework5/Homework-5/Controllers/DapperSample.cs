using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using Dapper;
using Homework_5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Homework_5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DapperSampleController : ControllerBase
    {
        private string _connectionString;

        public DapperSampleController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        [HttpGet("DapperInsert")]
        public IActionResult DapperInsert()
        {
            /*
             * Dapper'da insert işlemi yapmak için,bir sql sorgusunu execute eden Execute metodu kullanılır.
             * Bu metod parametre olarak sql sorgusunu ve sorguya ait parametreleri obje şeklinde alır.
             * Execure metodu affected row'ların sayısını döner
             */
            int affected = 0;
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                var query =
                    @"INSERT INTO Production.UnitMeasure (UnitMeasureCode,Name,ModifiedDate) VALUES (@UnitMeasureCode,@Name,@ModifiedDate)";

                if (dbConnection.State != ConnectionState.Open)
                {
                    dbConnection.Open();
                    affected = dbConnection.Execute(query, new
                    {
                        UnitMeasureCode = "H",
                        Name = "Planck Constant",
                        ModifiedDate = DateTime.Now
                    });
                }
            }

            return Ok(affected);
        }

        [HttpGet("DapperUpdate")]
        public IActionResult DapperUpdate()
        {
            /*
             * Dapper ile bir tablodaki datanın update edilmesi ile insert edilmesi arasında pek bir fark yoktur.
             * Yine Execute metodu kullanarak update query ile data update edilir.
             */
            int affected = 0;
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                var query =
                    @"UPDATE Production.UnitMeasure SET Name = @Name,ModifiedDate = @ModifiedDate where UnitMeasureCode = @UnitMeasureCode";

                if (dbConnection.State != ConnectionState.Open)
                {
                    dbConnection.Open();
                    affected = dbConnection.Execute(query, new
                    {
                        UnitMeasureCode = "H",
                        Name = "Planck Constant Updated",
                        ModifiedDate = DateTime.Now
                    });
                }
            }

            return Ok(affected);
        }

        [HttpGet("DapperSpInsert")]
        public IActionResult DapperSpInsert()
        {
            /*
            * Sql'de store prodecure'lar fonksiyon görevi görür ve içine yüklediğimiz business'a göre sorgularımızı çalıştırırlar.
             * InsertUnitMeasure sp'si ile UnitMeasures tablosuna insert işlemi yapar
            */
            int affected = 0;
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                if (dbConnection.State != ConnectionState.Open)
                {
                    dbConnection.Open();
                    affected = dbConnection.Execute("dbo.InsertUnitMeasure ", new
                    {
                        UnitMeasureCode = "TST",
                        Name = "Test",
                        ModifiedDate = DateTime.Now
                    }, commandType: CommandType.StoredProcedure);
                }

                return Ok(affected);
            }
        }

        [HttpGet("DapperSelectIn")]
        public IActionResult DapperSelectIn()
        {
            /*
             * Sql'de IN ifadesi, belirli bir alan üzerinden çoklu filtreleme yapmamızı sağlar.
             * Dapper'da bu ifadeye parametre aktarmak için liste şeklinde obje göndermeliyiz
             * Bu örnekte Product Review tablosundan sadece Rating alanı 4 ve 5 olan satırları getirdim.
             * Query metodu bir query'yi execute edip bir değer döner.Burada dönecek tipi  ProductReview olarak belirttim
             */

            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                IEnumerable<ProductReview> result = null;
                var query = @"SELECT * FROM Production.ProductReview WHERE Rating IN @Ratings";
                var ratings = new List<int> {4, 5};
                if (dbConnection.State != ConnectionState.Open)
                {
                    dbConnection.Open();
                    result = dbConnection.Query<ProductReview>(query, new {ratings});
                }

                return Ok(result);
            }
        }

        [HttpGet("DapperSelectResultMapping")]
        public IActionResult DapperSelectResultMapping()
        {
            /*
             * Dapper'da Query metoduna tip vererek sql den dönen datayı map edebiliriz.
             * Aşağıdaki kod Product Review Tablosundaki tüm datayı getirerek ProductReview Modeline Bind eder.
             */

            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                IEnumerable<ProductReview> result = null;
                var query = @"SELECT * FROM Production.ProductReview";
                if (dbConnection.State != ConnectionState.Open)
                {
                    dbConnection.Open();
                    result = dbConnection.Query<ProductReview>(query);
                }

                return Ok(result);
            }
        }

        [HttpGet("DapperMultipleQueryMapping")]
        public IActionResult DapperMultipleQueryMapping()
        {
            /*
             * Sql'de birden fazla komut çalıştırılabilir ve sonuçları alınabilir.
             * Dapper'da bu işlemi QueryMultiple metoduyla gerçekleştirebiliriz.
             */

            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                var query = @"SELECT * FROM Production.ProductReview WHERE ProductReviewID = @Id
                              SELECT * FROM Production.UnitMeasure  WHERE UnitMeasureCode = @UnitMeasureCode";
                if (dbConnection.State != ConnectionState.Open)
                {
                    dbConnection.Open();
                    var result = dbConnection.QueryMultiple(query, new
                    {
                        Id = 5,
                        UnitMeasureCode = "CM"
                    });
                    var reviews = result.Read<ProductReview>();
                    var measures = result.Read<UnitMeasure>();
                    return Ok((reviews, measures));
                }
            }

            return Ok();
        }


        [HttpGet("DapperOneToManyMapping")]
        public IActionResult DapperOneToManyMapping()
        {
            /*
             * Veri tabanındaki Customer tablosu ile SalesTerritory tablosu arasında one to many ilişkisi vardır
             * Veri tabanından Customer verilerini getiririken SalesTerritory bilgisini de getirmek istediğimizde join kullanırız
             * Dapper'da gelen veriyi map etmek için Query methodunda  3 tane tip belirtmeliyiz.
             *Birincisi ve ikinicis ilişiki kuralacak ilgili tablolardır.Üçüncüsü ise dönüş tipidir.
             * Query methodu bir Func<Customer,SalesTerritory,Customer>() paremetresi alacaktır.Burada yapmamız gereken işlem,gelen territory'yi Customer'ın SalesTerritory alanına eşitleyip customer'ı dönmektir.
             * Buradaki Split on parametresi,hangi kolon üzerinden join işlemi yapılacağını belirtir.
             */

            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                var query = @"Select * from [AdventureWorks2017].[Sales].[Customer]
                              INNER JOIN [AdventureWorks2017].[Sales].SalesTerritory S on  Customer.TerritoryID = S.TerritoryID";
                if (dbConnection.State != ConnectionState.Open)
                {
                    dbConnection.Open();
                    var result = dbConnection.Query<Customer, SalesTerritory, Customer>(query,
                        (customer, salesTerritory) =>
                        {
                            customer.SalesTerritory = salesTerritory;
                            return customer;
                        },splitOn:"TerritoryID");
                    return Ok(result);
                }
            }

            return Ok();
        }
        
        [HttpGet("DapperOneToOneMapping")]
        public IActionResult DapperOneToOneMapping()
        {
            /*
             * Bu case'de Person tablosu ile Customer tablosu arasında one to one ilişki vardır.
             * Inner join ve Query metodumuzu yukarıdaki gibi kullanarak result'u map edip Person nesnesini döneriz. 
             */

            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                var query = @"  SELECT * FROM [AdventureWorks2017].[Person].Person
                                INNER JOIN [AdventureWorks2017].[Sales].Customer C on  Person.BusinessEntityID = C.PersonID";
                if (dbConnection.State != ConnectionState.Open)
                {
                    dbConnection.Open();
                    var result = dbConnection.Query<Person, Customer, Person>(query,
                        (person, customer) =>
                        {
                            person.Customer = customer;
                            return person;
                        },splitOn:"PersonID");
                    return Ok(result);
                }
            }

            return Ok();
        }
        [HttpGet("DapperDelete")]
        public IActionResult DapperDelete()
        {
            /*
             * Tablodan bir satırı silmek için Execute metodu ile delete sorgusu çalıştırılır ve data silinir.
             */

            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                var query = @"DELETE Production.ProductReview WHERE ProductReviewId = @Id";
                int affected = 0;
                if (dbConnection.State != ConnectionState.Open)
                {
                    dbConnection.Open();
                    affected = dbConnection.Execute(query, new
                    {
                        Id = 1
                    });
                }

                return Ok(affected);
            }
        }

        [HttpGet("DapperDeleteIn")]
        public IActionResult DapperDeleteIn()
        {
            /*
             * Product Review tablosundan çoklu satır silmek için Delete komutu ve IN ifadesini kullandım.Id'si 4 5 6 olan satırları sildim
             */

            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                int affected = 0;
                var query = @"DELETE  FROM Production.ProductReview WHERE ProductReviewID IN @Ids";
                var ids = new List<int> {5, 6, 7};
                if (dbConnection.State != ConnectionState.Open)
                {
                    dbConnection.Open();
                    affected = dbConnection.Execute(query, new {ids});
                }

                return Ok(affected);
            }
        }

        [HttpGet("DapperTransaction")]
        public IActionResult DapperTransaction()
        {
            /*
             * Aşağıdaki ilk kod bloğu önceden yaptığım UnitMesaures tablosuna insert etme işlemini gerçekleştiriyor.
             * İkinci kod bloğuna geçmeden programın hata verdiği varsayalım.Biz gerçek hayatta böyle bi durumla karşılaşırsak hata alındığında tabloya
             * veri eklemesini istemeyiz.Bu yüzden transaction kullanırız.
             */

            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                int affected1 = 0;
                int affected2 = 0;
                var insertQuery =
                    @"INSERT INTO Production.UnitMeasure (UnitMeasureCode,Name,ModifiedDate) VALUES (@UnitMeasureCode,@Name,@ModifiedDate)";

                if (dbConnection.State != ConnectionState.Open)
                {
                    dbConnection.Open();
                    using (var transaction = dbConnection.BeginTransaction())
                    {
                        affected1 = dbConnection.Execute(insertQuery, new
                        {
                            UnitMeasureCode = "DNM",
                            Name = "Deneme",
                            ModifiedDate = DateTime.Now
                        }, transaction);
                        throw new Exception();
                        affected1 = dbConnection.Execute(insertQuery, new
                        {
                            UnitMeasureCode = "DNM",
                            Name = "Deneme",
                            ModifiedDate = DateTime.Now
                        }, transaction);
                    }
                }

                return Ok(($"Affected1:{affected1}", $"Affected2:{affected2}"));
            }
        }
    }
}