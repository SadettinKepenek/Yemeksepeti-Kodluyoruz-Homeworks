# week1_homework1

Sıfırdan Empty şeklinde ilerlenip bir WebAPI projesi oluşturulacak.
API Product üzerine olacak 

API Get - Post - Put - Delete - Options  methodlarını içerecek ve bunlara göre yapılması gerekenler sorunsuz çalışmalı.

Database ile çalışmadığımız için In-Memory bir liste tutulacak
List WEBAPI çalışma zamanında ayakta kalması için singleton Pattern'i kullanabilirsiniz ( Core AddSingleton kullanılmayacak)

Singleton dizayn'ı kullanılmadan önce ;
https://csharpindepth.com/articles/singleton 
Jon Skeet  amcanın konuyla ilgili önerilerinin okunmasını tavsiye ediyorum. Bu kısmı özellikle soracağım :)  Kendi pattern'inizi de makalede en çok  yatkın olduğunuz yöneteme göre tasarlamanızı istiyorum.

PostMan üzerinden tüm endpointlerimizi test edebiliyor olmalıyız.

Bonus : Proje kapanıp açıldığında Verileri kaybetmeyelim.



# Homework-1
- Program kapandığında verilerin saklanması için MSSQL database kullanılmıştır.
-  Code First yaklaşımı kullanıldığı için projeyi clone'layan kişinin database'i local'inde update etmesi gerekir.
- Migration kodda bulunmaktadır.

## Powershell

    week1-homework1-SadettinKepenek\Homework-1\Homework.Services.Product
  
  dizinine gidilip `dotnet ef update database` komutu çalıştırılmalıdır.
- Bu işlem başarısız olursa package manager console'dan aşağıdaki kod çalıştırılmalıdır.

## Package Manager Console
       week1-homework1-SadettinKepenek\Homework-1\Homework.Services.Product
  
  dizinine gidilip `Update-Database` komutu çalıştırılmalıdır.

[Request Scheme](https://github.com/YemekSepeti-WebAPI-Bootcamp/week1-homework1-SadettinKepenek/blob/main/is_akisi.png?raw=true)

